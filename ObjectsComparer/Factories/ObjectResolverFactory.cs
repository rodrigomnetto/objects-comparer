using ObjectsComparer.Attributes;
using ObjectsComparer.Interfaces;
using ObjectsComparer.Resolvers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ObjectsComparer.Factories
{
    public class ObjectResolverFactory : IObjectResolverFactory
    {
        private static readonly string DEFAULT_COMPARABLE_PROPERTY = "ID";

        public bool CanCreate(Type type) => type.IsClass 
                                         && !typeof(ICollection).IsAssignableFrom(type) 
                                         && !type.IsGenericType
                                         && typeof(string) != type;

        public IResolver CreateResolver(Type type, IResolverFinder resolverFinder)
        {
            var comparableProperties = GetObjectComparableProperties(type);

            return new ObjectResolver(resolverFinder, comparableProperties);
        }

        private IEnumerable<PropertyInfo> GetObjectComparableProperties(Type type) //transformar em dependencia injetada, não testar detalhes
        {
            var comparableProps = type.GetProperties().Where(w => w.GetCustomAttributes(true).Where(w => w.GetType().Equals(typeof(Comparable))).Any());

            if (!comparableProps.Any())
                comparableProps = type.GetProperties().Where(w => w.Name == DEFAULT_COMPARABLE_PROPERTY);

            if (!comparableProps.Any())
                throw new Exception("Comparable properties not found.");

            return comparableProps;
        }
    }
}
