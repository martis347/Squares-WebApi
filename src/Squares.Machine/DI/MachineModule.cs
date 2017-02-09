using Autofac;
using Squares.Contracts;
using Squares.Contracts.Points;
using Squares.Contracts.Squares;
using Squares.Storage.Client;

namespace Squares.Machine.DI
{
    public class MachineModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register((c, p) =>
                new Machine(
                    c.Resolve<IStorage<Square>>(),
                    c.ResolveNamed<IStorage<Point>>("FreePoints")))
                .As<IMachine<BaseRequest>>()
                .SingleInstance();
        }
    }
}
