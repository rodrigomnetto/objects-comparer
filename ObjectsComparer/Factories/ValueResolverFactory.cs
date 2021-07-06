using ObjectsComparer.Interfaces;
using ObjectsComparer.Resolvers;
using System;

namespace ObjectsComparer.Factories
{
    public class ValueResolverFactory : IValueResolverFactory
    {
        public bool CanCreate(Type type)
            => type.IsPrimitive || type == typeof(string) || type == typeof(decimal);

        public IResolver CreateResolver() => new ValueResolver();  
    }
}
