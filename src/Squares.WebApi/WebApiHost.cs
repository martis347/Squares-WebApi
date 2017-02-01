using System;
using Autofac;
using Microsoft.Owin.Hosting;

namespace Squares.WebApi
{
    public class WebApiHost
    {
        private IDisposable _webApp;
        private readonly string _baseUrl;
        private readonly ILifetimeScope _container;

        public WebApiHost(string baseUrl, ILifetimeScope container)
        {
            _baseUrl = baseUrl;
            _container = container;
        }

        public void Start()
        {
            _webApp = WebApp.Start(
                _baseUrl,
                builder =>
                {
                    new Startup(_container).Configure(builder);
                });
        }

        public void Stop()
        {
            _webApp.Dispose();
        }
    }
}
