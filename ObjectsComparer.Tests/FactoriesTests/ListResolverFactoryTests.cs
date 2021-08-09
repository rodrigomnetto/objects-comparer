using ObjectsComparer.Factories;
using ObjectsComparer.Interfaces;
using ObjectsComparer.Resolvers;
using ObjectsComparer.Tests.FactoriesTests.Fakes;
using System;
using System.Collections.Generic;
using Xunit;

namespace ObjectsComparer.Tests.FactoriesTests
{
    public class ListResolverFactoryTests
    {
        [Theory]
        [InlineData(typeof(List<>))]
        public void should_return_valid_type(Type type)
        {
            //Arrange
            var factory = new ListResolverFactory();

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
        [InlineData(typeof(Lazy<>))]
        [InlineData(typeof(FakeObject))]
        public void should_return_invalid_type(Type type)
        {
            //Arrange
            var factory = new ListResolverFactory();

            //Act
            var canCreate = factory.CanCreate(type);

            //Assert
            Assert.False(canCreate);
        }

        [Fact]
        public void should_return_list_resolver()
        {
            //Arrange
            var factory = new ListResolverFactory();

            //Act
            var resolver = factory.CreateResolver(typeof(FakeObject), new ResolverFinder(new List<IObjectResolverFactory>(), new List<IValueResolverFactory>()));

            //Assert
            Assert.IsAssignableFrom<IResolver>(resolver);
            Assert.IsType<ListResolver>(resolver);
        }
    }
}
