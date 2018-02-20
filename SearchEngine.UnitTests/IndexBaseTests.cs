using System;
using Xunit;

namespace TurnScrew.Wiki.SearchEngine.UnitTests
{
    public abstract class IndexBaseTests : TestsBase
    {
        // These tests treat instances of IInMemoryIndex as special cases
        // They are tested correctly, properly handling the IndexChanged event

        /// <summary>
        /// Gets the instance of the index to test.
        /// </summary>
        /// <returns>The instance of the index.</returns>
        protected abstract IIndex GetIndex();

        [Fact]
        public void StopWordsProperty()
        {
            IIndex sut = GetIndex();
            Assert.Empty(sut.StopWords);

            sut.StopWords = new string[] { "the", "those" };
            Assert.Equal(2, sut.StopWords.Length);
            Assert.Equal("the", sut.StopWords[0]);
            Assert.Equal("those", sut.StopWords[1]);
        }

        [Fact]
        public void Statistics()
        {
            IIndex sut = GetIndex();

            Assert.Equal(0, sut.TotalWords);
            Assert.Equal(0, sut.TotalDocuments);
            Assert.Equal(0, sut.TotalOccurrences);
        }

        [Fact]
        public void Clear()
        {
            IIndex sut = GetIndex();
            IInMemoryIndex imIndex = sut as IInMemoryIndex;

            IDocument doc = MockDocument("Doc", "Document", "ptdoc", DateTime.Now);

            if (imIndex != null) imIndex.IndexChanged += AutoHandlerForDocumentStorage;
            sut.StoreDocument(doc, null, PlainTextDocumentContent, null);

            bool eventFired = false;
            if (imIndex != null)
            {
                imIndex.IndexChanged += delegate (object sender, IndexChangedEventArgs e)
                {
                    eventFired = true;
                };
            }

            sut.Clear(null);

            if (imIndex != null) Assert.True(eventFired, "IndexChanged event not fired");
            Assert.Equal(0, sut.TotalDocuments);
            Assert.Equal(0, sut.TotalWords);
            Assert.Equal(0, sut.TotalOccurrences);
            Assert.Empty(sut.Search(new SearchParameters("document")));
        }

        [Fact]
        public void StoreDocument()
        {
            IIndex sut = GetIndex();
            IInMemoryIndex imIndex = sut as IInMemoryIndex;

            IDocument doc = MockDocument("Doc", "Document", "ptdoc", DateTime.Now);

            bool eventFired = false;
            if (imIndex != null)
            {
                imIndex.IndexChanged += delegate (object sender, IndexChangedEventArgs e)
                {
                    eventFired = e.Document == doc && e.Change == IndexChangeType.DocumentAdded;
                    if (eventFired)
                    {
                        Assert.Equal("Doc", e.ChangeData.Document.Name);
                        Assert.Equal(5, e.ChangeData.Words.Count);
                        Assert.Equal(5, e.ChangeData.Mappings.Count);
                        Assert.True(e.ChangeData.Mappings.Find(delegate (DumpedWordMapping m) { return m.WordIndex == 0 && m.Location == WordLocation.Content.Location; }) != null, "Mappings does not contain a word");
                        Assert.True(e.ChangeData.Mappings.Find(delegate (DumpedWordMapping m) { return m.WordIndex == 1 && m.Location == WordLocation.Content.Location; }) != null, "Mappings does not contain a word");
                        Assert.True(e.ChangeData.Mappings.Find(delegate (DumpedWordMapping m) { return m.WordIndex == 2 && m.Location == WordLocation.Content.Location; }) != null, "Mappings does not contain a word");
                        Assert.True(e.ChangeData.Mappings.Find(delegate (DumpedWordMapping m) { return m.WordIndex == 3 && m.Location == WordLocation.Content.Location; }) != null, "Mappings does not contain a word");
                        Assert.True(e.ChangeData.Mappings.Find(delegate (DumpedWordMapping m) { return m.WordIndex == 0 && m.Location == WordLocation.Title.Location; }) != null, "Mappings does not contain a word");
                    }
                };

                imIndex.IndexChanged += AutoHandlerForDocumentStorage;
            }

            Assert.Equal(5, sut.StoreDocument(doc, null, PlainTextDocumentContent, null));
            Assert.Equal(5, sut.TotalWords);
            Assert.Equal(5, sut.TotalOccurrences);
            Assert.Equal(1, sut.TotalDocuments);
            if (imIndex != null) Assert.True(eventFired, "Event not fired");
        }

