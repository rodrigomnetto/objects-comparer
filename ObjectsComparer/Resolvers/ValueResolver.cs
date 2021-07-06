using ObjectsComparer.Interfaces;

namespace ObjectsComparer.Resolvers
{
    public class ValueResolver : IResolver
    {
        public IComparisonResult Compare(object object1, object object2)
            => new ComparisonResult(!object1.Equals(object2));
    }
}
