using ObjectsComparer.Factories;
using ObjectsComparer.Interfaces;
using ObjectsComparer.Resolvers;
using ObjectsComparer.Tests.FactoriesTests.Fakes;
using System;
using System.Collections.Generic;
using Xunit;

namespace ObjectsComparer.Tests.FactoriesTests
{
    public class ObjectResolverFactoryTests
    {
        [Theory]
        [InlineData(typeof(FakeObject))]
        public void should_return_valid_type(Type type)
        {
            //Arrange
            var factory = new ObjectResolverFactory();

            //Act
            var canCreate = factory.CanCreate(type);

            //Assert
            Assert.True(canCreate);
        }

        [Theory]
        [InlineData(typeof(string))]
        [InlineData(typeof(char))]
        [InlineData(typeof(byte))]
        [InlineData(typeof(sbyte))]
        [InlineData(typeof(ushort))]
        [InlineData(typeof(short))]
        [InlineData(typeof(uint))]
        [InlineData(typeof(int))]
        [InlineData(typeof(ulong))]
        [InlineData(typeof(long))]
        [InlineData(typeof(float))]
        [InlineData(typeof(double))]
        [InlineData(typeof(decimal))]
        [InlineData(typeof(bool))]
        [InlineData(typeof(List<>))]
        [InlineData(typeof(Lazy<>))]
        public void should_return_invalid_type(Type type)
        {
            //Arrange
            var factory = new ObjectResolverFactory();

            //Act
            var canCreate = factory.CanCreate(type);

            //Assert
            Assert.False(canCreate);
        }

        [Fact]
        public void should_return_object_resolver_when_type_have_comparable_properties()
        {
            //Arrange
            var factory = new ObjectResolverFactory();

            //Act
            var resolver = factory.CreateResolver(typeof(FakeComparableObject), new ResolverFinder(new List<IObjectResolverFactory>(), new List<IValueResolverFactory>()));

            //Assert
            Assert.IsAssignableFrom<IResolver>(resolver);
            Assert.IsType<ObjectResolver>(resolver);
        }

        [Fact]
        public void should_return_object_resolver_when_type_have_id_property_only()
        {
            //Arrange
            var factory = new ObjectResolverFactory();

            //Act
            var resolver = factory.CreateResolver(typeof(FakeIDComparableObject), new ResolverFinder(new List<IObjectResolverFactory>(), new List<IValueResolverFactory>()));

            //Assert
            Assert.IsAssignableFrom<IResolver>(resolver);
            Assert.IsType<ObjectResolver>(resolver);
        }

        [Fact]
        public void should_throw_exception_when_object_does_not_have_comparable_properties()
        {
            //Arrange
            var factory = new ObjectResolverFactory();

            //Act
            void action() => factory.CreateResolver(typeof(FakeObject), new ResolverFinder(new List<IObjectResolverFactory>(), new List<IValueResolverFactory>()));

            //Assert
            Assert.Throws<Exception>(action);
        }
    }
}