        [Fact]
        public void StoreDocument_ExistingDocument()
        {
            IIndex sut = GetIndex();
            IInMemoryIndex imIndex = sut as IInMemoryIndex;

            if (imIndex != null) imIndex.IndexChanged += AutoHandlerForDocumentStorage;

            IDocument doc = MockDocument("Doc", "Document", "ptdoc", DateTime.Now);

            Assert.Equal(5, sut.StoreDocument(doc, null, PlainTextDocumentContent, null));
            Assert.Equal(5, sut.TotalWords);
            Assert.Equal(5, sut.TotalOccurrences);
            Assert.Equal(1, sut.TotalDocuments);

            doc = MockDocument2("Doc", "Document", "ptdoc", DateTime.Now);

            Assert.Equal(7, sut.StoreDocument(doc, null, PlainTextDocumentContent2, null));
            Assert.Equal(7, sut.TotalWords);
            Assert.Equal(7, sut.TotalOccurrences);
            Assert.Equal(1, sut.TotalDocuments);
        }

        [Fact]
        public void StoreDocument_Document_Null()
        {
            IIndex sut = GetIndex();
            var ex = Assert.Throws<ArgumentNullException>("document", () => sut.StoreDocument(null, null, "blah", null));
        }

        [Fact]
        public void StoreDocument_Content_Null()
        {
            IIndex sut = GetIndex();
            var ex = Assert.Throws<ArgumentNullException>("content", () => sut.StoreDocument(MockDocument("Doc", "Document", "ptdoc", DateTime.Now), null, null, null));
        }

        [Fact]
        public void RemoveDocument()
        {
            IIndex sut = GetIndex();
            IInMemoryIndex imIndex = sut as IInMemoryIndex;

            IDocument doc1 = MockDocument("Doc1", "Document 1", "ptdoc", DateTime.Now);
            IDocument doc2 = MockDocument("Doc2", "Document 2", "ptdoc", DateTime.Now);

            if (imIndex != null) imIndex.IndexChanged += AutoHandlerForDocumentStorage;

            sut.StoreDocument(doc1, null, "", null);
            sut.StoreDocument(doc2, null, "", null);

            bool eventFired = false;

            if (imIndex != null)
            {
                imIndex.IndexChanged += delegate (object sender, IndexChangedEventArgs e)
                {
                    eventFired = e.Document == doc1 && e.Change == IndexChangeType.DocumentRemoved;
                    if (eventFired)
                    {
                        Assert.Equal("Doc1", e.ChangeData.Document.Name);
                        Assert.Single(e.ChangeData.Words);
                        Assert.Equal(6, e.ChangeData.Mappings.Count);
                        Assert.True(e.ChangeData.Mappings.Find(delegate (DumpedWordMapping m) { return m.WordIndex == 0 && m.Location == WordLocation.Content.Location; }) != null, "Mappings does not contain a word");
                        Assert.True(e.ChangeData.Mappings.Find(delegate (DumpedWordMapping m) { return m.WordIndex == 1 && m.Location == WordLocation.Content.Location; }) != null, "Mappings does not contain a word");
                        Assert.True(e.ChangeData.Mappings.Find(delegate (DumpedWordMapping m) { return m.WordIndex == 2 && m.Location == WordLocation.Content.Location; }) != null, "Mappings does not contain a word");
                        Assert.True(e.ChangeData.Mappings.Find(delegate (DumpedWordMapping m) { return m.WordIndex == 3 && m.Location == WordLocation.Content.Location; }) != null, "Mappings does not contain a word");
                        Assert.True(e.ChangeData.Mappings.Find(delegate (DumpedWordMapping m) { return m.WordIndex == 0 && m.Location == WordLocation.Title.Location; }) != null, "Mappings does not contain a word");
                        Assert.True(e.ChangeData.Mappings.Find(delegate (DumpedWordMapping m) { return m.WordIndex == 1 && m.Location == WordLocation.Title.Location; }) != null, "Mappings does not contain a word");
                    }
                };
            }

            Assert.Equal(2, sut.TotalDocuments);

            sut.RemoveDocument(doc1, null);
            Assert.Equal(1, sut.TotalDocuments);

            IDocument doc3 = MockDocument("Doc1", "Document 1", "ptdoc", DateTime.Now);
            sut.StoreDocument(doc3, null, "", null);
            Assert.Equal(2, sut.TotalDocuments);

            sut.RemoveDocument(doc1, null);
            Assert.Equal(1, sut.TotalDocuments);
        }

