using System;
using Xunit;

namespace TurnScrew.Wiki.SearchEngine.UnitTests
{
    public class InMemoryIndexBaseTests : IndexBaseTests
    {
        /// <summary>
        /// Gets the instance of the index to test.
        /// </summary>
        /// <returns>The instance of the index.</returns>
        protected override IIndex GetIndex()
        {
            return MockInMemoryIndex();
        }

        [Fact]
        public void SetBuildDocumentDelegate_BuildDocument_Null()
        {
            IInMemoryIndex sut = (IInMemoryIndex)GetIndex();
            var ex = Assert.Throws<ArgumentNullException>("buildDocument", () => sut.SetBuildDocumentDelegate(null));
        }

        [Fact]
        public void InitializeData()
        {
            IInMemoryIndex sut = (IInMemoryIndex)GetIndex();

            IDocument d = MockDocument("doc", "Document", "doc", DateTime.Now);
            DumpedDocument[] documents = new DumpedDocument[] { new DumpedDocument(d) };

            DumpedWord[] words = new DumpedWord[] {
                new DumpedWord(new Word(1, "document")),
                new DumpedWord(new Word(2, "this")),
                new DumpedWord(new Word(3, "is")),
                new DumpedWord(new Word(4, "some")),
                new DumpedWord(new Word(5, "content")) };

            DumpedWordMapping[] mappings = new DumpedWordMapping[] {
                new DumpedWordMapping(words[0].ID, documents[0].ID, new BasicWordInfo(0, 0, WordLocation.Title)),
                new DumpedWordMapping(words[1].ID, documents[0].ID, new BasicWordInfo(0, 0, WordLocation.Content)),
                new DumpedWordMapping(words[2].ID, documents[0].ID, new BasicWordInfo(5, 1, WordLocation.Content)),
                new DumpedWordMapping(words[3].ID, documents[0].ID, new BasicWordInfo(8, 2, WordLocation.Content)),
                new DumpedWordMapping(words[4].ID, documents[0].ID, new BasicWordInfo(13, 3, WordLocation.Content)) };

            sut.SetBuildDocumentDelegate(delegate (DumpedDocument doc) { return d; });

            sut.InitializeData(documents, words, mappings);

            Assert.Equal(1, sut.TotalDocuments);
            Assert.Equal(5, sut.TotalWords);
            Assert.Equal(5, sut.TotalOccurrences);

            SearchResultCollection res = sut.Search(new SearchParameters("document content"));
            Assert.Single(res);
            Assert.Equal(2, res[0].Matches.Count);

            Assert.Equal("document", res[0].Matches[0].Text);
            Assert.Equal(0, res[0].Matches[0].FirstCharIndex);
            Assert.Equal(0, res[0].Matches[0].WordIndex);
            Assert.Equal(WordLocation.Title, res[0].Matches[0].Location);

            Assert.Equal("content", res[0].Matches[1].Text);
            Assert.Equal(13, res[0].Matches[1].FirstCharIndex);
            Assert.Equal(3, res[0].Matches[1].WordIndex);
            Assert.Equal(WordLocation.Content, res[0].Matches[1].Location);
        }

        [Fact]
        public void InitializeData_Documents_Null()
        {
            IInMemoryIndex sut = (IInMemoryIndex)GetIndex();
            sut.SetBuildDocumentDelegate(delegate (DumpedDocument doc) { return null; });
            var ex = Assert.Throws<ArgumentNullException>(() => sut.InitializeData(null, new DumpedWord[0], new DumpedWordMapping[0]));
            Assert.Equal("Value cannot be null.\r\nParameter name: documents", ex.Message);
        }

        [Fact]
        public void InitializeData_Words_Null()
        {
            IInMemoryIndex sut = (IInMemoryIndex)GetIndex();
            sut.SetBuildDocumentDelegate(delegate (DumpedDocument doc) { return null; });
            var ex = Assert.Throws<ArgumentNullException>(() => sut.InitializeData(new DumpedDocument[0], null, new DumpedWordMapping[0]));
            Assert.Equal("Value cannot be null.\r\nParameter name: words", ex.Message);
        }

