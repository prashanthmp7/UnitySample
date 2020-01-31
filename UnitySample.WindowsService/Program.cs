using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace UnitySample.TestService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            if (args.Length >= 1 && args.Contains("/c"))
            {
                Service svc = new Service();
                svc.StartService(args);
                Console.ReadLine();
                svc.StopService();
            }
            else
            {
                ServiceBase[] servicesToRun = {
                    new Service()
                };
                ServiceBase.Run(servicesToRun);
            }
        }
    }
}
