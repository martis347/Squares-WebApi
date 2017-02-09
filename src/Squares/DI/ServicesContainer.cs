using Autofac;
using Squares.Handlers.DI;
using Squares.Machine.DI;
using Squares.Storage.Client.DI;

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
            builder.RegisterModule<HandlersModule>();
            builder.RegisterModule<FileStorageModule>();
            builder.RegisterModule<MachineModule>();

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