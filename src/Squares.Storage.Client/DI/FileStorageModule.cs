using System.Configuration;
using Autofac;
using Squares.Contracts.Points;
using Squares.Contracts.Squares;

namespace Squares.Storage.Client.DI
{
    public class FileStorageModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new FileStorage<Square>(ConfigurationManager.AppSettings["SquaresLocation"]))
                .As<IStorage<Square>>()
                .SingleInstance();

            builder.Register(c => new FileStorage<Point>(ConfigurationManager.AppSettings["PointsLocation"]))
                .As<IStorage<Point>>()
                .SingleInstance();

            builder.Register(c => new FileStorage<Point>(ConfigurationManager.AppSettings["FreePointsLocation"]))
                .Named<IStorage<Point>>("FreePoints")
                .SingleInstance();
        }
    }
}