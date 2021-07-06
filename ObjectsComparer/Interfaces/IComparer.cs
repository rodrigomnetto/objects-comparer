
namespace ObjectsComparer.Interfaces
{
    public interface IComparer
    {
        bool IsDifferent(object object1, object object2);

        IComparisonResult GetDifferences(object object1, object object2);
    }
}
