using CommonServiceLocator;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Text;
using System.Threading.Tasks;
using UnitySample.Common.Exceptions;

namespace UnitySample.Common.ServiceClient
{
    public class ServiceClient<T>
    {
        private readonly ChannelFactory<T> ChannelFactory = new ChannelFactory<T>(Consts.Consts.Urls.EndpointConfigurationName);

        /// <summary>
        /// Executes a wcf service call asynchronous.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="action">The action.</param>
        /// <returns>the result</returns>
        public async Task<TResult> ExecuteAsync<TResult>(Func<T, Task<TResult>> action)
        {
            IClientChannel clientChannel = (IClientChannel)ChannelFactory.CreateChannel();

            TaskCompletionSource<TResult> taskCompletionSource = new TaskCompletionSource<TResult>();
            try
            {
                taskCompletionSource.TrySetResult(await DoExecuteAsync(action, clientChannel));
                clientChannel.Close();
            }
            catch (TimeoutException exception)
            {
                Logger.Logger.Write($"\rTimeout Exception:\n----------------------------\n" +
                                    $"Method: {action.Method}\n" +
                                    $"Target: {action.Target}\n" +
                                    $"Exception message: {exception.Message}\n" +
                                    $"Exception detail: {exception}\n" +
                                    $"Stack Trace: {exception.StackTrace}\n"
                                    , "Error", TraceEventType.Error);
                taskCompletionSource.TrySetException(exception);
                clientChannel.Abort();
            }
            // Catch the contractually specified SOAP fault raised here as an exception.
            catch (FaultException<RemoteFault> exception)
            {
                Logger.Logger.Write($"\rRemote Fault Exception:\n----------------------------\n" +
                                    $"Exception message: {exception.Message}" +
                                    $"\r\nMethod: {action.Method}\n" +
                                    $"Target: {action.Target}\n" +
                                    $"Fault ID: {exception.Detail.FaultId }\nStackTrace: {exception.Detail.FaultDescription}"
                                    , "Error", TraceEventType.Error);
                taskCompletionSource.TrySetException(exception);
                clientChannel.Abort();
            }
            // Catch unrecognized faults. This handler receives exceptions thrown by WCF
            // services when ServiceDebugBehavior.IncludeExceptionDetailInFaults 
            // is set to true.
            catch (FaultException exception)
            {
                Logger.Logger.Write($"\rFault Exception:\n----------------------------\n" +
                                    $"\r\nMethod: {action.Method}\n" +
                                    $"Target: {action.Target}\n" +
                                    $"Exception message: {exception.Message}\n" +
                                    $"Exception detail: {exception}\n" +
                                    $"Stack Trace: {exception.StackTrace}\n"
                                    , "Error", TraceEventType.Error);
                taskCompletionSource.TrySetException(exception);
                clientChannel.Abort();
            }
            catch (SecurityAccessDeniedException exception)
            {
                Logger.Logger.Write($"\rSecurityAccessDenied Exception:\n----------------------------\n" +
                                   $"Method: {action.Method}\n" +
                                   $"Target: {action.Target}\n" +
                                   $"Exception message: {exception.Message}\n" +
                                   $"Exception detail: {exception}\n" +
                                   $"Stack Trace: {exception.StackTrace}\n"
                                   , "Error", TraceEventType.Error);
                taskCompletionSource.TrySetException(exception);
                clientChannel.Abort();
            }
            catch (CommunicationException exception)///only for communication exceptions 3 attempts will be made
            {
                Logger.Logger.Write($"\rCommunication Exception:\n----------------------------\n" +
                                   $"Method: {action.Method}\n" +
                                   $"Target: {action.Target}\n" +
                                   $"Exception message: {exception.Message}\n" +
                                   $"Exception detail: {exception}\n" +
                                   $"Stack Trace: {exception.StackTrace}\n"
                                   , "Error", TraceEventType.Error);
                //Try 3 attempts before throwing exception
                var attempts = 0;
                while (++attempts <= 3)
                {
                    try
                    {
                        clientChannel.Abort();//Since we got an exception its important to abort the current open channel and create a new one
                        clientChannel = (IClientChannel)ChannelFactory.CreateChannel();
                        taskCompletionSource.TrySetResult(await DoExecuteAsync(action, clientChannel));
                        clientChannel.Close();
                        await Task.Delay(2000);
                        break;
                    }
                    catch (Exception innerException)
                    {
                        Logger.Logger.Write($"\rAttempt#{attempts}" +
                                  $"Exception message: {innerException.Message}\n" +
                                  $"Exception detail: {innerException}\n" +
                                  $"Stack Trace: {innerException.StackTrace}\n"
                                  , "Error", TraceEventType.Error);
                        clientChannel.Abort();
                    }
                }
                if (attempts > 3)//More than 3 throw the error back to client
                {
                    IEventAggregator eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
                    eventAggregator.GetEvent<CommunicationError>().Publish(exception);
                    taskCompletionSource.TrySetException(exception);
                    clientChannel.Abort();
                }
            }
            catch (Exception exception)
            {
                Logger.Logger.Write($"\rException:\n----------------------------\n" +
                                   $"Method: {action.Method}\n" +
                                   $"Target: {action.Target}\n" +
                                   $"Exception message: {exception.Message}\n" +
                                   $"Exception detail: {exception}\n" +
                                   $"Stack Trace: {exception.StackTrace}\n"
                                   , "Error", TraceEventType.Error);
                taskCompletionSource.TrySetException(exception);
                clientChannel.Abort();
            }
            finally
            {
                ChannelFactory.Close();
            }
            return await taskCompletionSource.Task;
        }

