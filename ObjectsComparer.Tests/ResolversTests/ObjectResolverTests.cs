using Moq;
using ObjectsComparer.Interfaces;
using ObjectsComparer.Resolvers;
using ObjectsComparer.Tests.ResolverTests.Fakes;
using System;
using System.Linq;
using Xunit;

namespace ObjectsComparer.Tests.ResolversTests
{
    public class ObjectResolverTests
    {
        [Fact]
        public void should_return_comparison_result()
        {
            //Arrange
            var mockedResolver = new Mock<IResolver>();
            mockedResolver.Setup(s => s.Compare(It.IsAny<object>(), It.IsAny<object>())).Returns(new ComparisonResult(false));
            var mockedResolverFinder = new Mock<IResolverFinder>();
            mockedResolverFinder.Setup(s => s.FindResolver(It.IsAny<Type>())).Returns(mockedResolver.Object);
            var objectResolver = new ObjectResolver(mockedResolverFinder.Object, typeof(FakeComparableObject).GetProperties());
            var fakeComparableObject = new FakeComparableObject();

            //Act
            var comparisonResult = objectResolver.Compare(fakeComparableObject, fakeComparableObject);

            //Assert
            Assert.IsAssignableFrom<IComparisonResult>(comparisonResult);
            Assert.IsType<ComparisonResult>(comparisonResult);
        }


        [Fact]
        public void should_return_equal_objects()
        {
            //Arrange
            var mockedResolver = new Mock<IResolver>();
            mockedResolver.Setup(s => s.Compare(It.IsAny<object>(), It.IsAny<object>())).Returns(new ComparisonResult(false));
            var mockedResolverFinder = new Mock<IResolverFinder>();
            mockedResolverFinder.Setup(s => s.FindResolver(It.IsAny<Type>())).Returns(mockedResolver.Object);
            var objectResolver = new ObjectResolver(mockedResolverFinder.Object, typeof(FakeComparableObject).GetProperties());
            var fakeComparableObject = new FakeComparableObject();

            //Act
            var comparisonResult = objectResolver.Compare(fakeComparableObject, fakeComparableObject);

            //Assert
            Assert.False(comparisonResult.IsDifferent);
        }

        [Fact]
        public void should_return_different_objects()
        {
            //Arrange
            var mockedResolver = new Mock<IResolver>();
            mockedResolver.SetupSequence(s => s.Compare(It.IsAny<object>(), It.IsAny<object>()))
                          .Returns(new ComparisonResult(false))
                          .Returns(new ComparisonResult(true));
            var mockedResolverFinder = new Mock<IResolverFinder>();
            mockedResolverFinder.Setup(s => s.FindResolver(It.IsAny<Type>())).Returns(mockedResolver.Object);
            var objectResolver = new ObjectResolver(mockedResolverFinder.Object, typeof(FakeComparableObject).GetProperties());
            var fakeComparableObject = new FakeComparableObject();

            //Act
            var comparisonResult = objectResolver.Compare(fakeComparableObject, fakeComparableObject);

            //Assert
            Assert.True(comparisonResult.IsDifferent);
        }

        [Fact]
        public void should_compare_object_properties_twice_times()
        {
            //Arrange
            var mockedResolver = new Mock<IResolver>();
            mockedResolver.Setup(s => s.Compare(It.IsAny<object>(), It.IsAny<object>())).Returns(new ComparisonResult(false));
            var mockedResolverFinder = new Mock<IResolverFinder>();
            mockedResolverFinder.Setup(s => s.FindResolver(It.IsAny<Type>())).Returns(mockedResolver.Object);
            var objectResolver = new ObjectResolver(mockedResolverFinder.Object, typeof(FakeComparableObject).GetProperties());
            var fakeComparableObject = new FakeComparableObject();

            //Act
            var comparisonResult = objectResolver.Compare(fakeComparableObject, fakeComparableObject);

            //Assert
            mockedResolver.Verify(v => v.Compare(It.IsAny<object>(), It.IsAny<object>()), Times.Exactly(2));
        }

        [Fact]
        public void should_return_list_of_different_properties()
        {
            //Arrange
            var mockedResolver = new Mock<IResolver>();
            mockedResolver.Setup(s => s.Compare(It.IsAny<object>(), It.IsAny<object>())).Returns(new ComparisonResult(true));
            var mockedResolverFinder = new Mock<IResolverFinder>();
            mockedResolverFinder.Setup(s => s.FindResolver(It.IsAny<Type>())).Returns(mockedResolver.Object);
            var objectResolver = new ObjectResolver(mockedResolverFinder.Object, typeof(FakeComparableObject).GetProperties());
            var fakeComparableObject = new FakeComparableObject();

            //Act
            var comparisonResult = objectResolver.Compare(fakeComparableObject, fakeComparableObject);

            //Assert
            Assert.Equal(nameof(fakeComparableObject.TestProperty), comparisonResult.DifferentProperties.ElementAt(0));
            Assert.Equal(nameof(fakeComparableObject.TestProperty2), comparisonResult.DifferentProperties.ElementAt(1));
        }
    }
}
