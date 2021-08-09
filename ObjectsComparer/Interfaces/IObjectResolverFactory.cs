
using System;

namespace ObjectsComparer.Interfaces
{
    public interface IObjectResolverFactory : IResolverFactory
    {
        IResolver CreateResolver(Type type, IResolverFinder resolverFinder);
    }
}
