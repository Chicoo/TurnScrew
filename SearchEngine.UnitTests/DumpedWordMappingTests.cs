using System;
using Xunit;

namespace TurnScrew.Wiki.SearchEngine.UnitTests
{
    public class DumpedWordMappingTests : TestsBase
    {
        [Fact]
        public void Constructor_Integers()
        {
            DumpedWordMapping sut = new DumpedWordMapping(5, 2, 3, 4, WordLocation.Keywords.Location);
            Assert.Equal((uint)5, sut.WordID);
            Assert.Equal((uint)2, sut.DocumentID);
            Assert.Equal(3, sut.FirstCharIndex);
            Assert.Equal(4, sut.WordIndex);
            Assert.Equal(WordLocation.Keywords.Location, sut.Location);
        }

        [Fact]
        public void Constructor_WithBasicWordInfo()
        {
            DumpedWordMapping sut = new DumpedWordMapping(5, 2, new BasicWordInfo(3, 4, WordLocation.Keywords));
            Assert.Equal((uint)5, sut.WordID);
            Assert.Equal((uint)2, sut.DocumentID);
            Assert.Equal(3, sut.FirstCharIndex);
            Assert.Equal(4, sut.WordIndex);
            Assert.Equal(WordLocation.Keywords.Location, sut.Location);
        }

        [Fact]
        public void Constructor_WithBasicWordInfo_Info_Null()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new DumpedWordMapping(10, 12, null));
            Assert.Equal("Value cannot be null.\r\nParameter name: info", ex.Message);
        }
    }
}
