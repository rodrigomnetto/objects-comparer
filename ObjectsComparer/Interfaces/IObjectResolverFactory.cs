
namespace ObjectsComparer.Interfaces
{
    public interface IObjectResolverFactory : IResolverFactory
    {
        IResolver CreateResolver(IResolverFinder resolverFinder);
    }
}
