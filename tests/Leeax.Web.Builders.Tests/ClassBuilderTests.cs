using Xunit;

namespace Leeax.Web.Builders.Tests
{
    public class ClassBuilderTests
    {
        private const string CLASS_DUMMY_1 = "test-1";
        private const string CLASS_DUMMY_2 = "test-2";
        private const string CLASS_DUMMY_3 = "test-3";

        #region ClassBuilder.Create() [static]
        [Fact]
        public void CreateEmptyBuilder()
        {
            var builder = ClassBuilder.Create();
            Assert.True(builder.Build() == null);
        }

        [Fact]
        public void CreateBuilderWithClasses()
        {
            var builder = ClassBuilder.Create(CLASS_DUMMY_1, CLASS_DUMMY_2, CLASS_DUMMY_3);
            Assert.True(builder.Build() == string.Join(" ", CLASS_DUMMY_1, CLASS_DUMMY_2, CLASS_DUMMY_3));
        }

        [Fact]
        public void CreateBuilderWithEmptyValues()
        {
            // If only empty values are added, the builder should return null
            var builder = ClassBuilder.Create("", "  ");
            Assert.True(builder.Build() == null);

            // Test with null value
            builder = ClassBuilder.Create(null);
            Assert.True(builder.Build() == null);

            // Test with multiple null values
            builder = ClassBuilder.Create(null, CLASS_DUMMY_1, null, CLASS_DUMMY_2, CLASS_DUMMY_3, null);
            Assert.True(builder.Build() == string.Join(" ", CLASS_DUMMY_1, CLASS_DUMMY_2, CLASS_DUMMY_3));
        }
        #endregion

        #region ClassBuilder.Merge() [static]
        [Fact]
        public void MergeZeroBuilders()
        {
            var builder = ClassBuilder.Merge();

            Assert.True(builder.Build() == null);
        }

        [Fact]
        public void MergeMultipleBuilders()
        {
            var builder1 = ClassBuilder.Create();
            var builder2 = ClassBuilder.Create(CLASS_DUMMY_1, CLASS_DUMMY_2);
            var builder3 = ClassBuilder.Create(CLASS_DUMMY_3);
            var builder4 = ClassBuilder.Create();

            // Merge all builders
            var builder5 = ClassBuilder.Merge(builder1, builder2, null, builder3, builder4);

            Assert.True(builder5.Build() == string.Join(" ", CLASS_DUMMY_1, CLASS_DUMMY_2, CLASS_DUMMY_3));
        }
        #endregion

        #region ClassBuilder.Add() [instance]
        [Theory]
        [InlineData(true, CLASS_DUMMY_1)]
        [InlineData(false, CLASS_DUMMY_2)]
        public void AddConditional(bool condition, string expectedString)
        {
            var builder = ClassBuilder.Create();

            builder.Add(CLASS_DUMMY_1, condition);
            builder.Add(CLASS_DUMMY_2, !condition);

            Assert.True(builder.Build() == expectedString);
        }

        [Theory]
        [InlineData(true, CLASS_DUMMY_1 + " " + CLASS_DUMMY_2)]
        [InlineData(false, CLASS_DUMMY_2 + " " + CLASS_DUMMY_1)]
        public void AddConditionalExtended(bool condition, string expectedString)
        {
            var builder = ClassBuilder.Create();

            builder.Add(CLASS_DUMMY_1, CLASS_DUMMY_2, condition);
            builder.Add(CLASS_DUMMY_1, CLASS_DUMMY_2, !condition);

            Assert.True(builder.Build() == expectedString);
        }

        [Theory]
        [InlineData(true, CLASS_DUMMY_1)]
        [InlineData(false, null)]
        public void AddConditionalWithFactory(bool condition, string expectedString)
        {
            var factoryEvaluated = false;
            var builder = ClassBuilder.Create();

            builder.Add(
                () =>
                {
                    factoryEvaluated = true;
                    return CLASS_DUMMY_1;
                }, 
                condition);

            Assert.True(builder.Build() == expectedString);
            Assert.True(factoryEvaluated == condition);
        }
        #endregion

        #region ClassBuilder.AddMultiple() [instance]
        [Theory]
        [InlineData(true, CLASS_DUMMY_1 + " " + CLASS_DUMMY_2)]
        [InlineData(false, null)]
        public void AddMultipleConditional(bool condition, string expectedString)
        {
            var builder = ClassBuilder.Create();

            builder.AddMultiple(new string[] { CLASS_DUMMY_1, CLASS_DUMMY_2 }, condition);

            Assert.True(builder.Build() == expectedString);
        }

        [Theory]
        [InlineData(true, CLASS_DUMMY_1 + " " + CLASS_DUMMY_2)]
        [InlineData(false, null)]
        public void AddMultipleWithFactory(bool condition, string expectedString)
        {
            var factoryEvaluated = false;
            var builder = ClassBuilder.Create();

            builder.AddMultiple(
                () =>
                {
                    factoryEvaluated = true;
                    return new string[] { CLASS_DUMMY_1, CLASS_DUMMY_2 };
                },
                condition);

            Assert.True(builder.Build() == expectedString);
            Assert.True(factoryEvaluated == condition);
        }
        #endregion

        #region ClassBuilder.Clear() [instance]
        [Fact]
        public void ClearBuilder()
        {
            // Add some classes to the builder
            var builder = ClassBuilder.Create(CLASS_DUMMY_1, CLASS_DUMMY_2, CLASS_DUMMY_3);

            Assert.True(builder.Build() == string.Join(" ", CLASS_DUMMY_1, CLASS_DUMMY_2, CLASS_DUMMY_3));

            // Clear the builder again
            builder.Clear();

            Assert.True(builder.Build() == null);
        }
        #endregion

        #region ClassBuilder.Merge() [instance]
        [Theory]
        [InlineData(true, CLASS_DUMMY_1 + " " + CLASS_DUMMY_2 + " " + CLASS_DUMMY_3)]
        [InlineData(false, CLASS_DUMMY_1)]
        public void MergeConditionalBuilder(bool condition, string expectedString)
        {
            var builder = ClassBuilder.Create(CLASS_DUMMY_1);
            var builderToMerge = ClassBuilder.Create(CLASS_DUMMY_2, CLASS_DUMMY_3);

            // Merge first builder with the second one
            builder.Merge(builderToMerge, condition);

            Assert.True(builder.Build() == expectedString);
        }

        [Theory]
        [InlineData(true, CLASS_DUMMY_1 + " " + CLASS_DUMMY_2 + " " + CLASS_DUMMY_3)]
        [InlineData(false, CLASS_DUMMY_1)]
        public void MergeConditionalBuilderFactory(bool condition, string expectedString)
        {
            var factoryEvaluated = false;
            var builder = ClassBuilder.Create(CLASS_DUMMY_1);

            // Merge first builder with the second one
            builder.Merge(
                () =>
                {
                    factoryEvaluated = true;
                    return ClassBuilder.Create(CLASS_DUMMY_2, CLASS_DUMMY_3);
                },
                condition);

            Assert.True(builder.Build() == expectedString);
            Assert.True(factoryEvaluated == condition);
        }
        #endregion
    }
}