
namespace ObjectsComparer.Interfaces
{
    public interface IResolver
    {
        IComparisonResult Compare(object obj1, object obj2);
    }
}
