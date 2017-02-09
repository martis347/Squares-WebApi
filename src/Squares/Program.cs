using System;
using System.Configuration;
using System.IO;

namespace Squares
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!Directory.Exists("../../../../Files"))
            {
                Directory.CreateDirectory("../../../../Files");
            }
            var serviceUrl = ConfigurationManager.AppSettings["WebApiServiceUrl"];
            var serviceHost = new ServiceHost(serviceUrl);
            serviceHost.Start();
            
            Console.WriteLine($"Web api service started. Url: {serviceUrl}");
            Console.ReadLine();
        }
    }
}
