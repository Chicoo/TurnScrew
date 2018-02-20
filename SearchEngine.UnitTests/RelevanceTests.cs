
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace TurnScrew.Wiki.SearchEngine.UnitTests
{


    public class RelevanceTests
    {

        [Fact]
        public void Constructor()
        {
            Relevance rel = new Relevance();
            Assert.Equal(0, rel.Value);
            Assert.False(rel.IsFinalized, "Value should not be finalized");
        }

        [Fact]
        public void Constructor_WithValue()
        {
            Relevance rel = new Relevance(5);
            Assert.Equal(5, rel.Value);
            Assert.False(rel.IsFinalized, "Value should not be finalized");
        }

        [Fact]
        public void Constructor_Value_Invalid()
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => new Relevance(-1));
            Assert.Equal("Value must be greater than or equal to zero.\r\nParameter name: value", ex.Message);
        }

        [Fact]
        public void SetValue()
        {
            Relevance rel = new Relevance();
            rel.SetValue(8);
            Assert.Equal(8, rel.Value);
            Assert.False(rel.IsFinalized, "Value should not be finalized");
            rel.SetValue(14);
            Assert.Equal(14, rel.Value);
            Assert.False(rel.IsFinalized, "Value should not be finalized");
        }

        [Fact]
        public void SetValue_Value_Invalid()
        {
            Relevance sut = new Relevance();
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => sut.SetValue(-1));
            Assert.Equal("Value must be greater than or equal to zero.\r\nParameter name: value", ex.Message);
        }

        [Fact]
        // Underscore to avoid interference with Destructor
        public void Finalize_()
        {
            Relevance rel = new Relevance();
            rel.SetValue(12);
            Assert.Equal(12, rel.Value);
            rel.Finalize(24);
            Assert.Equal(50, rel.Value);
            Assert.True(rel.IsFinalized, "Value should be finalized");
        }

        [Fact]
        public void Finalize_Factor_Invalid()
        {
            Relevance sut = new Relevance();
            sut.SetValue(0);
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => sut.Finalize(-1));
            Assert.Equal("Total must be greater than or equal to zero.\r\nParameter name: total", ex.Message);
        }

        [Fact]
        public void Finalize_Already_Performed()
        {
            Relevance sut = new Relevance();
            sut.SetValue(0);
            sut.Finalize(0.5F);
            var ex = Assert.Throws<InvalidOperationException>(() => sut.Finalize(1));
            Assert.Equal("Finalization already performed.", ex.Message);
        }

        [Fact]
        public void Finalize_Total_Invalid()
        {
            Relevance sut = new Relevance(2);
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => sut.Finalize(-1));
            Assert.Equal("Total must be greater than or equal to zero.\r\nParameter name: total", ex.Message);
        }

        [Fact]
        public void Finalize_SetVAlue_AfterFinalize()
        {
            Relevance sut = new Relevance();
            sut.SetValue(0);
            sut.Finalize(12);
            var ex = Assert.Throws<InvalidOperationException>(() => sut.Finalize(8));
            Assert.Equal("Finalization already performed.", ex.Message);
        }

        [Fact]
        public void NormalizeAfterFinalization()
        {
            Relevance rel = new Relevance(8);
            rel.Finalize(16);
            rel.NormalizeAfterFinalization(0.5F);
            Assert.Equal(25, rel.Value, 1);
        }

        [Fact]
        public void NormalizeAfterFinalization_Factor_Invalid()
        {
            Relevance sut = new Relevance(8);
            var ex = Assert.Throws<InvalidOperationException>(() => sut.NormalizeAfterFinalization(16));
            Assert.Equal("Normalization can be performed only after finalization.", ex.Message);
        }

        [Fact]
        public void NormalizeAfterFinalization_BeforeFinalize()
        {
            Relevance sut = new Relevance(8);
            var ex = Assert.Throws<InvalidOperationException>(() => sut.NormalizeAfterFinalization(0.5F));
            Assert.Equal("Normalization can be performed only after finalization.", ex.Message);
        }
    }
}
