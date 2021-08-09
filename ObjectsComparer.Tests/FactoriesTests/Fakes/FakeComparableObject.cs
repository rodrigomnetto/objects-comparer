using ObjectsComparer.Attributes;

namespace ObjectsComparer.Tests.FactoriesTests.Fakes
{
    public class FakeComparableObject
    {
        [Comparable]
        public string TestProperty { get; set;}
    }
}