        [Fact]
        public void RemoveDocument_Document_Null()
        {
            IIndex sut = GetIndex();
            var ex = Assert.Throws<ArgumentNullException>(() => sut.RemoveDocument(null, null));
            Assert.Equal("Value cannot be null.\r\nParameter name: document", ex.Message);
        }

        private static bool AreDocumentsEqual(IDocument doc1, IDocument doc2)
        {
            return
                //doc1.ID == doc2.ID && // ID can be different (new instance / loaded from storage)
                doc1.Name == doc2.Name &&
                doc1.Title == doc2.Title &&
                doc1.TypeTag == doc2.TypeTag &&
                doc1.DateTime.ToString("yyMMddHHmmss") == doc2.DateTime.ToString("yyMMddHHmmss");
        }

        [Fact]
        public void Search_Basic()
        {
            IIndex sut = GetIndex();
            IInMemoryIndex imIndex = sut as IInMemoryIndex;

            // The mocked document has a default content
            IDocument doc1 = MockDocument("Doc1", "Document 1", "ptdoc", DateTime.Now);
            IDocument doc2 = MockDocument("Doc2", "Document 2", "ptdoc", DateTime.Now);
            IDocument doc3 = MockDocument("Doc3", "Document 3", "ptdoc", DateTime.Now);

            if (imIndex != null) imIndex.IndexChanged += AutoHandlerForDocumentStorage;

            sut.StoreDocument(doc1, null, "", null);
            sut.StoreDocument(doc2, null, "", null);
            sut.StoreDocument(doc3, null, "", null);

            SearchResultCollection res1 = sut.Search(new SearchParameters("specifications"));
            SearchResultCollection res2 = sut.Search(new SearchParameters("this"));

            Assert.Empty(res1);
            Assert.Equal(3, res2.Count);

            // Matches are in unpredictable order
            bool doc1Found = false, doc2Found = false, doc3Found = false;
            foreach (SearchResult r in res2)
            {
                if (AreDocumentsEqual(r.Document, doc1)) doc1Found = true;
                else if (AreDocumentsEqual(r.Document, doc2)) doc2Found = true;
                else if (AreDocumentsEqual(r.Document, doc3)) doc3Found = true;

                Assert.Single(r.Matches);
                Assert.Equal(0, r.Matches[0].FirstCharIndex);
                Assert.Equal(4, r.Matches[0].Text.Length);
                Assert.Equal(33.3333F, r.Relevance.Value, 2);
            }

            Assert.True(doc1Found, "Doc1 not found in results");
            Assert.True(doc2Found, "Doc2 not found in results");
            Assert.True(doc3Found, "Doc3 not found in results");
        }

        [Fact]
        public void Search_Basic_SingleResultWord()
        {
            IIndex sut = GetIndex();

            IDocument doc = MockDocument3("Doc", "Document", "ptdoc", DateTime.Now);

            if (sut is IInMemoryIndex imIndex) imIndex.IndexChanged += AutoHandlerForDocumentStorage;

            sut.StoreDocument(doc, null, "", null);

            SearchResultCollection res = sut.Search(new SearchParameters("todo"));

            Assert.Single(res);
            Assert.True(AreDocumentsEqual(doc, res[0].Document));
            Assert.Equal(100, res[0].Relevance.Value);
            Assert.Single(res[0].Matches);
            Assert.Equal(0, res[0].Matches[0].FirstCharIndex);
            Assert.Equal(0, res[0].Matches[0].WordIndex);
            Assert.Equal("todo", res[0].Matches[0].Text);
        }

