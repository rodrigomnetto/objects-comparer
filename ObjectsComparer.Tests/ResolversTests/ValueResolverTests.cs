using ObjectsComparer.Interfaces;
using ObjectsComparer.Resolvers;
using Xunit;

namespace ObjectsComparer.Tests.ResolversTests
{
    public class ValueResolverTests
    {
        [Fact]
        public void should_return_comparison_result()
        {
            //Arrange
            var fakeValue1 = "1";
            var fakeValue2 = "2";
            var valueResolver = new ValueResolver();

            //Act
            var comparisonResult = valueResolver.Compare(fakeValue1, fakeValue2);

            //Assert
            Assert.IsAssignableFrom<IComparisonResult>(comparisonResult);
            Assert.IsType<ComparisonResult>(comparisonResult);
        }

        [Theory]
        [InlineData(null, 1)]
        [InlineData(1, null)]
        [InlineData(2, 1)]
        [InlineData("1", "2")]
        [InlineData(1.1, 1.2)]
        [InlineData(false, true)]
        [InlineData('a', 'A')]
        public void should_return_different_objects(object object1, object object2)
        {
            //Arrange
            var valueResolver = new ValueResolver();

            //Act
            var comparisonResult = valueResolver.Compare(object1, object2);

            //Assert
            Assert.True(comparisonResult.IsDifferent);
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(null, null)]
        [InlineData(false, false)]
        [InlineData("1", "1")]
        [InlineData(1.1, 1.1)]
        [InlineData('A', 'A')]
        public void should_return_equal_objects(object object1, object object2)
        {
            //Arrange
            var valueResolver = new ValueResolver();

            //Act
            var comparisonResult = valueResolver.Compare(object1, object2);

            //Assert
            Assert.False(comparisonResult.IsDifferent);
        }
    }
}
