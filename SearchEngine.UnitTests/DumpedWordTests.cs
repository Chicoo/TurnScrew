
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace TurnScrew.Wiki.SearchEngine.UnitTests {

	
	public class DumpedWordTests {

		[Fact]
		public void Constructor_WithParameters() {
			DumpedWord w = new DumpedWord(12, "word");
			Assert.Equal((uint)12, w.ID);
			Assert.Equal("word", w.Text);
		}

        [Fact]
        public void Constructor_WithParameters_Text_Null()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new DumpedWord(5, null));

            Assert.Equal("Value cannot be null.\r\nParameter name: text", ex.Message);
        }

        [Fact]
        public void Constructor_WithParameters_Text_Empty()
        {
            var ex = Assert.Throws<ArgumentException>(() => new DumpedWord(5 ,""));

            Assert.Equal("Text cannot be empty.\r\nParameter name: text", ex.Message);
        }

        [Fact]
		public void Constructor_Word() {
			DumpedWord w = new DumpedWord(new Word(23, "text"));
            Assert.Equal((uint)23, w.ID);
            Assert.Equal("text", w.Text);
        }

        [Fact]
        public void Constructor_Word_Word_Null()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new DumpedWord(null));
            Assert.Equal("Value cannot be null.\r\nParameter name: word", ex.Message);
        }
    }

}
