
namespace ObjectsComparer.Interfaces
{
    public interface IValueResolverFactory : IResolverFactory
    {
        IResolver CreateResolver();
    }
}
