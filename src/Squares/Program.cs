using System;
using System.Configuration;

namespace Squares
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceUrl = ConfigurationManager.AppSettings["WebApiServiceUrl"];
            var serviceHost = new ServiceHost(serviceUrl);
            serviceHost.Start();
            
            Console.WriteLine($"Web api service started. Url: {serviceUrl}");
            Console.ReadLine();
        }
    }
}
