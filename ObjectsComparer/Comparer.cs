using ObjectsComparer.Interfaces;
using System;

namespace ObjectsComparer
{
    public class Comparer : IComparer
    {
        private IResolverFinder _resolverFinder;

        public Comparer(IResolverFinder resolverFinder)
            => _resolverFinder = resolverFinder;

        public bool IsDifferent(object object1, object object2) => GetDifferences(object1, object2).IsDifferent;

        public IComparisonResult GetDifferences(object object1, object object2)
        {
            if (object1.GetType() != object2.GetType())
                throw new Exception("Cannot compare different type objects.");

            return _resolverFinder.FindResolver(object1.GetType()).Compare(object1, object2);
        }
    }
}
