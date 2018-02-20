using System;
using System.Collections.Generic;
using Xunit;

namespace TurnScrew.Wiki.SearchEngine.UnitTests
{
    public class IndexChangedEventArgsTests : TestsBase
    {
        [Fact]
        public void Constructor()
        {
            IDocument doc = MockDocument("Doc", "Document", "ptdoc", DateTime.Now);
            DumpedChange change = new DumpedChange(new DumpedDocument(doc), new List<DumpedWord>(),
                new List<DumpedWordMapping>(new DumpedWordMapping[] { new DumpedWordMapping(1, 1, 1, 1, 1) }));

            IndexChangedEventArgs args = new IndexChangedEventArgs(doc, IndexChangeType.DocumentAdded, change, null);

            Assert.Same(doc, args.Document);
            Assert.Equal(IndexChangeType.DocumentAdded, args.Change);
        }

        [Fact]
        public void Constructor_Document_Null()
        {
            IDocument doc = MockDocument("Doc", "Document", "ptdoc", DateTime.Now);
            DumpedChange change = new DumpedChange(new DumpedDocument(doc), new List<DumpedWord>(), new List<DumpedWordMapping>(new DumpedWordMapping[] { new DumpedWordMapping(1, 1, 1, 1, 1) }));
            var ex = Assert.Throws<ArgumentNullException>(() => new IndexChangedEventArgs(null, IndexChangeType.DocumentAdded, change, null));
            Assert.Equal("Value cannot be null.\r\nParameter name: document", ex.Message);
        }

        [Fact]
        public void Constructor_IndexCleared()
        {
            IndexChangedEventArgs args = new IndexChangedEventArgs(null, IndexChangeType.IndexCleared, null, null);
        }

        [Fact]
        public void Constructor_ChangeData_Null()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new IndexChangedEventArgs(MockDocument("Doc", "Document", "ptdoc", DateTime.Now), IndexChangeType.DocumentAdded, null, null));
            Assert.Equal("Value cannot be null.\r\nParameter name: changeData", ex.Message);
        }
    }
}
