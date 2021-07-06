using ObjectsComparer.Interfaces;

namespace ObjectsComparer.Resolvers
{
    public abstract class AbstractObjectResolver : IResolver
    {
        protected readonly IResolverFinder _resolverFinder;

        protected AbstractObjectResolver(IResolverFinder resolverFinder)
            => _resolverFinder = resolverFinder;

        public abstract IComparisonResult Compare(object obj1, object obj2);
    }
}
