using ObjectsComparer.Factories;
using ObjectsComparer.Interfaces;
using ObjectsComparer.Resolvers;
using ObjectsComparer.Tests.FactoriesTests.Fakes;
using System;
using System.Collections.Generic;
using Xunit;

namespace ObjectsComparer.Tests.FactoriesTests
{
    public class ValueResolverFactoryTests
    {
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
        public void should_return_valid_type(Type type)
        {
            //Arrange
            var factory = new ValueResolverFactory();

            //Act
            var canCreate = factory.CanCreate(type);

            //Assert
            Assert.True(canCreate);
        }

        [Theory]
        [InlineData(typeof(List<>))]
        [InlineData(typeof(FakeObject))]
        [InlineData(typeof(Lazy<>))]
        public void should_return_invalid_type(Type type)
        {
            //Arrange
            var factory = new ValueResolverFactory();

            //Act
            var canCreate = factory.CanCreate(type);

            //Assert
            Assert.False(canCreate);
        }

        [Fact]
        public void should_return_value_resolver()
        {
            //Arrange
            var factory = new ValueResolverFactory();

            //Act
            var resolver = factory.CreateResolver();

            //Assert
            Assert.IsAssignableFrom<IResolver>(resolver);
            Assert.IsType<ValueResolver>(resolver);
        }
    }
}
