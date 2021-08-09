using Moq;
using ObjectsComparer.Interfaces;
using ObjectsComparer.Resolvers;
using ObjectsComparer.Tests.ResolverTests.Fakes;
using System;
using System.Collections.Generic;
using Xunit;

namespace ObjectsComparer.Tests.ResolversTests
{
    public class ListResolverTests
    {
        [Fact]
        public void should_return_comparison_result()
        {
            //Arrange
            var mockedResolver = new Mock<IResolver>();
            mockedResolver.Setup(s => s.Compare(It.IsAny<object>(), It.IsAny<object>())).Returns(new ComparisonResult(false));
            var mockedResolverFinder = new Mock<IResolverFinder>();
            mockedResolverFinder.Setup(s => s.FindResolver(It.IsAny<Type>())).Returns(mockedResolver.Object);
            var listResolver = new ListResolver(mockedResolverFinder.Object, typeof(List<FakeComparableObject>));
            var fakeList = new List<FakeComparableObject>() { new FakeComparableObject(), new FakeComparableObject() };

            //Act
            var comparisonResult = listResolver.Compare(fakeList, fakeList);

            //Assert
            Assert.IsAssignableFrom<IComparisonResult>(comparisonResult);
            Assert.IsType<ComparisonResult>(comparisonResult);
        }

        [Fact]
        public void should_return_is_different_size_lists()
        {
            //Arrange
            var mockedResolver = new Mock<IResolver>();
            mockedResolver.Setup(s => s.Compare(It.IsAny<object>(), It.IsAny<object>())).Returns(new ComparisonResult(false));
            var mockedResolverFinder = new Mock<IResolverFinder>();
            mockedResolverFinder.Setup(s => s.FindResolver(It.IsAny<Type>())).Returns(mockedResolver.Object);
            var listResolver = new ListResolver(mockedResolverFinder.Object, typeof(List<FakeComparableObject>));
            var fakeList1 = new List<FakeComparableObject>() { new FakeComparableObject(), new FakeComparableObject() };
            var fakeList2 = new List<FakeComparableObject>() { new FakeComparableObject() };

            //Act
            var comparisonResult = listResolver.Compare(fakeList1, fakeList2);

            //Assert
            Assert.True(comparisonResult.IsDifferent);
        }

        [Fact]
        public void should_return_is_different_lists()
        {
            //Arrange
            var mockedResolver = new Mock<IResolver>();
            mockedResolver.Setup(s => s.Compare(It.IsAny<object>(), It.IsAny<object>())).Returns(new ComparisonResult(true));
            var mockedResolverFinder = new Mock<IResolverFinder>();
            mockedResolverFinder.Setup(s => s.FindResolver(It.IsAny<Type>())).Returns(mockedResolver.Object);
            var listResolver = new ListResolver(mockedResolverFinder.Object, typeof(List<FakeComparableObject>));
            var fakeList1 = new List<FakeComparableObject>() { new FakeComparableObject(), new FakeComparableObject() };
            var fakeList2 = new List<FakeComparableObject>() { new FakeComparableObject(), new FakeComparableObject() };

            //Act
            var comparisonResult = listResolver.Compare(fakeList1, fakeList2);

            //Assert
            Assert.True(comparisonResult.IsDifferent);
        }

        [Fact]
        public void should_return_is_equal_lists()
        {
            //Arrange
            var mockedResolver = new Mock<IResolver>();
            mockedResolver.Setup(s => s.Compare(It.IsAny<object>(), It.IsAny<object>())).Returns(new ComparisonResult(false));
            var mockedResolverFinder = new Mock<IResolverFinder>();
            mockedResolverFinder.Setup(s => s.FindResolver(It.IsAny<Type>())).Returns(mockedResolver.Object);
            var listResolver = new ListResolver(mockedResolverFinder.Object, typeof(List<FakeComparableObject>));
            var fakeList1 = new List<FakeComparableObject>() { new FakeComparableObject(), new FakeComparableObject() };
            var fakeList2 = new List<FakeComparableObject>() { new FakeComparableObject(), new FakeComparableObject() };

            //Act
            var comparisonResult = listResolver.Compare(fakeList1, fakeList2);

            //Assert
            Assert.False(comparisonResult.IsDifferent);
            mockedResolver.Verify(v => v.Compare(It.IsAny<object>(), It.IsAny<object>()), Times.Exactly(2));
        }
    }
}
