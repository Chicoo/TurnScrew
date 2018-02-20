using System;
using Xunit;

namespace TurnScrew.Wiki.SearchEngine.UnitTests
{
    public class SearchResultCollectionTests : TestsBase
    {
        [Fact]
        public void Constructor_NoCapacity()
        {
            SearchResultCollection sut = new SearchResultCollection();
            Assert.Empty(sut);
        }

        [Fact]
        public void Constructor_WithCapacity()
        {
            SearchResultCollection sut = new SearchResultCollection(15);
            Assert.Empty(sut);
            Assert.Equal(15, sut.Capacity);
        }

        [Fact]
        public void Constructor_Capacity_Empty()
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => new SearchResultCollection(0));
            Assert.Equal("Invalid capacity.\r\nParameter name: capacity", ex.Message);
        }

        [Fact]
        public void AddAndCount()
        {
            SearchResultCollection sut = new SearchResultCollection();

            Assert.Empty(sut);

            SearchResult res = new SearchResult(MockDocument("d", "d", "d", DateTime.Now));
            SearchResult res2 = new SearchResult(MockDocument("d2", "d", "d", DateTime.Now));

            sut.Add(res);
            sut.Add(res2);
            Assert.Equal(2, sut.Count);
            Assert.Equal(res, sut[0]);
            Assert.Equal(res2, sut[1]);
        }

        [Fact]
        public void Add_Item_Null()
        {
            SearchResultCollection sut = new SearchResultCollection();
            var ex = Assert.Throws<ArgumentNullException>(() => sut.Add(null));
            Assert.Equal("Value cannot be null.\r\nParameter name: item", ex.Message);
        }

        [Fact]
        public void Add_Item_Duplicate()
        {
            SearchResultCollection sut = new SearchResultCollection();

            SearchResult res = new SearchResult(MockDocument("d", "d", "d", DateTime.Now));
            SearchResult res2 = new SearchResult(MockDocument("d2", "d", "d", DateTime.Now));

            sut.Add(res);
            sut.Add(res2);
            var ex = Assert.Throws<ArgumentException>(() => sut.Add(res));
            Assert.Equal("Item is already present in the collection.\r\nParameter name: item", ex.Message);
        }

        [Fact]
        public void Clear()
        {
            SearchResultCollection sut = new SearchResultCollection();

            SearchResult res = new SearchResult(MockDocument("d", "d", "d", DateTime.Now));

            sut.Add(res);
            Assert.Single(sut);

            sut.Clear();
            Assert.Empty(sut);
        }

        [Fact]
        public void Contains()
        {
            SearchResultCollection sut = new SearchResultCollection();

            SearchResult res = new SearchResult(MockDocument("d", "d", "d", DateTime.Now));
            SearchResult res2 = new SearchResult(MockDocument("d2", "d", "d", DateTime.Now));

            sut.Add(res);
            Assert.True(sut.Contains(res), "Collection should contain item");
            Assert.False(sut.Contains(res2), "Collection should not contain item");

            Assert.False(sut.Contains(null), "Contains should return false");
        }

        [Fact]
        public void GetSearchResult()
        {
            SearchResultCollection sut = new SearchResultCollection();

            IDocument doc1 = MockDocument("d", "d", "d", DateTime.Now);
            IDocument doc2 = MockDocument("d2", "d", "d", DateTime.Now);
            IDocument doc3 = MockDocument("d3", "d", "d", DateTime.Now);
            SearchResult res = new SearchResult(doc1);
            SearchResult res2 = new SearchResult(doc2);

            sut.Add(res);
            sut.Add(res2);

            Assert.Equal(res, sut.GetSearchResult(doc1));
            Assert.Null(sut.GetSearchResult(doc3));
        }

        [Fact]
        public void GetSearchResult_Document_Null()
        {
            SearchResultCollection sut = new SearchResultCollection();
            var ex = Assert.Throws<ArgumentNullException>(() => sut.GetSearchResult(null));
            Assert.Equal("Value cannot be null.\r\nParameter name: document", ex.Message);
        }

        [Fact]
        public void CopyTo()
        {
            SearchResultCollection sut = new SearchResultCollection();

            SearchResult res = new SearchResult(MockDocument("d", "d", "d", DateTime.Now));
            SearchResult res2 = new SearchResult(MockDocument("d2", "d", "d", DateTime.Now));

            sut.Add(res);
            sut.Add(res2);

            SearchResult[] results = new SearchResult[2];
            sut.CopyTo(results, 0);

            Assert.Equal(res, results[0]);
            Assert.Equal(res2, results[1]);

            results = new SearchResult[3];
            sut.CopyTo(results, 0);

            Assert.Equal(res, results[0]);
            Assert.Equal(res2, results[1]);
            Assert.Null(results[2]);

            results = new SearchResult[3];
            sut.CopyTo(results, 1);

            Assert.Null(results[0]);
            Assert.Equal(res, results[1]);
            Assert.Equal(res2, results[2]);
        }

        [Fact]
        public void CopyTo_Array_Null()
        {
            SearchResultCollection sut = new SearchResultCollection();
            var ex = Assert.Throws<ArgumentNullException>(() => sut.CopyTo(null, 0));
            Assert.Equal("Value cannot be null.\r\nParameter name: array", ex.Message);
        }

        [Fact]
        public void CopyTo_Index_Negative()
        {
            SearchResultCollection sut = new SearchResultCollection();
            SearchResult[] results = new SearchResult[10];

            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => sut.CopyTo(results, -1));
            Assert.Equal("Index should be greater than or equal to zero and less than the number of items in the array.\r\nParameter name: arrayIndex", ex.Message);
        }

        [Fact]
        public void CopyTo_Index_TooBig()
        {
            SearchResultCollection sut = new SearchResultCollection();
            SearchResult[] results = new SearchResult[10];

            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => sut.CopyTo(results, 10));
            Assert.Equal("Index should be greater than or equal to zero and less than the number of items in the array.\r\nParameter name: arrayIndex", ex.Message);
        }

        [Fact]
        public void CopyTo_Index_TooSmall()
        {
            SearchResultCollection sut = new SearchResultCollection();
            SearchResult res = new SearchResult(MockDocument("d", "d", "d", DateTime.Now));
            SearchResult res2 = new SearchResult(MockDocument("d2", "d", "d", DateTime.Now));

            sut.Add(res);
            sut.Add(res2);

            SearchResult[] results = new SearchResult[1];

            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => sut.CopyTo(results, 0));
            Assert.Equal("Not enough space for copying the items starting at the specified index.\r\nParameter name: arrayIndex", ex.Message);
        }

        [Fact]
        public void CopyTo_Index_NoSpace()
        {
            SearchResultCollection sut = new SearchResultCollection();
            SearchResult res = new SearchResult(MockDocument("d", "d", "d", DateTime.Now));
            SearchResult res2 = new SearchResult(MockDocument("d2", "d", "d", DateTime.Now));

            sut.Add(res);
            sut.Add(res2);

            SearchResult[] results = new SearchResult[2];

            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => sut.CopyTo(results, 1));
            Assert.Equal("Not enough space for copying the items starting at the specified index.\r\nParameter name: arrayIndex", ex.Message);
        }

        [Fact]
        public void ReadOnly()
        {
            SearchResultCollection collection = new SearchResultCollection();
            Assert.False(collection.IsReadOnly);
        }

        [Fact]
        public void Remove()
        {
            SearchResultCollection collection = new SearchResultCollection();

            SearchResult res = new SearchResult(MockDocument("d", "d", "d", DateTime.Now));
            SearchResult res2 = new SearchResult(MockDocument("d2", "d", "d", DateTime.Now));
            SearchResult res3 = new SearchResult(MockDocument("d3", "d", "d", DateTime.Now));

            collection.Add(res);
            collection.Add(res2);

            Assert.True(collection.Remove(res), "Remove should return true");
            Assert.False(collection.Remove(res3), "Remove should return false");
            Assert.Single(collection);
            Assert.Equal(res2, collection[0]);

        }

        [Fact]
        public void Remove_Item_Null()
        {
            SearchResultCollection sut = new SearchResultCollection();
            var ex = Assert.Throws<ArgumentNullException>(() => sut.Remove(null));
            Assert.Equal("Value cannot be null.\r\nParameter name: item", ex.Message);
        }

        [Fact]
        public void GetEnumerator()
        {
            SearchResultCollection collection = new SearchResultCollection();

            SearchResult res = new SearchResult(MockDocument("d", "d", "d", DateTime.Now));
            SearchResult res2 = new SearchResult(MockDocument("d2", "d", "d", DateTime.Now));

            collection.Add(res);
            collection.Add(res2);

            int count = 0;
            foreach (SearchResult r in collection)
            {
                count++;
            }
            Assert.Equal(2, count);
        }

        [Fact]
        public void Indexer()
        {
            SearchResultCollection collection = new SearchResultCollection();

            SearchResult res = new SearchResult(MockDocument("d", "d", "d", DateTime.Now));
            SearchResult res2 = new SearchResult(MockDocument("d2", "d", "d", DateTime.Now));

            collection.Add(res);
            collection.Add(res2);

            Assert.Equal(res, collection[0]);
            Assert.Equal(res2, collection[1]);
        }

        [Fact]
        public void Indexer_Index_Negative()
        {
            SearchResultCollection sut = new SearchResultCollection();

            var ex = Assert.Throws<IndexOutOfRangeException>(() => sut[-1]);
            Assert.Equal("Index should be greater than or equal to zero and less than the number of items in the collection.", ex.Message);
        }

        [Fact]
        public void Indexer_Index_TooBig()
        {
            SearchResultCollection sut = new SearchResultCollection();

            var ex = Assert.Throws<IndexOutOfRangeException>(() => sut[1]);
            Assert.Equal("Index should be greater than or equal to zero and less than the number of items in the collection.", ex.Message);
        }
    }
}
