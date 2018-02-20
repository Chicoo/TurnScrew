using System;
using Xunit;

namespace TurnScrew.Wiki.SearchEngine.UnitTests {

	
	public class WordTests : TestsBase {

		[Fact]
		public void Constructor_2Params() {
			Word sut = new Word(1, "Hello");
			Assert.Equal("hello", sut.Text);
			Assert.Empty(sut.Occurrences);
			Assert.Equal(0, sut.TotalOccurrences);
		}

		[Fact]
		public void Constructor_3Params_NoOccurrences() {
			Word sut = new Word(1, "Hello1", new OccurrenceDictionary());
			Assert.Equal("hello1", sut.Text);
			Assert.Empty(sut.Occurrences);
			Assert.Equal(0, sut.TotalOccurrences);
		}

		[Fact]
		public void Constructor_3Params_1Occurrence() {
			OccurrenceDictionary occ = new OccurrenceDictionary();

			IDocument doc = MockDocument("Doc", "Doc", "d", DateTime.Now);

			SortedBasicWordInfoSet set = new SortedBasicWordInfoSet();
			set.Add(new BasicWordInfo(0, 0, WordLocation.Content));
			occ.Add(doc, set);

			Word sut = new Word(12, "Hello", occ);
			Assert.Equal("hello", sut.Text);
			Assert.Single(sut.Occurrences);
			Assert.Equal(1, sut.TotalOccurrences);
		}

