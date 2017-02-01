using Autofac;
using Autofac.Integration.WebApi;
using Squares.WebApi;

namespace Squares.DI
{
    public class WebApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(typeof(WebApiHost).Assembly).PropertiesAutowired();
        }
    }
}