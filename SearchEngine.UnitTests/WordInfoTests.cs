using System;
using Xunit;

namespace TurnScrew.Wiki.SearchEngine.UnitTests
{
    public class WordInfoTests : TestsBase
    {
        [Fact]
        public void Constructor()
        {
            WordInfo sut = new WordInfo("continuous", 2, 0, WordLocation.Content);
            Assert.Equal(2, sut.FirstCharIndex);
            Assert.Equal(10, sut.Text.Length);
        }

        [Fact]
        public void Constructor_Text_Null()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new WordInfo(null, 0, 0, WordLocation.Content));

            Assert.Equal("Value cannot be null.\r\nParameter name: text", ex.Message);
        }

        [Fact]
        public void Constructor_Text_Empty()
        {
            var ex = Assert.Throws<ArgumentException>(() => new WordInfo("", 0, 0, WordLocation.Content));
            Assert.Equal("Invalid text.\r\nParameter name: text", ex.Message);
        }

        [Fact]
        public void Equal()
        {
            WordInfo info1 = new WordInfo("word", 0, 0, WordLocation.Content);
            WordInfo info2 = new WordInfo("word", 0, 0, WordLocation.Content);
            WordInfo info3 = new WordInfo("word", 10, 1, WordLocation.Content);
            WordInfo info4 = new WordInfo("word", 10, 1, WordLocation.Title);
            WordInfo info5 = new WordInfo("word2", 0, 0, WordLocation.Content);

            Assert.True(info1.Equals(info2), "info1 should equal info2");
            Assert.False(info1.Equals(info3), "info1 should not equal info3");
            Assert.False(info3.Equals(info4), "info3 should not equal info4");
            Assert.True(info1.Equals(info1), "info1 should equal itself");
            Assert.False(info1.Equals(null), "info1 should not equal null");
            Assert.False(info1.Equals("hello"), "info1 should not equal a string");
            Assert.False(info1.Equals(info5), "info1 should not equal info5");
        }

        [Fact]
        public void EqualityOperator()
        {
            WordInfo info1 = new WordInfo("word", 0, 0, WordLocation.Content);
            WordInfo info2 = new WordInfo("word", 0, 0, WordLocation.Content);
            WordInfo info3 = new WordInfo("word", 10, 1, WordLocation.Content);
            WordInfo info4 = new WordInfo("word", 10, 1, WordLocation.Title);
            WordInfo info5 = new WordInfo("word2", 0, 0, WordLocation.Content);

            Assert.True(info1 == info2, "info1 should equal info2");
            Assert.False(info1 == info3, "info1 should not equal info3");
            Assert.False(info3 == info4, "info3 should not equal info4");
            Assert.False(info1 == null, "info1 should not equal null");
            Assert.False(info1 == info5, "info1 should not equal info5");
        }

        [Fact]
        public void InequalityOperator()
        {
            WordInfo info1 = new WordInfo("word", 0, 0, WordLocation.Content);
            WordInfo info2 = new WordInfo("word", 0, 0, WordLocation.Content);
            WordInfo info3 = new WordInfo("word", 10, 1, WordLocation.Content);
            WordInfo info4 = new WordInfo("word", 10, 1, WordLocation.Title);
            WordInfo info5 = new WordInfo("word2", 0, 0, WordLocation.Content);

            Assert.False(info1 != info2, "info1 should equal info2");
            Assert.True(info1 != info3, "info1 should not equal info3");
            Assert.True(info3 != info4, "info3 should not equal info4");
            Assert.True(info1 != null, "info1 should not equal null");
            Assert.True(info1 != info5, "info1 should not equal info5");
        }

        [Fact]
        public void CompareTo()
        {
            WordInfo info1 = new WordInfo("word", 0, 0, WordLocation.Content);
            WordInfo info2 = new WordInfo("word", 0, 0, WordLocation.Content);
            WordInfo info3 = new WordInfo("word", 10, 1, WordLocation.Content);
            WordInfo info4 = new WordInfo("word", 10, 1, WordLocation.Title);
            WordInfo info5 = new WordInfo("word2", 0, 0, WordLocation.Content);

            Assert.Equal(0, info1.CompareTo(info2));
            Assert.Equal(-3, info1.CompareTo(info3));
            Assert.Equal(2, info3.CompareTo(info4));
            Assert.Equal(1, info1.CompareTo(null));
            Assert.Equal(-1, info1.CompareTo(info5));
        }
    }
}
