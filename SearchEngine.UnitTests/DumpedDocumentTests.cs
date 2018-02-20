using System;
using Xunit;

namespace TurnScrew.Wiki.SearchEngine.UnitTests {
	public class DumpedDocumentTests : TestsBase {
		[Fact]
		public void Constructor_WithDocument() {
			IDocument doc = MockDocument("name", "Title", "doc", DateTime.Now);
			DumpedDocument ddoc = new DumpedDocument(doc);

			Assert.Equal(doc.ID, ddoc.ID);
			Assert.Equal("name", ddoc.Name);
			Assert.Equal("Title", ddoc.Title);
			Assert.Equal(doc.DateTime, ddoc.DateTime);
		}

        [Fact]
        public void Constructor_WithDocument_NullDocument()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new DumpedDocument(null));

            Assert.Equal("Value cannot be null.\r\nParameter name: document", ex.Message);
        }

        [Fact]
		public void Constructor_WithParameters() {
			IDocument doc = MockDocument("name", "Title", "doc", DateTime.Now);
			DumpedDocument ddoc = new DumpedDocument(doc.ID, doc.Name, doc.Title, doc.TypeTag, doc.DateTime);

			Assert.Equal(doc.ID, ddoc.ID);
			Assert.Equal("name", ddoc.Name);
			Assert.Equal("Title", ddoc.Title);
			Assert.Equal(doc.DateTime, ddoc.DateTime);
		}

        [Fact]
        public void Constructor_WithDocument_Name_Null()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new DumpedDocument(10, null, "Title", "doc", DateTime.Now));

            Assert.Equal("Value cannot be null.\r\nParameter name: name", ex.Message);
        }

        [Fact]
        public void Constructor_WithDocument_Name_Empty()
        {
            var ex = Assert.Throws<ArgumentException>(() => new DumpedDocument(10, "", "Title", "doc", DateTime.Now));

            Assert.Equal("Name cannot be empty.\r\nParameter name: name", ex.Message);
        }

        [Fact]
        public void Constructor_WithDocument_Title_Null()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new DumpedDocument(10, "Name", null, "doc", DateTime.Now));

            Assert.Equal("Value cannot be null.\r\nParameter name: title", ex.Message);
        }

        [Fact]
        public void Constructor_WithDocument_Title_Empty()
        {
            var ex = Assert.Throws<ArgumentException>(() => new DumpedDocument(10, "Name", "", "doc", DateTime.Now));

            Assert.Equal("Title cannot be empty.\r\nParameter name: title", ex.Message);
        }

        [Fact]
        public void Constructor_WithDocument_TypeTag_Null()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new DumpedDocument(10, "Name", "Title", null, DateTime.Now));

            Assert.Equal("Value cannot be null.\r\nParameter name: typeTag", ex.Message);
        }

        [Fact]
        public void Constructor_WithDocument_TypeTag_Empty()
        {
            var ex = Assert.Throws<ArgumentException>(() => new DumpedDocument(10, "Name", "Title", "", DateTime.Now));

            Assert.Equal("Type Tag cannot be empty.\r\nParameter name: typeTag", ex.Message);
        }

    }

}