        [Fact]
        public void Search_Basic_MultipleWords()
        {
            IIndex sut = GetIndex();
            IInMemoryIndex imIndex = sut as IInMemoryIndex;

            // The mocked document has a default content
            IDocument doc1 = MockDocument("Doc1", "Document 1", "ptdoc", DateTime.Now);

            if (imIndex != null) imIndex.IndexChanged += AutoHandlerForDocumentStorage;

            sut.StoreDocument(doc1, null, "", null);

            SearchResultCollection res = sut.Search(new SearchParameters("this content"));

            Assert.Single(res);
            Assert.Equal(100F, res[0].Relevance.Value, 2);
            Assert.True(AreDocumentsEqual(doc1, res[0].Document));
            Assert.Equal(2, res[0].Matches.Count);
            Assert.Equal(0, res[0].Matches[0].FirstCharIndex);
            Assert.Equal(4, res[0].Matches[0].Text.Length);
            Assert.Equal(13, res[0].Matches[1].FirstCharIndex);
            Assert.Equal(7, res[0].Matches[1].Text.Length);
        }

        [Fact]
        public void Search_Filtered()
        {
            IIndex sut = GetIndex();
            IInMemoryIndex imIndex = sut as IInMemoryIndex;

            // The mocked document has a default content
            IDocument doc1 = MockDocument("Doc1", "Document 1", "ptdoc", DateTime.Now);
            IDocument doc2 = MockDocument("Doc2", "Document 2", "htmldoc", DateTime.Now);
            IDocument doc3 = MockDocument("Doc3", "Document 3", "odoc", DateTime.Now);

            if (imIndex != null) imIndex.IndexChanged += AutoHandlerForDocumentStorage;

            sut.StoreDocument(doc1, null, "", null);
            sut.StoreDocument(doc2, null, "", null);
            sut.StoreDocument(doc3, null, "", null);

            SearchResultCollection res = sut.Search(new SearchParameters("this", "ptdoc", "htmldoc"));

            Assert.Equal(2, res.Count);

            // Matches are in unpredictable order
            bool doc1Found = false, doc2Found = false, doc3Found = false;
            foreach (SearchResult r in res)
            {
                if (AreDocumentsEqual(r.Document, doc1)) doc1Found = true;
                else if (AreDocumentsEqual(r.Document, doc2)) doc2Found = true;
                else if (AreDocumentsEqual(r.Document, doc3)) doc3Found = true;

                Assert.Single(r.Matches);
                Assert.Equal(0, r.Matches[0].FirstCharIndex);
                Assert.Equal(4, r.Matches[0].Text.Length);
                Assert.Equal(50F, r.Relevance.Value, 2);
            }

            Assert.True(doc1Found, "Doc1 not found in results");
            Assert.True(doc2Found, "Doc2 not found in results");
            Assert.False(doc3Found, "Doc3 found in results");
        }

        [Fact]
        public void Search_Filtered_DocumentTypeTags_Empty()
        {
            IIndex sut = GetIndex();
            var ex = Assert.Throws<ArgumentException>(() => sut.Search(new SearchParameters("hello", new string[0])));

            Assert.Equal("DocumentTypeTags cannot be empty.\r\nParameter name: documentTypeTags", ex.Message);
        }

