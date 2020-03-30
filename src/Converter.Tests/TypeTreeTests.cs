using System;
using System.Collections.Generic;
using System.Linq;
using Converter.Extraction;
using Xunit;

namespace Converter.Tests
{
    public class TypeTreeTests
    {
        [Theory]
        [InlineData(typeof(int))]
        [InlineData(typeof(List<int>))]
        [InlineData(typeof(Dictionary<HashSet<double>, ICollection<double>>))]
        public void From_AnyType_RootTreeTypeShouldMatch(Type type)
        {
            // Act
            var tree = TypeTree.From(type);

            // Assert
            Assert.Equal(type, tree.Type);
        }
        
        [Theory]
        [InlineData(typeof(int))]
        [InlineData(typeof(double))]
        [InlineData(typeof(bool))]
        public void From_BuiltInType_TreeShouldNotHaveGenericArguments(Type simpleType)
        {
            // Act
            var tree = TypeTree.From(simpleType);

            // Assert
            Assert.Null(tree.GenericArguments);
        }

        [Fact]
        public void From_ClassWithNoImplementedInterfaces_TreeShouldNotHaveAny()
        {
            // Act
            var tree = TypeTree.From(typeof(NonGenericNoInterfacesClass));

            // Assert
            Assert.Null(tree.DirectImplementedInterfaces);
        }

        [Fact]
        public void From_ClassWithNoGenericArguments_TreeShouldNotHaveAny()
        {
            // Act
            var tree = TypeTree.From(typeof(NonGenericNoInterfacesClass));

            // Assert
            Assert.Null(tree.GenericArguments);
        }

        [Fact]
        public void From_TypeWithDirectInterfaces_TreeShouldContainThem()
        {
            // Arrange
            var expectedInterfaces = new[] {typeof(IFoo), typeof(IBar)}; 

            // Act
            var tree = TypeTree.From(typeof(ClassWithInterfaces));

            // Assert
            Assert.NotNull(tree.DirectImplementedInterfaces);
            Assert.NotEmpty(tree.DirectImplementedInterfaces);
            Assert.Equal(expectedInterfaces, tree.DirectImplementedInterfaces.Select(t => t.Type));
        }

        [Fact]
        public void From_TypeWithInterfaceInheritance_TreeShouldReflectIt()
        {
            // Arrange
            var expectedNestedTreeInterfaces = new[] {typeof(IFoo), typeof(IBar)};

            // Act
            var tree = TypeTree.From(typeof(ClassWithInheritedInterfaces));

            // Assert
            Assert.Single(tree.DirectImplementedInterfaces);
            var nestedTree = tree.DirectImplementedInterfaces.First();
            
            Assert.Equal(typeof(IFooBar), nestedTree.Type);
            Assert.NotNull(nestedTree.DirectImplementedInterfaces);
            Assert.Equal(expectedNestedTreeInterfaces, nestedTree.DirectImplementedInterfaces.Select(t => t.Type));
            
        }
        
        [Fact]
        public void From_TypeWithSingleGenericArgument_TreeShouldContainIt()
        {
            // Arrange
            var argumentType = typeof(int);
            
            var genericClass = CreateGeneric<OneGenericArgumentClass<int>>(typeof(OneGenericArgumentClass<>), typeof(int));

            // Act
            var tree = TypeTree.From(genericClass.GetType());

            // Assert
            Assert.NotNull(tree.GenericArguments);
            Assert.Single(tree.GenericArguments);
            Assert.Equal(argumentType, tree.GenericArguments.First().Type);
        }

        [Fact]
        public void From_TypeWithNestedGenericArguments_TreeShouldReflectIt()
        {
            // Arrange
            var nestedArgumentType = typeof(int);

            var genericClass = CreateGeneric<OneGenericArgumentClass<List<int>>>(typeof(OneGenericArgumentClass<>), typeof(List<int>));
            
            // Act
            var tree = TypeTree.From(genericClass.GetType());
            
            // Assert
            Assert.NotNull(tree.GenericArguments);
            Assert.Single(tree.GenericArguments);
            
            var genericArgument = tree.GenericArguments.First();
            
            Assert.NotNull(genericArgument.GenericArguments);
            Assert.Single(genericArgument.GenericArguments);

            var nestedGenericArgument = genericArgument.GenericArguments.First();
            
            Assert.Equal(nestedArgumentType, nestedGenericArgument.Type);
            Assert.Null(nestedGenericArgument.GenericArguments);
        }
        
        private static T CreateGeneric<T>(Type genericType, Type argumentType)
        {
            var genericClassType = genericType.MakeGenericType(argumentType);
            return (T)Activator.CreateInstance(genericClassType);
        }

        private class NonGenericNoInterfacesClass
        {
        }

        private class ClassWithInterfaces : IFoo, IBar
        {
        }

        private class ClassWithInheritedInterfaces : IFooBar
        {
        }

        private class OneGenericArgumentClass<TArg>
        {
        }
        
        private interface IFoo
        {
        }
        
        private interface IBar
        {
        }
        
        private interface IFooBar : IFoo, IBar
        {
        }
    }
}
