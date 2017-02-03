using System;
using System.Web.Http;
using System.Web.Http.Cors;
using Autofac;
using Autofac.Integration.WebApi;
using Newtonsoft.Json;
using Owin;
using Squares.WebApi.Filters;

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
                DependencyResolver = new AutofacWebApiDependencyResolver(_container),
                Filters =
                {
                    new ValidateModelAttribute(),
                    new ExceptionFilter()
                }
            };
            configuration.EnableCors(new EnableCorsAttribute(
                origins: "*",
                headers: "*",
                methods: "*"));

            RegisterFormatters(configuration);
            RegisterRoutes(configuration);
            appBuilder.UseWebApi(configuration);
        }

        private void RegisterFormatters(HttpConfiguration configuration)
        {
            configuration.Formatters.JsonFormatter.SerializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
        }

        private static void RegisterRoutes(HttpConfiguration configuration)
        {
            configuration.MapHttpAttributeRoutes();

            configuration.Routes.MapHttpRoute(
                name: "Default",
                routeTemplate: "{controller}/",
                defaults: new { controller = "Default" },
                constraints: null
                );
        }

        #endregion
    }
}