using System;

namespace ObjectsComparer.Interfaces
{
    public interface IResolverFactory
    {
        bool CanCreate(Type type);
    }
}
