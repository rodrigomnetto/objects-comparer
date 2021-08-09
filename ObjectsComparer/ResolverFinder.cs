using ObjectsComparer.Interfaces;
using System;
using System.Collections.Generic;

namespace ObjectsComparer
{
    public class ResolverFinder : IResolverFinder
    {
        IEnumerable<IObjectResolverFactory> _objectResolverFactories;
        IEnumerable<IValueResolverFactory> _valueResolverFactories;

        public ResolverFinder(IEnumerable<IObjectResolverFactory> objectResolverFactories, IEnumerable<IValueResolverFactory> valueResolverFactories)
        {
            _objectResolverFactories = objectResolverFactories;
            _valueResolverFactories = valueResolverFactories;
        }
            
        public IResolver FindResolver(Type type)
        {
            foreach (var factory in _objectResolverFactories)
            {
                if (factory.CanCreate(type))
                    return factory.CreateResolver(type, this);
            }

            foreach (var factory in _valueResolverFactories)
            {
                if (factory.CanCreate(type))
                    return factory.CreateResolver();
            }

            throw new Exception($"Cannot find factory for type {type.Name}");
        }
    }
}