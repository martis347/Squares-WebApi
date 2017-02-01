using System;
using System.Net.Http.Formatting;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Owin;

namespace Squares.WebApi
{
    public class Startup
    {
        #region Constructor

        private readonly ILifetimeScope _container;

        public Startup(ILifetimeScope container)
        {
            if (container == null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            _container = container;
        }

        #endregion

        #region Configure

        public void Configure(IAppBuilder appBuilder)
        {
            var configuration = new HttpConfiguration
            {
                IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always,
                DependencyResolver = new AutofacWebApiDependencyResolver(_container)
            };

            RegisterRoutes(configuration);
            RegisterFormatters(configuration);
            appBuilder.UseWebApi(configuration);
        }

        private void RegisterFormatters(HttpConfiguration configuration)
        {
            configuration.Formatters.Add(new JsonMediaTypeFormatter
            {
                SerializerSettings =
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    MissingMemberHandling = MissingMemberHandling.Error,
                    NullValueHandling = NullValueHandling.Ignore
                }
            });
        }

        private static void RegisterRoutes(HttpConfiguration configuration)
        {
            configuration.MapHttpAttributeRoutes();

            configuration.Routes.MapHttpRoute(
                name: "Default",
                routeTemplate: "{controller}",
                defaults: new { controller = "Default" },
                constraints: null
                );
        }

        #endregion
    }
}