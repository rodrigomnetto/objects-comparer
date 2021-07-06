using System;

namespace ObjectsComparer.Interfaces
{
    public interface IResolverFinder
    {
        IResolver FindResolver(Type type);
    }
}
