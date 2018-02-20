using System;
using Xunit;

namespace TurnScrew.Wiki.SearchEngine.UnitTests
{
    public class SearchParametersTests
    {
        [Fact]
        public void Constructor_QueryOnly()
        {
            SearchParameters par = new SearchParameters("query");
            Assert.Equal("query", par.Query);
            Assert.Null(par.DocumentTypeTags);
            Assert.Equal(SearchOptions.AtLeastOneWord, par.Options);
        }

        [Fact]
        public void Constructor_QueryOnly_Query_Null()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new SearchParameters(null));

            Assert.Equal("Value cannot be null.\r\nParameter name: query", ex.Message);
        }

        [Fact]
        public void Constructor_QueryOnly_Query_Empty()
        {
            var ex = Assert.Throws<ArgumentException>(() => new SearchParameters(""));
            Assert.Equal("Query cannot be empty.\r\nParameter name: query", ex.Message);
        }

        [Fact]
        public void Constructor_QueryDocumentTypeTags()
        {
            SearchParameters par = new SearchParameters("query", "blah", "doc");
            Assert.Equal("query", par.Query);
            Assert.Equal(2, par.DocumentTypeTags.Length);
            Assert.Equal("blah", par.DocumentTypeTags[0]);
            Assert.Equal("doc", par.DocumentTypeTags[1]);
            Assert.Equal(SearchOptions.AtLeastOneWord, par.Options);
        }

        [Fact]
        public void Constructor_QueryDocumentTypeTags_Query_Null()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new SearchParameters(null, "blah", "doc"));

            Assert.Equal("Value cannot be null.\r\nParameter name: query", ex.Message);
        }

        [Fact]
        public void Constructor_QueryDocumentTypeTags_Query_Empty()
        {
            var ex = Assert.Throws<ArgumentException>(() => new SearchParameters("", "blah", "doc"));

            Assert.Equal("Query cannot be empty.\r\nParameter name: query", ex.Message);
        }

        [Fact]
        public void Constructor_QueryDocumentTypeTags_NullDocumentTypeTags()
        {
            SearchParameters par = new SearchParameters("query", null);
            Assert.Equal("query", par.Query);
            Assert.Null(par.DocumentTypeTags);
            Assert.Equal(SearchOptions.AtLeastOneWord, par.Options);
        }

        [Fact]
        public void Constructor_QueryDocumentTypeTags_DocumentTypeTags_Empty()
        {
            var ex = Assert.Throws<ArgumentException>(() => new SearchParameters("query", new string[0]));

            Assert.Equal("DocumentTypeTags cannot be empty.\r\nParameter name: documentTypeTags", ex.Message);
        }

        [Fact]
        public void Constructor_QueryDocumentTypeTags_DocumentTypeTagsElement_Null()
        {
            Assert.Throws<ArgumentNullException>("documentTypeTags", () => new SearchParameters("query", new string[] { "blah", null }));
        }

        [Fact]
        public void Constructor_QueryDocumentTypeTags_DocumentTypeTagsElement_Empty()
        {
            var ex = Assert.Throws<ArgumentException>(() => new SearchParameters("query", new string[] { "blah", "" }));

            Assert.Equal("DocumentTypeTag cannot be empty.\r\nParameter name: documentTypeTag", ex.Message);
        }

        [Fact]
        public void Constructor_QueryOptions()
        {
            SearchParameters par = new SearchParameters("query", SearchOptions.ExactPhrase);
            Assert.Equal("query", par.Query);
            Assert.Null(par.DocumentTypeTags);
            Assert.Equal(SearchOptions.ExactPhrase, par.Options);
        }

        [Fact]
        public void Constructor_QueryOptions_Query_Null()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new SearchParameters(null, SearchOptions.ExactPhrase));

            Assert.Equal("Value cannot be null.\r\nParameter name: query", ex.Message);
        }

        [Fact]
        public void Constructor_QueryDocumentOptions_Query_Empty()
        {
            var ex = Assert.Throws<ArgumentException>(() => new SearchParameters("", SearchOptions.ExactPhrase));

            Assert.Equal("Query cannot be empty.\r\nParameter name: query", ex.Message);
        }

        [Fact]
        public void Constructor_Full()
        {
            SearchParameters par = new SearchParameters("query", new string[] { "blah", "doc" }, SearchOptions.AllWords);
            Assert.Equal("query", par.Query);
            Assert.Equal(2, par.DocumentTypeTags.Length);
            Assert.Equal("blah", par.DocumentTypeTags[0]);
            Assert.Equal("doc", par.DocumentTypeTags[1]);
            Assert.Equal(SearchOptions.AllWords, par.Options);
        }

        [Fact]
        public void Constructor_Full_Query_Null()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new SearchParameters(null, new string[] { "blah", "doc" }, SearchOptions.AllWords));

            Assert.Equal("Value cannot be null.\r\nParameter name: query", ex.Message);
        }

        [Fact]
        public void Constructor_Full_Query_Empty()
        {
            var ex = Assert.Throws<ArgumentException>(() => new SearchParameters("", new string[] { "blah", "doc" }, SearchOptions.AllWords));

            Assert.Equal("Query cannot be empty.\r\nParameter name: query", ex.Message);
        }

        [Fact]
        public void Constructor_Full_NullDocumentTypeTags()
        {
            SearchParameters par = new SearchParameters("query", null, SearchOptions.AllWords);
            Assert.Equal("query", par.Query);
            Assert.Null(par.DocumentTypeTags);
            Assert.Equal(SearchOptions.AllWords, par.Options);
        }

        [Fact]
        public void Constructor_Full_DocumentTypeTags_Empty()
        {
            var ex = Assert.Throws<ArgumentException>(() => new SearchParameters("query", new string[0], SearchOptions.AllWords));

            Assert.Equal("DocumentTypeTags cannot be empty.\r\nParameter name: documentTypeTags", ex.Message);
        }

        [Fact]
        public void Constructor_Full_DocumentTypeTagsElement_Null()
        {
            var ex = Assert.Throws<ArgumentNullException>("documentTypeTags", () => new SearchParameters("query", new string[] { "blah", null }, SearchOptions.ExactPhrase));
        }

        [Fact]
        public void Constructor_Full_DocumentTypeTagsElement_Empty()
        {
            var ex = Assert.Throws<ArgumentException>(() => new SearchParameters("query", new string[] { "blah", "" }, SearchOptions.ExactPhrase));

            Assert.Equal("DocumentTypeTag cannot be empty.\r\nParameter name: documentTypeTag", ex.Message);
        }
    }
}