        [Fact]
        public void InitializeData_Mappings_Null()
        {
            IInMemoryIndex sut = (IInMemoryIndex)GetIndex();
            sut.SetBuildDocumentDelegate(delegate (DumpedDocument doc) { return null; });
            var ex = Assert.Throws<ArgumentNullException>(() => sut.InitializeData(new DumpedDocument[0], new DumpedWord[0], null));
            Assert.Equal("Value cannot be null.\r\nParameter name: mappings", ex.Message);
        }

        [Fact]
        public void InitializeData_Document_NoBuildDelegate()
        {
            IInMemoryIndex sut = (IInMemoryIndex)GetIndex();
            var ex = Assert.Throws<InvalidOperationException>(() => sut.InitializeData(new DumpedDocument[0], new DumpedWord[0], new DumpedWordMapping[0]));
            Assert.Equal("InitializeData can be invoked only when the BuildDocument delegate is set.", ex.Message);
        }

        [Fact]
        public void InitializeData_DocumentNotAvailable()
        {
            IInMemoryIndex index = (IInMemoryIndex)GetIndex();

            IDocument doc = MockDocument("doc", "Document", "doc", DateTime.Now);
            IDocument inexistent = MockDocument2("inexistent", "Inexistent", "doc", DateTime.Now);

            DumpedDocument[] documents = new DumpedDocument[] {
                new DumpedDocument(doc),
                new DumpedDocument(inexistent) };

            DumpedWord[] words = new DumpedWord[] {
                new DumpedWord(new Word(1, "document")),
                new DumpedWord(new Word(2, "this")),
                new DumpedWord(new Word(3, "is")),
                new DumpedWord(new Word(4, "some")),
                new DumpedWord(new Word(5, "content")),

                new DumpedWord(new Word(6, "inexistent")),
                new DumpedWord(new Word(7, "dummy")),
                new DumpedWord(new Word(8, "text")),
                new DumpedWord(new Word(9, "used")),
                new DumpedWord(new Word(10, "for")),
                new DumpedWord(new Word(11, "testing")),
                new DumpedWord(new Word(12, "purposes")) };

            DumpedWordMapping[] mappings = new DumpedWordMapping[] {
                new DumpedWordMapping(words[0].ID, documents[0].ID, new BasicWordInfo(0, 0, WordLocation.Title)),
                new DumpedWordMapping(words[1].ID, documents[0].ID, new BasicWordInfo(0, 0, WordLocation.Content)),
                new DumpedWordMapping(words[2].ID, documents[0].ID, new BasicWordInfo(5, 1, WordLocation.Content)),
                new DumpedWordMapping(words[3].ID, documents[0].ID, new BasicWordInfo(8, 2, WordLocation.Content)),
                new DumpedWordMapping(words[4].ID, documents[0].ID, new BasicWordInfo(13, 3, WordLocation.Content)),

                new DumpedWordMapping(words[5].ID, documents[1].ID, new BasicWordInfo(0, 0, WordLocation.Title)),
                new DumpedWordMapping(words[6].ID, documents[1].ID, new BasicWordInfo(0, 0, WordLocation.Content)),
                new DumpedWordMapping(words[7].ID, documents[1].ID, new BasicWordInfo(6, 1, WordLocation.Content)),
                new DumpedWordMapping(words[8].ID, documents[1].ID, new BasicWordInfo(11, 2, WordLocation.Content)),
                new DumpedWordMapping(words[9].ID, documents[1].ID, new BasicWordInfo(16, 3, WordLocation.Content)),
                new DumpedWordMapping(words[10].ID, documents[1].ID, new BasicWordInfo(20, 4, WordLocation.Content)),
                new DumpedWordMapping(words[11].ID, documents[1].ID, new BasicWordInfo(28, 5, WordLocation.Content)) };

            index.SetBuildDocumentDelegate(delegate (DumpedDocument d)
            {
                if (d.Name == "doc") return doc;
                else return null;
            });

            index.InitializeData(documents, words, mappings);

            Assert.Single(index.Search(new SearchParameters("this")));
            Assert.Empty(index.Search(new SearchParameters("dummy")));
        }
    }
}
