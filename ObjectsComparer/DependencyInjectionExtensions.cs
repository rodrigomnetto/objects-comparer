using Microsoft.Extensions.DependencyInjection;
using ObjectsComparer.Factories;
using ObjectsComparer.Interfaces;

namespace ObjectsComparer
{
    public static class DependencyInjectionExtensions
    {
        public static void RegisterObjectsComparer(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IComparer, Comparer>();
            serviceCollection.AddTransient<IResolverFinder, ResolverFinder>();
            serviceCollection.AddTransient<IObjectResolverFactory, ObjectResolverFactory>();
            serviceCollection.AddTransient<IObjectResolverFactory, ListResolverFactory>();
            serviceCollection.AddTransient<IValueResolverFactory, ValueResolverFactory>();
        }
    }
}
