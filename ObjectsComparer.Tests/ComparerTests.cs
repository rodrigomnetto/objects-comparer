using Moq;
using ObjectsComparer.Interfaces;
using System;
using Xunit;

namespace ObjectsComparer.Tests
{
    public class ComparerTests
    {
        private readonly IComparer _comparer;
        private readonly Mock<IResolverFinder> _mockedResolverFinder;

        public ComparerTests()
        {
            _mockedResolverFinder = new Mock<IResolverFinder>(); //fazer um CreateDefault ao inves de deixar no construtor
            _comparer = new Comparer(_mockedResolverFinder.Object);
        }

        [Fact]
        public void should_throw_exception_when_different_types_are_compared()
        {
            //Assert
            Assert.Throws<ArgumentException>(() => _comparer.GetDifferences(1, ""));
        }

        [Fact]
        public void should_return_is_different()
        {
            //Arrange
            var mockedResolver = new Mock<IResolver>();
            mockedResolver.Setup(x => x.Compare(It.IsAny<string>(), It.IsAny<string>())).Returns(new ComparisonResult(true));
            _mockedResolverFinder.Setup(x => x.FindResolver(It.IsAny<Type>())).Returns(mockedResolver.Object);

            //Act
            var result = _comparer.IsDifferent("1", "2");

            //Assert
            Assert.IsType<bool>(result);
            Assert.True(result);
        }

        [Fact]
        public void should_return_is_not_different()
        {
            //Arrange
            var mockedResolver = new Mock<IResolver>();
            mockedResolver.Setup(x => x.Compare(It.IsAny<string>(), It.IsAny<string>())).Returns(new ComparisonResult(false));
            _mockedResolverFinder.Setup(x => x.FindResolver(It.IsAny<Type>())).Returns(mockedResolver.Object);

            //Act
            var result = _comparer.IsDifferent("1", "1");

            //Assert
            _mockedResolverFinder.Verify(m => m.FindResolver(It.IsAny<Type>()), Times.Once);
            Assert.IsType<bool>(result);
            Assert.False(result);
        }

        [Fact]
        public void should_return_different_comparison_result()
        {
            //Arrange
            var mockedResolver = new Mock<IResolver>();
            mockedResolver.Setup(x => x.Compare(It.IsAny<string>(), It.IsAny<string>())).Returns(new ComparisonResult(true));
            _mockedResolverFinder.Setup(x => x.FindResolver(It.IsAny<Type>())).Returns(mockedResolver.Object);

            //Act
            var result = _comparer.GetDifferences("1", "1");

            //Assert
            _mockedResolverFinder.Verify(m => m.FindResolver(It.IsAny<Type>()), Times.Once);
            Assert.IsAssignableFrom<IComparisonResult>(result);
            Assert.True(result.IsDifferent);
        }

        [Fact]
        public void should_return_equal_comparison_result()
        {
            //Arrange
            var mockedResolver = new Mock<IResolver>();
            mockedResolver.Setup(x => x.Compare(It.IsAny<string>(), It.IsAny<string>())).Returns(new ComparisonResult(false));
            _mockedResolverFinder.Setup(x => x.FindResolver(It.IsAny<Type>())).Returns(mockedResolver.Object);

            //Act
            var result = _comparer.GetDifferences("1", "1");

            //Assert
            _mockedResolverFinder.Verify(m => m.FindResolver(It.IsAny<Type>()), Times.Once);
            Assert.IsAssignableFrom<IComparisonResult>(result);
            Assert.False(result.IsDifferent);
        }
    }
}
