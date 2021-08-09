using ObjectsComparer.Interfaces;
using ObjectsComparer.Resolvers;
using System;
using System.Collections;

namespace ObjectsComparer.Factories
{
    public class ListResolverFactory : IObjectResolverFactory
    {
        public bool CanCreate(Type type)
            => typeof(ICollection).IsAssignableFrom(type) && type.IsGenericType;

        public IResolver CreateResolver(Type type, IResolverFinder resolverFinder) 
            => new ListResolver(resolverFinder, type);
    }
}