        private Task<TResult> DoExecuteAsync<TResult>(Func<T, Task<TResult>> action, IClientChannel clientChannel)
        {
#if DEBUG
            if (!string.IsNullOrEmpty(Consts.Consts.OnBehalfUser))
            {
                using (new OperationContextScope(clientChannel))
                {
                    System.ServiceModel.Channels.MessageHeader header = System.ServiceModel.Channels.MessageHeader.CreateHeader("OnBehalfUser", "http://test.com", Consts.Consts.OnBehalfUser);
                    OperationContext.Current.OutgoingMessageHeaders.Add(header);
                    return action((T)clientChannel);
                }
            }
#endif
            return action((T)clientChannel);
        }

        /// <summary>
        /// Executes the specified action.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="action">The action.</param>
        /// <returns>the result</returns>
        public TResult Execute<TResult>(Func<T, TResult> action)
        {
            IClientChannel clientChannel = (IClientChannel)ChannelFactory.CreateChannel();
            TResult result = default(TResult);
            try
            {
                result = action((T)clientChannel);
                clientChannel.Close();
            }
            catch (TimeoutException exception)
            {
                Logger.Logger.Write($"\rTimeout Exception:\n----------------------------\n" +
                                    $"Method: {action.Method}\n" +
                                    $"Target: {action.Target}\n" +
                                    $"Exception message: {exception.Message}\n" +
                                    $"Exception detail: {exception}\n" +
                                    $"Stack Trace: {exception.StackTrace}\n"
                                    , "Error", TraceEventType.Error);
                clientChannel.Abort();
            }
            // Catch the contractually specified SOAP fault raised here as an exception.
            catch (FaultException<RemoteFault> exception)
            {
                Logger.Logger.Write($"\rRemote Fault Exception:\n----------------------------\n" +
                                   $"Exception message: {exception.Message}" +
                                   $"\r\nMethod: {action.Method}\n" +
                                   $"Target: {action.Target}\n" +
                                   $"Fault ID: {exception.Detail.FaultId }\nStackTrace: {exception.Detail.FaultDescription}"
                                   , "Error", TraceEventType.Error);
                clientChannel.Abort();
            }
            // Catch unrecognized faults. This handler receives exceptions thrown by WCF
            // services when ServiceDebugBehavior.IncludeExceptionDetailInFaults 
            // is set to true.
            catch (FaultException exception)
            {
                Logger.Logger.Write($"\rFault Exception:\n----------------------------\n" +
                                        $"\r\nMethod: {action.Method}\n" +
                                        $"Target: {action.Target}\n" +
                                        $"Exception message: {exception.Message}\n" +
                                        $"Exception detail: {exception}\n" +
                                        $"Stack Trace: {exception.StackTrace}\n"
                                        , "Error", TraceEventType.Error);
                clientChannel.Abort();
            }
            catch (SecurityAccessDeniedException exception)
            {
                Logger.Logger.Write($"\rSecurityAccessDenied Exception:\n----------------------------\n" +
                                    $"Method: {action.Method}\n" +
                                    $"Target: {action.Target}\n" +
                                    $"Exception message: {exception.Message}\n" +
                                    $"Exception detail: {exception}\n" +
                                    $"Stack Trace: {exception.StackTrace}\n"
                                    , "Error", TraceEventType.Error);
                clientChannel.Abort();
            }
            catch (CommunicationException exception)///only for communication exceptions 3 attempts will be made
            {
                Logger.Logger.Write($"\rCommunication Exception:\n----------------------------\n" +
                                   $"Method: {action.Method}\n" +
                                   $"Target: {action.Target}\n" +
                                   $"Exception message: {exception.Message}\n" +
                                   $"Exception detail: {exception}\n" +
                                   $"Stack Trace: {exception.StackTrace}\n"
                                   , "Error", TraceEventType.Error);
                //Try 3 attempts before throwing exception
                var attempts = 0;
                while (++attempts <= 3)
                {
                    try
                    {
                        clientChannel.Abort();//Since we got an exception its important to abort the current open channel and create a new one
                        clientChannel = (IClientChannel)ChannelFactory.CreateChannel();
                        clientChannel.Close();
                        Task tsk = Task.Run(async () => await Task.Delay(2000));
                        Task.WaitAll(tsk);
                        break;
                    }
                    catch (Exception innerException)
                    {
                        Logger.Logger.Write($"\rAttempt#{attempts}" +
                                 $"Exception message: {innerException.Message}\n" +
                                 $"Exception detail: {innerException}\n" +
                                 $"Stack Trace: {innerException.StackTrace}\n"
                                 , "Error", TraceEventType.Error);
                        clientChannel.Abort();
                    }
                }
                if (attempts > 3)//More than 3 throw the error back to client
                {
                    IEventAggregator eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
                    eventAggregator.GetEvent<CommunicationError>().Publish(exception);
                    clientChannel.Abort();
                }
            }
            catch (Exception exception)
            {
                Logger.Logger.Write($"\rException:\n----------------------------\n" +
                                   $"Method: {action.Method}\n" +
                                   $"Target: {action.Target}\n" +
                                   $"Exception message: {exception.Message}\n" +
                                   $"Exception detail: {exception}\n" +
                                   $"Stack Trace: {exception.StackTrace}\n"
                                   , "Error", TraceEventType.Error);
                clientChannel.Abort();
            }
            finally
            {
                ChannelFactory.Close();
            }
            return result;
        }

