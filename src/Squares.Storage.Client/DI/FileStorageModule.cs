using Autofac;

namespace Squares.Storage.Client.DI
{
    public class FileStorageModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FileStorage>()
                .As<IStorage>()
                .PropertiesAutowired()
                .SingleInstance();
        }
    }
}