        [Fact]
        public void Search_WithOptions_AtLeastOneWord()
        {
            IIndex sut = GetIndex();
            IInMemoryIndex imIndex = sut as IInMemoryIndex;

            IDocument doc1 = MockDocument("Doc1", "Document 1", "ptdoc", DateTime.Now);

            if (imIndex != null) imIndex.IndexChanged += AutoHandlerForDocumentStorage;

            sut.StoreDocument(doc1, null, "", null);

            SearchResultCollection res = sut.Search(new SearchParameters("this content", SearchOptions.AllWords));

            Assert.Single(res);
            Assert.Equal(0, res[0].Matches[0].FirstCharIndex);
            Assert.Equal(4, res[0].Matches[0].Text.Length);
            Assert.Equal(13, res[0].Matches[1].FirstCharIndex);
            Assert.Equal(7, res[0].Matches[1].Text.Length);

            res = sut.Search(new SearchParameters("this stuff", SearchOptions.AtLeastOneWord));

            Assert.Single(res);
        }

        [InlineData("content", 1)]
        [InlineData("this content", 1)]
        [InlineData("this stuff", 0)]
        [InlineData("blah", 0)]
        [Theory]
        public void Search_WithOptions_AllWords(string query, int results)
        {
            IIndex sut = GetIndex();
            IInMemoryIndex imIndex = sut as IInMemoryIndex;

            IDocument doc1 = MockDocument("Doc1", "Document 1", "ptdoc", DateTime.Now);

            if (imIndex != null) imIndex.IndexChanged += AutoHandlerForDocumentStorage;

            sut.StoreDocument(doc1, null, "", null);

            SearchResultCollection res = sut.Search(new SearchParameters(query, SearchOptions.AllWords));

            Assert.Equal(results, res.Count);
        }

        [InlineData("content", 1)]
        [InlineData("this is some content", 1)]
        [InlineData("THIS SOME content is", 0)]
        [InlineData("this is test content", 0)]
        [InlineData("blah", 0)]
        [Theory]
        public void Search_WithOptions_ExactPhrase(string query, int results)
        {
            IIndex sut = GetIndex();
            IInMemoryIndex imIndex = sut as IInMemoryIndex;

            IDocument doc1 = MockDocument("Doc1", "Document 1", "ptdoc", DateTime.Now);

            if (imIndex != null) imIndex.IndexChanged += AutoHandlerForDocumentStorage;

            sut.StoreDocument(doc1, null, "", null);

            SearchResultCollection res = sut.Search(new SearchParameters(query, SearchOptions.ExactPhrase));

            Assert.Equal(results, res.Count);
        }

        [Fact]
        public void Search_ExactPhrase()
        {
            IIndex sut = GetIndex();
            IInMemoryIndex imIndex = sut as IInMemoryIndex;

            IDocument doc = MockDocument4("Doc", "Document", "ptdoc", DateTime.Now);

            if (imIndex != null) imIndex.IndexChanged += AutoHandlerForDocumentStorage;

            sut.StoreDocument(doc, null, "", null);

            SearchResultCollection res = sut.Search(new SearchParameters("content repeated content blah blah", SearchOptions.ExactPhrase));
            Assert.Empty(res);

            res = sut.Search(new SearchParameters("repeated content", SearchOptions.ExactPhrase));
            Assert.Single(res);
        }

        [Fact]
        public void Search_Basic_Location()
        {
            IIndex sut = GetIndex();
            IInMemoryIndex imIndex = sut as IInMemoryIndex;

            IDocument doc1 = MockDocument("Doc1", "Document 1", "ptdoc", DateTime.Now);

            if (imIndex != null) imIndex.IndexChanged += AutoHandlerForDocumentStorage;

            sut.StoreDocument(doc1, new string[] { "development" }, "", null);

            SearchResultCollection res = sut.Search(new SearchParameters("document"));

            Assert.Single(res);
            Assert.Single(res[0].Matches);
            Assert.Equal(WordLocation.Title, res[0].Matches[0].Location);

            res = sut.Search(new SearchParameters("content"));

            Assert.Single(res);
            Assert.Single(res[0].Matches);
            Assert.Equal(WordLocation.Content, res[0].Matches[0].Location);

            res = sut.Search(new SearchParameters("development"));

            Assert.Single(res);
            Assert.Single(res[0].Matches);
            Assert.Equal(WordLocation.Keywords, res[0].Matches[0].Location);

            IDocument doc2 = MockDocument2("Doc2", "Text 2", "ptdoc", DateTime.Now);
            sut.StoreDocument(doc2, new string[] { "document" }, "", null);

            res = sut.Search(new SearchParameters("document"));

            Assert.Equal(2, res.Count);
            Assert.Single(res[0].Matches);
            Assert.Single(res[1].Matches);
        }

