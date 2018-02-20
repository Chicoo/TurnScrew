using System;
using System.Collections.Generic;
using Xunit;

namespace TurnScrew.Wiki.SearchEngine.UnitTests
{
    public class DumpedChangeTests : TestsBase
    {
        [Fact]
        public void Constructor()
        {
            DumpedChange sut = new DumpedChange(
                new DumpedDocument(MockDocument("doc", "Docum", "d", DateTime.Now)),
                new List<DumpedWord>(new DumpedWord[] { new DumpedWord(1, "word") }),
                new List<DumpedWordMapping>(new DumpedWordMapping[] { new DumpedWordMapping(1, 1, 4, 1, WordLocation.Content.Location) }));

            Assert.Equal("doc", sut.Document.Name);
            Assert.Single(sut.Words);
            Assert.Equal("word", sut.Words[0].Text);
            Assert.Single(sut.Mappings);
            Assert.Equal((uint)1, sut.Mappings[0].WordID);
        }

        [Fact]
        public void Constructor_NullDocument()
        {
            var ex = Assert.Throws<ArgumentNullException>(() =>
                new DumpedChange(
                null,
                new List<DumpedWord>(new DumpedWord[] { new DumpedWord(1, "word") }),
                new List<DumpedWordMapping>(new DumpedWordMapping[] { new DumpedWordMapping(1, 1, 4, 1, WordLocation.Content.Location) })));

            Assert.Equal("Value cannot be null.\r\nParameter name: document", ex.Message);
        }

        [Fact]
        public void Constructor_NullWords()
        {
            var ex = Assert.Throws<ArgumentNullException>(() =>
                new DumpedChange(
                    new DumpedDocument(MockDocument("doc", "Docum", "d", DateTime.Now)),
                    null,
                    new List<DumpedWordMapping>(new DumpedWordMapping[] { new DumpedWordMapping(1, 1, 4, 1, WordLocation.Content.Location) })));

            Assert.Equal("Value cannot be null.\r\nParameter name: words", ex.Message);
        }

        [Fact]
        public void Constructor_EmptyWords()
        {
            // Words can be empty
            DumpedChange change = new DumpedChange(
                new DumpedDocument(MockDocument("doc", "Docum", "d", DateTime.Now)),
                new List<DumpedWord>(),
                new List<DumpedWordMapping>(new DumpedWordMapping[] { new DumpedWordMapping(1, 1, 4, 1, WordLocation.Content.Location) }));
        }

        [Fact]
        public void Constructor_NullMappings()
        {
            var ex = Assert.Throws<ArgumentNullException>(() =>
                new DumpedChange(
                new DumpedDocument(MockDocument("doc", "Docum", "d", DateTime.Now)),
                new List<DumpedWord>(),
                null));

            Assert.Equal("Value cannot be null.\r\nParameter name: mappings", ex.Message);
        }

        [Fact]
        public void Constructor_EmptyMappings()
        {
            // Mappings can be empty
            DumpedChange change = new DumpedChange(
                new DumpedDocument(MockDocument("doc", "Docum", "d", DateTime.Now)),
                new List<DumpedWord>(new DumpedWord[] { new DumpedWord(1, "word") }),
                new List<DumpedWordMapping>());
        }
    }
}
