using System;
using Autofac;
using Squares.WebApi;

namespace Squares
{
    public class ServiceHost
    {
        private readonly WebApiHost _webApiHost;

        public ServiceHost(string webApiBaseUrl)
        {
            if (String.IsNullOrWhiteSpace(webApiBaseUrl))
            {
                throw new ArgumentException("webApiBaseUrl");
            }
            _webApiHost = new WebApiHost(webApiBaseUrl, DI.ServicesContainer.Resolve<ILifetimeScope>());
        }

        public void Start()
        {
            _webApiHost.Start();
        }

        public void Stop()
        {
            _webApiHost.Stop();
        }
    }
}