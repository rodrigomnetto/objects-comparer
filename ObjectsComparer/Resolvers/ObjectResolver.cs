using ObjectsComparer.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ObjectsComparer.Resolvers
{
    public class ObjectResolver : AbstractObjectResolver
    {
        IEnumerable<PropertyInfo> _comparableProperties;

        public ObjectResolver(IResolverFinder resolverFinder, IEnumerable<PropertyInfo> comparableProperties) : base(resolverFinder) 
            => _comparableProperties = comparableProperties;        
        
        public override IComparisonResult Compare(object object1, object object2)
        {
            var differentProperties = GetDifferentProperties(object1, object2);

            return new ComparisonResult(differentProperties.Any(), differentProperties);
        }

        private IEnumerable<string> GetDifferentProperties(object object1, object object2)
        {
            foreach (var property in _comparableProperties)
            {
                if (_resolverFinder.FindResolver(property.PropertyType).Compare(property.GetValue(object1), property.GetValue(object2)).IsDifferent)
                    yield return property.Name;
            }
        }
    }
}
