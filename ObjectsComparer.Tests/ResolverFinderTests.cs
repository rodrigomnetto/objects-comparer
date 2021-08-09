using Moq;
using ObjectsComparer.Interfaces;
using ObjectsComparer.Resolvers;
using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit;

namespace ObjectsComparer.Tests
{
    public class ResolverFinderTests
    {
        [Fact]
        public void should_return_object_resolver()
        {
            //Arrange
            var mockedObjectResolverFactory = new Mock<IObjectResolverFactory>();
            var mockedValueResolverFactory = new Mock<IValueResolverFactory>();
            var resolverFinder = CreateDefaultResolverFinder(mockedObjectResolverFactory.Object, mockedValueResolverFactory.Object);
            mockedObjectResolverFactory.Setup(s => s.CanCreate(It.IsAny<Type>())).Returns(true);
            mockedValueResolverFactory.Setup(s => s.CanCreate(It.IsAny<Type>())).Returns(false);
            mockedObjectResolverFactory.Setup(s => s.CreateResolver(It.IsAny<Type>(), resolverFinder)).Returns(new ObjectResolver(resolverFinder, new List<PropertyInfo>()));
            mockedValueResolverFactory.Setup(s => s.CreateResolver()).Returns(new ValueResolver());

            //Act
            var resolver = resolverFinder.FindResolver(It.IsAny<Type>());

            //Assert
            Assert.IsType<ObjectResolver>(resolver);
        }

        [Fact]
        public void should_return_value_resolver()
        {
            //Arrange
            var mockedObjectResolverFactory = new Mock<IObjectResolverFactory>();
            var mockedValueResolverFactory = new Mock<IValueResolverFactory>();
            var resolverFinder = CreateDefaultResolverFinder(mockedObjectResolverFactory.Object, mockedValueResolverFactory.Object);
            mockedObjectResolverFactory.Setup(s => s.CanCreate(It.IsAny<Type>())).Returns(false);
            mockedValueResolverFactory.Setup(s => s.CanCreate(It.IsAny<Type>())).Returns(true);
            mockedObjectResolverFactory.Setup(s => s.CreateResolver(It.IsAny<Type>(), resolverFinder)).Returns(new ObjectResolver(resolverFinder, new List<PropertyInfo>()));
            mockedValueResolverFactory.Setup(s => s.CreateResolver()).Returns(new ValueResolver());

            //Act
            var resolver = resolverFinder.FindResolver(It.IsAny<Type>());

            //Assert
            Assert.IsType<ValueResolver>(resolver);
        }

        [Fact]
        public void should_throw_exception_when_factory_not_found()
        {
            //Arrange
            var mockedObjectResolverFactory = new Mock<IObjectResolverFactory>();
            var mockedValueResolverFactory = new Mock<IValueResolverFactory>();
            var resolverFinder = CreateDefaultResolverFinder(mockedObjectResolverFactory.Object, mockedValueResolverFactory.Object);
            mockedObjectResolverFactory.Setup(s => s.CanCreate(It.IsAny<Type>())).Returns(false);
            mockedValueResolverFactory.Setup(s => s.CanCreate(It.IsAny<Type>())).Returns(false);

            //Act
            Action action = () => resolverFinder.FindResolver(typeof(int));

            //Assert
            Assert.Throws<Exception>(action);
        }

        private IResolverFinder CreateDefaultResolverFinder(IObjectResolverFactory objectResolverFactory, IValueResolverFactory valueResolverFactory)
        {
            var objectResolverFactories = new List<IObjectResolverFactory>() { objectResolverFactory };
            var valueResolverFactories = new List<IValueResolverFactory>() { valueResolverFactory };
            return new ResolverFinder(objectResolverFactories, valueResolverFactories);
        }
    }
}
