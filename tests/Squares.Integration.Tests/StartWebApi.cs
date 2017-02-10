using System.Configuration;
using System.IO;
using Autofac;
using Squares.WebApi;

namespace Squares.Integration.Tests
{
    public class StartWebApi
    {
        private readonly WebApiHost _webApiHost;
        private readonly ILifetimeScope _container = DI.ServicesContainer.Resolve<ILifetimeScope>();

        public StartWebApi(string url)
        {
            ClearDirectory(ConfigurationManager.AppSettings["PointsLocation"]);
            ClearDirectory(ConfigurationManager.AppSettings["SquaresLocation"]);

            _webApiHost = new WebApiHost(url, _container);
        }

        public void Start()
        {
            _webApiHost.Start();
        }

        public void Clean()
        {
            ClearDirectory(ConfigurationManager.AppSettings["PointsLocation"]);
            ClearDirectory(ConfigurationManager.AppSettings["SquaresLocation"]);
        }

        private void ClearDirectory(string directory)
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