using Xunit;

namespace TurnScrew.Wiki.SearchEngine.UnitTests
{
    public class BasicWordInfoTests
    {
        [Fact]
        public void Constructor()
        {
            BasicWordInfo info = new BasicWordInfo(2, 0, WordLocation.Content);
            Assert.Equal(2, info.FirstCharIndex);
            Assert.Equal(0, info.WordIndex);
        }

        [Fact]
        public void Equal()
        {
            BasicWordInfo info1 = new BasicWordInfo(0, 0, WordLocation.Content);
            BasicWordInfo info2 = new BasicWordInfo(0, 0, WordLocation.Content);
            BasicWordInfo info3 = new BasicWordInfo(10, 1, WordLocation.Content);
            BasicWordInfo info4 = new BasicWordInfo(10, 1, WordLocation.Title);

            Assert.True(info1.Equals(info2), "info1 should equal info2");
            Assert.False(info1.Equals(info3), "info1 should not equal info3");
            Assert.False(info3.Equals(info4), "info3 should not equal info4");
            Assert.True(info1.Equals(info1), "info1 should equal itself");
            Assert.False(info1.Equals(null), "info1 should not equal null");
            Assert.False(info1.Equals("hello"), "info1 should not equal a string");
        }

        [Fact]
        public void EqualityOperator()
        {
            BasicWordInfo info1 = new BasicWordInfo(0, 0, WordLocation.Content);
            BasicWordInfo info2 = new BasicWordInfo(0, 0, WordLocation.Content);
            BasicWordInfo info3 = new BasicWordInfo(10, 1, WordLocation.Content);
            BasicWordInfo info4 = new BasicWordInfo(10, 1, WordLocation.Title);

            Assert.True(info1 == info2, "info1 should equal info2");
            Assert.False(info1 == info3, "info1 should not equal info3");
            Assert.False(info3 == info4, "info3 should not equal info4");
            Assert.False(info1 == null, "info1 should not equal null");
        }

        [Fact]
        public void InequalityOperator()
        {
            BasicWordInfo info1 = new BasicWordInfo(0, 0, WordLocation.Content);
            BasicWordInfo info2 = new BasicWordInfo(0, 0, WordLocation.Content);
            BasicWordInfo info3 = new BasicWordInfo(10, 1, WordLocation.Content);
            BasicWordInfo info4 = new BasicWordInfo(10, 1, WordLocation.Title);

            Assert.False(info1 != info2, "info1 should equal info2");
            Assert.True(info1 != info3, "info1 should not equal info3");
            Assert.True(info3 != info4, "info3 should not equal info4");
            Assert.True(info1 != null, "info1 should not equal null");
        }

        [Fact]
        public void CompareTo()
        {
            BasicWordInfo info1 = new BasicWordInfo(0, 0, WordLocation.Content);
            BasicWordInfo info2 = new BasicWordInfo(0, 0, WordLocation.Content);
            BasicWordInfo info3 = new BasicWordInfo(10, 1, WordLocation.Content);
            BasicWordInfo info4 = new BasicWordInfo(10, 1, WordLocation.Title);

            Assert.Equal(0, info1.CompareTo(info2));
            Assert.Equal(-1, info1.CompareTo(info3));
            Assert.Equal(2, info3.CompareTo(info4));
            Assert.Equal(1, info1.CompareTo(null));
        }
    }
}
