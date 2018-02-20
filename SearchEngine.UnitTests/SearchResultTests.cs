using System;
using Xunit;

namespace TurnScrew.Wiki.SearchEngine.UnitTests
{
    public class SearchResultTests : TestsBase
    {
        [Fact]
        public void Constructor()
        {
            IDocument doc = MockDocument("Doc", "Document", "ptdoc", DateTime.Now);
            SearchResult res = new SearchResult(doc);

            Assert.Equal(doc, res.Document);
            Assert.Equal(0, res.Relevance.Value);
            Assert.False(res.Relevance.IsFinalized, "Initial relevance value should not be finalized");
        }

        [Fact]
        public void Constructor_Document_Null()
        {
            SearchResultCollection sut = new SearchResultCollection();
            var ex = Assert.Throws<ArgumentNullException>(() => new SearchResult(null));
            Assert.Equal("Value cannot be null.\r\nParameter name: document", ex.Message);
        }
    }
}
