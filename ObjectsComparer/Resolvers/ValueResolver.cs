using ObjectsComparer.Interfaces;

namespace ObjectsComparer.Resolvers
{
    public class ValueResolver : IResolver
    {
        public IComparisonResult Compare(object object1, object object2)
        {
            bool isDifferent;

            if (object1 == null && object2 == null)
                isDifferent = false;
            else if (object1 == null && object2 != null)
                isDifferent = true;
            else
                isDifferent = !object1.Equals(object2);

            return new ComparisonResult(isDifferent);
        }
    }
}