        /// <summary>
        /// Executes the specified action.
        /// </summary>
        /// <param name="action">The action.</param>
        public void Execute(Action<T> action)
        {
            IClientChannel clientChannel = (IClientChannel)ChannelFactory.CreateChannel();
            try
            {
                action((T)clientChannel);
                clientChannel.Close();
            }
            catch (TimeoutException exception)
            {
                Logger.Logger.Write($"\rTimeout Exception:\n----------------------------\n" +
                                    $"Method: {action.Method}\n" +
                                    $"Target: {action.Target}\n" +
                                    $"Exception message: {exception.Message}\n" +
                                    $"Exception detail: {exception}\n" +
                                    $"Stack Trace: {exception.StackTrace}\n"
                                    , "Error", TraceEventType.Error);
                clientChannel.Abort();
            }
            // Catch the contractually specified SOAP fault raised here as an exception.
            catch (FaultException<RemoteFault> exception)
            {
                Logger.Logger.Write($"\rRemote Fault Exception:\n----------------------------\n" +
                                   $"Exception message: {exception.Message}" +
                                   $"\r\nMethod: {action.Method}\n" +
                                   $"Target: {action.Target}\n" +
                                   $"Fault ID: {exception.Detail.FaultId }\nStackTrace: {exception.Detail.FaultDescription}"
                                   , "Error", TraceEventType.Error);
                clientChannel.Abort();
            }
            // Catch unrecognized faults. This handler receives exceptions thrown by WCF
            // services when ServiceDebugBehavior.IncludeExceptionDetailInFaults 
            // is set to true.
            catch (FaultException exception)
            {
                Logger.Logger.Write($"\rFault Exception:\n----------------------------\n" +
                                        $"\r\nMethod: {action.Method}\n" +
                                        $"Target: {action.Target}\n" +
                                        $"Exception message: {exception.Message}\n" +
                                        $"Exception detail: {exception}\n" +
                                        $"Stack Trace: {exception.StackTrace}\n"
                                        , "Error", TraceEventType.Error);
                clientChannel.Abort();
            }
            catch (SecurityAccessDeniedException exception)
            {
                Logger.Logger.Write($"\rSecurityAccessDenied Exception:\n----------------------------\n" +
                                    $"Method: {action.Method}\n" +
                                    $"Target: {action.Target}\n" +
                                    $"Exception message: {exception.Message}\n" +
                                    $"Exception detail: {exception}\n" +
                                    $"Stack Trace: {exception.StackTrace}\n"
                                    , "Error", TraceEventType.Error);
                clientChannel.Abort();
            }
            catch (CommunicationException exception)///only for communication exceptions 3 attempts will be made
            {
                Logger.Logger.Write($"\rCommunication Exception:\n----------------------------\n" +
                                   $"Method: {action.Method}\n" +
                                   $"Target: {action.Target}\n" +
                                   $"Exception message: {exception.Message}\n" +
                                   $"Exception detail: {exception}\n" +
                                   $"Stack Trace: {exception.StackTrace}\n"
                                   , "Error", TraceEventType.Error);
                //Try 3 attempts before throwing exception
                var attempts = 0;
                while (++attempts <= 3)
                {
                    try
                    {
                        clientChannel.Abort();//Since we got an exception its important to abort the current open channel and create a new one
                        clientChannel = (IClientChannel)ChannelFactory.CreateChannel();
                        clientChannel.Close();
                        //Task tsk = Task.Run(async () => await Task.Delay(2000));
                        //Task.WaitAll(tsk);
                        break;
                    }
                    catch (Exception innerException)
                    {
                        Logger.Logger.Write($"\rAttempt#{attempts}" +
                                $"Exception message: {innerException.Message}\n" +
                                $"Exception detail: {innerException}\n" +
                                $"Stack Trace: {innerException.StackTrace}\n"
                                , "Error", TraceEventType.Error);
                        clientChannel.Abort();
                    }
                }
                if (attempts > 3)//More than 3 throw the error back to client
                {
                    IEventAggregator eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
                    eventAggregator.GetEvent<CommunicationError>().Publish(exception);
                    clientChannel.Abort();
                }
            }
            catch (Exception exception)
            {
                Logger.Logger.Write($"\rException:\n----------------------------\n" +
                                   $"Method: {action.Method}\n" +
                                   $"Target: {action.Target}\n" +
                                   $"Exception message: {exception.Message}\n" +
                                   $"Exception detail: {exception}\n" +
                                   $"Stack Trace: {exception.StackTrace}\n"
                                   , "Error", TraceEventType.Error);
                clientChannel.Abort();
            }
            finally
            {
                ChannelFactory.Close();
            }
        }
    }
}
