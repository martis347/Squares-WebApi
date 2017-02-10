using System.Configuration;
using System.IO;
using Autofac;
using Squares.WebApi;

namespace Squares.Integration.Tests.Helpers
{
    public static class StartWebApi
    {
        private static WebApiHost _webApiHost;
        private static readonly ILifetimeScope Container = DI.ServicesContainer.Resolve<ILifetimeScope>();

        public static void Start(string url)
        {
            ClearDirectory(ConfigurationManager.AppSettings["PointsLocation"]);
            ClearDirectory(ConfigurationManager.AppSettings["SquaresLocation"]);

            if (_webApiHost == null)
            {
                _webApiHost = new WebApiHost(url, Container);
                _webApiHost.Start();
            }

        }

        public static void Stop()
        {
            _webApiHost.Stop();
        }

        private static void ClearDirectory(string directory)
        {
            if (Directory.Exists(directory))
            {

                DirectoryInfo di = new DirectoryInfo(directory);

                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    dir.Delete(true);
                }
            }

            Directory.CreateDirectory(directory);
        }
    }
}