        [Fact]
        public void Constructor_Text_Null()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new Word(1, null));
            Assert.Equal("Value cannot be null.\r\nParameter name: text", ex.Message);
        }

        [Fact]
        public void Constructor_Text_Empty()
        {
            var ex = Assert.Throws<ArgumentException>(() => new Word(1, ""));
            Assert.Equal("Text must contain at least one character.\r\nParameter name: text", ex.Message);
        }

        [Fact]
        public void Constructor_Occurences_Null()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new Word(1, "hello", null));
            Assert.Equal("Value cannot be null.\r\nParameter name: occurrences", ex.Message);
        }

        [Fact]
		public void Add1Occurrence() {
			Word sut = new Word(1, "hello");

			IDocument doc = MockDocument("Doc", "Doc", "d", DateTime.Now);

			sut.AddOccurrence(doc, 0, 0, WordLocation.Content);
			Assert.Single(sut.Occurrences);
			Assert.Equal(1, sut.TotalOccurrences);
			Assert.Equal(0, sut.Occurrences[doc][0].FirstCharIndex);
			Assert.Equal(0, sut.Occurrences[doc][0].WordIndex);
		}

		[Fact]
		public void Add2Occurrences_DifferentDocuments() {
			Word sut = new Word(1, "hello");

			IDocument doc1 = MockDocument("Doc1", "Doc1", "d", DateTime.Now);
			IDocument doc2 = MockDocument("Doc2", "Doc2", "d", DateTime.Now);
			sut.AddOccurrence(doc1, 0, 0, WordLocation.Content);
			sut.AddOccurrence(doc2, 10, 1, WordLocation.Content);
			Assert.Equal(2, sut.Occurrences.Count);
			Assert.Equal(2, sut.TotalOccurrences);
			Assert.Equal(0, sut.Occurrences[doc1][0].FirstCharIndex);
			Assert.Equal(0, sut.Occurrences[doc1][0].WordIndex);
			Assert.Equal(10, sut.Occurrences[doc2][0].FirstCharIndex);
			Assert.Equal(1, sut.Occurrences[doc2][0].WordIndex);
		}

		[Fact]
		public void Add2Occurrences_SameDocument() {
			Word sut = new Word(1, "hello");

			IDocument doc = MockDocument("Doc", "Doc", "d", DateTime.Now);
			sut.AddOccurrence(doc, 0, 0, WordLocation.Content);
			sut.AddOccurrence(doc, 10, 1, WordLocation.Content);
			Assert.Single(sut.Occurrences);
			Assert.Equal(2, sut.TotalOccurrences);
			Assert.Equal(0, sut.Occurrences[doc][0].FirstCharIndex);
			Assert.Equal(0, sut.Occurrences[doc][0].WordIndex);
			Assert.Equal(10, sut.Occurrences[doc][1].FirstCharIndex);
			Assert.Equal(1, sut.Occurrences[doc][1].WordIndex);
		}

        [Fact]
        public void AddOccurrence_Document_Null()
        {
            Word sut = new Word(1, "dummy");
            var ex = Assert.Throws<ArgumentNullException>(() => sut.AddOccurrence(null, 0, 0, WordLocation.Content));
            Assert.Equal("Value cannot be null.\r\nParameter name: document", ex.Message);
        }

        [Fact]
		public void RemoveOccurrences() {
			Word sut = new Word(1, "hello");

			IDocument doc1 = MockDocument("Doc1", "Doc1", "d", DateTime.Now);
			IDocument doc2 = MockDocument("Doc2", "Doc2", "d", DateTime.Now);
			sut.AddOccurrence(doc1, 0, 0, WordLocation.Content);
			sut.AddOccurrence(doc1, 10, 1, WordLocation.Content);
			sut.AddOccurrence(doc2, 5, 0, WordLocation.Content);
			Assert.Equal(2, sut.Occurrences.Count);
			Assert.Equal(3, sut.TotalOccurrences);

			sut.RemoveOccurrences(doc1);

			Assert.Single(sut.Occurrences);
			Assert.Equal(1, sut.TotalOccurrences);
			Assert.Equal(5, sut.Occurrences[doc2][0].FirstCharIndex);
			Assert.Equal(0, sut.Occurrences[doc2][0].WordIndex);
		}

        [Fact]
        public void RemoveOccurrence_Document_Null()
        {
            Word sut = new Word(1, "hey");
            var ex = Assert.Throws<ArgumentNullException>(() => sut.RemoveOccurrences(null));
            Assert.Equal("Value cannot be null.\r\nParameter name: document", ex.Message);
        }
        
        [Fact]
		public void BulkAddOccurrences_NewDocument() {
			Word sut = new Word(1, "hello");
			IDocument doc0 = MockDocument("Doc0", "Doc0", "d", DateTime.Now);
			sut.AddOccurrence(doc0, 10, 0, WordLocation.Content);
			Assert.Equal(10, sut.Occurrences[doc0][0].FirstCharIndex);
			Assert.Equal(0, sut.Occurrences[doc0][0].WordIndex);

			SortedBasicWordInfoSet set = new SortedBasicWordInfoSet();
			set.Add(new BasicWordInfo(10, 0, WordLocation.Content));
			set.Add(new BasicWordInfo(25, 1, WordLocation.Content));
			set.Add(new BasicWordInfo(102, 2, WordLocation.Content));
			IDocument doc = MockDocument("Doc", "Doc", "d", DateTime.Now);
			sut.BulkAddOccurrences(doc, set);

			Assert.Equal(2, sut.Occurrences.Count);
			Assert.Equal(4, sut.TotalOccurrences);
			Assert.Equal(10, sut.Occurrences[doc0][0].FirstCharIndex);
			Assert.Equal(0, sut.Occurrences[doc0][0].WordIndex);
			Assert.Equal(10, sut.Occurrences[doc][0].FirstCharIndex);
			Assert.Equal(0, sut.Occurrences[doc][0].WordIndex);
			Assert.Equal(25, sut.Occurrences[doc][1].FirstCharIndex);
			Assert.Equal(1, sut.Occurrences[doc][1].WordIndex);
			Assert.Equal(102, sut.Occurrences[doc][2].FirstCharIndex);
			Assert.Equal(2, sut.Occurrences[doc][2].WordIndex);
		}

		[Fact]
		public void BulkAddOccurrences_ExistingDocument() {
			Word sut = new Word(1, "hello");
			IDocument doc0 = MockDocument("Doc0", "Doc0", "d", DateTime.Now);
			sut.AddOccurrence(doc0, 10, 0, WordLocation.Content);
			Assert.Equal(10, sut.Occurrences[doc0][0].FirstCharIndex);
			Assert.Equal(0, sut.Occurrences[doc0][0].WordIndex);

			IDocument doc = MockDocument("Doc", "Doc", "d", DateTime.Now);
			sut.AddOccurrence(doc, 0, 0, WordLocation.Content);
			Assert.Equal(2, sut.Occurrences.Count);
			Assert.Equal(2, sut.TotalOccurrences);

			SortedBasicWordInfoSet set = new SortedBasicWordInfoSet();
			set.Add(new BasicWordInfo(10, 0, WordLocation.Content));
			set.Add(new BasicWordInfo(25, 1, WordLocation.Content));
			set.Add(new BasicWordInfo(102, 2, WordLocation.Content));
			sut.BulkAddOccurrences(doc, set);

			Assert.Equal(2, sut.Occurrences.Count);
			Assert.Equal(4, sut.TotalOccurrences);
			Assert.Equal(10, sut.Occurrences[doc0][0].FirstCharIndex);
			Assert.Equal(0, sut.Occurrences[doc0][0].WordIndex);
			Assert.Equal(10, sut.Occurrences[doc][0].FirstCharIndex);
			Assert.Equal(0, sut.Occurrences[doc][0].WordIndex);
			Assert.Equal(25, sut.Occurrences[doc][1].FirstCharIndex);
			Assert.Equal(1, sut.Occurrences[doc][1].WordIndex);
			Assert.Equal(102, sut.Occurrences[doc][2].FirstCharIndex);
			Assert.Equal(2, sut.Occurrences[doc][2].WordIndex);
		}

		[Fact]
		public void BulkAddOccurrences_ExistingDocument_EmptyPositionsSet() {
			Word sut = new Word(1, "hello");
			IDocument doc0 = MockDocument("Doc0", "Doc0", "d", DateTime.Now);
			sut.AddOccurrence(doc0, 10, 0, WordLocation.Content);
			Assert.Equal(10, sut.Occurrences[doc0][0].FirstCharIndex);
			Assert.Equal(0, sut.Occurrences[doc0][0].WordIndex);

			IDocument doc = MockDocument("Doc", "Doc", "d", DateTime.Now);
			sut.AddOccurrence(doc, 0, 0, WordLocation.Content);
			Assert.Equal(2, sut.Occurrences.Count);
			Assert.Equal(2, sut.TotalOccurrences);

			SortedBasicWordInfoSet set = new SortedBasicWordInfoSet();
			sut.BulkAddOccurrences(doc, set);

			Assert.Single(sut.Occurrences);
			Assert.Equal(1, sut.TotalOccurrences);
			Assert.Equal(10, sut.Occurrences[doc0][0].FirstCharIndex);
			Assert.Equal(0, sut.Occurrences[doc0][0].WordIndex);
		}

		[Fact]
        public void BulkAddOccurrences_Document_Null()
        {
            Word sut = new Word(1, "john");
            var ex = Assert.Throws<ArgumentNullException>(() => sut.BulkAddOccurrences(null, new SortedBasicWordInfoSet()));
            Assert.Equal("Value cannot be null.\r\nParameter name: document", ex.Message);
        }

        [Fact]
        public void BulkAddOccurrences_Positions_Null()
        {
            Word sut = new Word(1, "john");
            var ex = Assert.Throws<ArgumentNullException>("positions", () => sut.BulkAddOccurrences(MockDocument("Doc", "Doc", "d", DateTime.Now), null));
        }
    }

}
