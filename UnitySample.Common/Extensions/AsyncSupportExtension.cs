using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Unity.Interception.PolicyInjection.Pipeline;

namespace UnitySample.Common.Extensions
{
    /// <summary>
    /// Extension für Interception in asynchronen Methoden
    /// </summary>
    public static class AsyncSupportExtension
    {
        /// <summary>
        /// Invokes the after call action. This method handles the async methods as well.
        /// If the method returns <see cref="Task"/> or the generic version the action will be called after the task has been completed.
        /// </summary>
        /// <param name="input">Inputs to the current call to the target.</param>
        /// <param name="afterCall">The action to call after the current method executed.</param>
        /// <param name="methodReturn">The computed method return.</param>
        /// <returns>The original method return.</returns>
        public static IMethodReturn InvokeAfterCall(this IMethodReturn methodReturn, IMethodInvocation input, Action<IMethodReturn> afterCall)
        {
            if (afterCall != null)
            {
                var returnType = (input.MethodBase as MethodInfo).ReturnType;
                if (returnType == typeof(Task) || (returnType.IsGenericType && returnType.BaseType == typeof(Task)))
                {
                    Task myTask = methodReturn.ReturnValue as Task;
                    myTask?.ContinueWith(task =>
                    {
                        IMethodReturn asyncResult;
                        if (task.Exception != null)
                        {
                            asyncResult = input.CreateExceptionMethodReturn(task.Exception);
                        }
                        else
                        {
                            object taskReturnValue = null;
                            if (returnType.IsGenericType)
                            {
                                taskReturnValue = (task as dynamic).Result;
                            }
                            asyncResult = input.CreateMethodReturn(taskReturnValue);
                        }
                        afterCall.Invoke(asyncResult);
                    });
                }
                else
                {
                    afterCall.Invoke(methodReturn);
                }
            }
            return methodReturn;
        }
    }
}
