using ObjectsComparer.Attributes;
using ObjectsComparer.Interfaces;
using ObjectsComparer.Resolvers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ObjectsComparer.Factories
{
    public class ObjectResolverFactory : IObjectResolverFactory
    {
        private static readonly string DEFAULT_COMPARABLE_PROPERTY = "ID";

        protected Type _resolvedType;

        public bool CanCreate(Type type)
        {
            if (type.IsClass && !type.IsGenericType)
            {
                _resolvedType = type;

                return true;
            }

            return false;
        }

        public IResolver CreateResolver(IResolverFinder resolverFinder)
        {
            var comparableProperties = GetObjectComparableProperties(_resolvedType);

            return new ObjectResolver(resolverFinder, comparableProperties);
        }

        private IEnumerable<PropertyInfo> GetObjectComparableProperties(Type type)
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