        [Fact]
        public void Search_Basic_LocationRelevance_1()
        {
            IIndex sut = GetIndex();
            IInMemoryIndex imIndex = sut as IInMemoryIndex;

            IDocument doc1 = MockDocument("Doc1", "Document 1", "ptdoc", DateTime.Now);
            IDocument doc2 = MockDocument2("Doc2", "Text 2", "ptdoc", DateTime.Now);

            if (imIndex != null) imIndex.IndexChanged += AutoHandlerForDocumentStorage;

            sut.StoreDocument(doc1, null, "", null);
            sut.StoreDocument(doc2, null, "", null);

            // "dummy" is only present in the content of doc2
            // "document" is only present in the title of doc1
            SearchResultCollection res = sut.Search(new SearchParameters("dummy document"));

            Assert.Equal(2, res.Count);
            Assert.Single(res[0].Matches);
            Assert.Single(res[1].Matches);
            foreach (SearchResult r in res)
            {
                if (r.Matches[0].Location == WordLocation.Content) Assert.Equal(33.3, r.Relevance.Value, 1);
                else if (r.Matches[0].Location == WordLocation.Title) Assert.Equal(66.7, r.Relevance.Value, 1);
            }
        }

        [Fact]
        public void Search_Basic_LocationRelevance_2()
        {
            IIndex sut = GetIndex();
            IInMemoryIndex imIndex = sut as IInMemoryIndex;

            IDocument doc1 = MockDocument("Doc1", "Document 1", "ptdoc", DateTime.Now);
            IDocument doc2 = MockDocument2("Doc2", "Text 2", "ptdoc", DateTime.Now);

            if (imIndex != null) imIndex.IndexChanged += AutoHandlerForDocumentStorage;

            sut.StoreDocument(doc1, null, "", null);
            sut.StoreDocument(doc2, new string[] { "blah" }, "", null);

            // "blah" is only present in the keywords of doc2
            // "content" is only present in the content of doc1
            SearchResultCollection res = sut.Search(new SearchParameters("content blah"));

            Assert.Equal(2, res.Count);
            Assert.Single(res[0].Matches);
            Assert.Single(res[1].Matches);
            foreach (SearchResult r in res)
            {
                if (r.Matches[0].Location == WordLocation.Content) Assert.Equal(40.0, r.Relevance.Value, 1);
                else if (r.Matches[0].Location == WordLocation.Keywords) Assert.Equal(60.0, r.Relevance.Value, 1);
            }
        }

        [Fact]
        public void Search_Basic_LocationRelevance_3()
        {
            IIndex sut = GetIndex();
            IInMemoryIndex imIndex = sut as IInMemoryIndex;

            IDocument doc1 = MockDocument("Doc1", "Document 1", "ptdoc", DateTime.Now);
            IDocument doc2 = MockDocument2("Doc2", "Text 2", "ptdoc", DateTime.Now);

            if (imIndex != null) imIndex.IndexChanged += AutoHandlerForDocumentStorage;

            sut.StoreDocument(doc1, null, "", null);
            sut.StoreDocument(doc2, new string[] { "blah" }, "", null);

            // "blah" is only present in the keywords of doc2
            // "document" is only present in the title of doc1
            SearchResultCollection res = sut.Search(new SearchParameters("document blah"));

            Assert.Equal(2, res.Count);
            Assert.Single(res[0].Matches);
            Assert.Single(res[1].Matches);
            foreach (SearchResult r in res)
            {
                if (r.Matches[0].Location == WordLocation.Keywords) Assert.Equal(42.9, r.Relevance.Value, 1);
                else if (r.Matches[0].Location == WordLocation.Title) Assert.Equal(57.1, r.Relevance.Value, 1);
            }
        }
    }
}
