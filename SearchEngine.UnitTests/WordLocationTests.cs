using System;
using Xunit;

namespace TurnScrew.Wiki.SearchEngine.UnitTests {
	public class WordLocationTests {
		[Fact]
		public void StaticInstances_Title() {
			WordLocation loc1 = WordLocation.Title;
			WordLocation loc2 = WordLocation.Title;

			Assert.Equal("Title", loc1.ToString());
			Assert.Equal("Title", loc2.ToString());

			Assert.True(loc1 == loc2, "loc1 should equal loc2");
			Assert.True(loc1.Equals(loc2), "loc1 should equal loc2");
			Assert.NotSame(loc2, loc1);
		}

		[Fact]
		public void StaticInstances_Content() {
			WordLocation loc1 = WordLocation.Content;
			WordLocation loc2 = WordLocation.Content;

			Assert.Equal("Content", loc1.ToString());
			Assert.Equal("Content", loc2.ToString());

			Assert.True(loc1 == loc2, "loc1 should equal loc2");
			Assert.True(loc1.Equals(loc2), "loc1 should equal loc2");
			Assert.NotSame(loc2, loc1);
		}

		[Fact]
		public void StaticInstances_Keywords() {
			WordLocation loc1 = WordLocation.Keywords;
			WordLocation loc2 = WordLocation.Keywords;

			Assert.Equal("Keywords", loc1.ToString());
			Assert.Equal("Keywords", loc2.ToString());

			Assert.True(loc1 == loc2, "loc1 should equal loc2");
			Assert.True(loc1.Equals(loc2), "loc1 should equal loc2");
			Assert.NotSame(loc2, loc1);
		}

		[Fact]
		public void StaticInstances_CompareTo() {
			Assert.Equal(0, WordLocation.Title.CompareTo(WordLocation.Title));
			Assert.Equal(1, WordLocation.Content.CompareTo(WordLocation.Title));
			Assert.Equal(-1, WordLocation.Title.CompareTo(WordLocation.Content));
		}

		[Fact]
		public void StaticInstances_RelativeRelevance() {
			Assert.True(WordLocation.Title.RelativeRelevance > WordLocation.Keywords.RelativeRelevance, "Wrong relevance relationship");
			Assert.True(WordLocation.Keywords.RelativeRelevance > WordLocation.Content.RelativeRelevance, "Wrong relevance relationship");
		}

		[Fact]
		public void StaticMethods_GetInstance() {
			Assert.Equal(WordLocation.Title, WordLocation.GetInstance(1));
			Assert.Equal(WordLocation.Keywords, WordLocation.GetInstance(2));
			Assert.Equal(WordLocation.Content, WordLocation.GetInstance(3));
		}

        [Fact]
        public void StaticMethods_GetInstance_Location_Low()
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => WordLocation.GetInstance(0));
            Assert.Equal("Invalid location.\r\nParameter name: location", ex.Message);
        }

        [Fact]
        public void StaticMethods_GetInstance_Location_High()
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => WordLocation.GetInstance(4));
            Assert.Equal("Invalid location.\r\nParameter name: location", ex.Message);
        }
    }

}
