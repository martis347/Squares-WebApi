using System;

namespace Squares
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceHost = new ServiceHost("http://+:1069/");
            serviceHost.Start();

            Console.ReadLine();
        }
    }
}
