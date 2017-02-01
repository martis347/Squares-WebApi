using Autofac;

namespace Squares.DI
{
    public static class ServicesContainer
    {
        public static IContainer Container;

        static ServicesContainer()
        {
            CreateContainer();
        }

        private static void CreateContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<WebApiModule>();

            builder.RegisterType<Program>()
                .AsSelf()
                .SingleInstance();

            Container = builder.Build();
        }
        public static T Resolve<T>() where T : class
        {
            return Container.Resolve<T>();
        }
    }
}