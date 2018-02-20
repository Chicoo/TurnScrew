using System;
using Xunit;

namespace TurnScrew.Wiki.SearchEngine.UnitTests
{
    public class WordInfoCollectionTests
    {
        [Fact]
        public void Constructor_NoCapacity()
        {
            WordInfoCollection sut = new WordInfoCollection();
            Assert.Empty(sut);
        }

        [Fact]
        public void Constructor_WithCapacity()
        {
            WordInfoCollection sut = new WordInfoCollection(15);
            Assert.Empty(sut);
            Assert.Equal(15, sut.Capacity);
        }

        [Fact]
        public void Constructor_Capacity_Empty()
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => new WordInfoCollection(0));
            Assert.Equal("Invalid capacity.\r\nParameter name: capacity", ex.Message);
        }

        [Fact]
        public void AddAndCount()
        {
            WordInfoCollection sut = new WordInfoCollection();

            WordInfo mi1 = new WordInfo("continuous", 0, 0, WordLocation.Content);
            WordInfo mi2 = new WordInfo("taskbar", 21, 1, WordLocation.Content);

            Assert.Empty(sut);
            sut.Add(mi2);
            sut.Add(mi1);
            Assert.Equal(2, sut.Count);
            Assert.Equal(mi1, sut[0]);
            Assert.Equal(mi2, sut[1]);
        }

        [Fact]
        public void Add_Item_Null()
        {
            WordInfoCollection sut = new WordInfoCollection();
            var ex = Assert.Throws<ArgumentNullException>(() => sut.Add(null));
            Assert.Equal("Value cannot be null.\r\nParameter name: item", ex.Message);
        }

        [Fact]
        public void Clear()
        {
            WordInfoCollection sut = new WordInfoCollection();

            WordInfo mi1 = new WordInfo("continuous", 0, 0, WordLocation.Content);
            WordInfo mi2 = new WordInfo("taskbar", 21, 1, WordLocation.Content);

            sut.Add(mi1);
            sut.Add(mi2);
            Assert.Equal(2, sut.Count);

            sut.Clear();
            Assert.Empty(sut);
        }

        [Fact]
        public void Contains_WordInfo()
        {
            WordInfoCollection sut = new WordInfoCollection();

            WordInfo mi1 = new WordInfo("continuous", 0, 0, WordLocation.Content);
            WordInfo mi2 = new WordInfo("taskbar", 21, 0, WordLocation.Content);

            sut.Add(mi1);

            Assert.True(sut.Contains(mi1), "Collection should contain item");
            Assert.False(sut.Contains(mi2), "Collection should not contain item");

            Assert.False(sut.Contains(null as WordInfo), "Contains should return false");
        }

        [Fact]
        public void Contains_String()
        {
            WordInfoCollection sut = new WordInfoCollection();

            WordInfo mi1 = new WordInfo("continuous", 0, 0, WordLocation.Content);

            sut.Add(mi1);

            Assert.True(sut.Contains("continuous"), "Collection should contain string");
            Assert.False(sut.Contains("taskbar"), "Collection should not contain string");

            Assert.False(sut.Contains(null as string), "Contains should return false");
            Assert.False(sut.Contains(""), "Contains should return false");
        }

        [Fact]
        public void ContainsOccurrence()
        {
            WordInfoCollection sut = new WordInfoCollection();

            WordInfo mi1 = new WordInfo("continuous", 7, 0, WordLocation.Content);

            sut.Add(mi1);

            Assert.True(sut.ContainsOccurrence("continuous", 7), "Collection should contain occurrence");
            Assert.False(sut.ContainsOccurrence("continuous2", 7), "Collection should not contain occurrence");
            Assert.False(sut.ContainsOccurrence("continuous", 6), "Collection should not contain occurrence");
            Assert.False(sut.ContainsOccurrence("continuous", 8), "Collection should not contain occurrence");
            Assert.False(sut.ContainsOccurrence("continuous2", 6), "Collection should not contain occurrence");

            Assert.False(sut.ContainsOccurrence("continuous2", -1), "Contains should return false");
            Assert.False(sut.ContainsOccurrence("", 7), "Contains should return false");
            Assert.False(sut.ContainsOccurrence(null, 7), "Contains should return false");
        }

        [Fact]
        public void CopyTo()
        {
            WordInfoCollection sut = new WordInfoCollection();

            WordInfo mi1 = new WordInfo("continuous", 0, 0, WordLocation.Content);
            WordInfo mi2 = new WordInfo("goose", 34, 0, WordLocation.Content);

            sut.Add(mi1);
            sut.Add(mi2);

            WordInfo[] matches = new WordInfo[2];
            sut.CopyTo(matches, 0);

            Assert.Equal(mi1, matches[0]);
            Assert.Equal(mi2, matches[1]);

            matches = new WordInfo[3];
            sut.CopyTo(matches, 0);

            Assert.Equal(mi1, matches[0]);
            Assert.Equal(mi2, matches[1]);
            Assert.Null(matches[2]);

            matches = new WordInfo[3];
            sut.CopyTo(matches, 1);

            Assert.Null(matches[0]);
            Assert.Equal(mi1, matches[1]);
            Assert.Equal(mi2, matches[2]);
        }

        [Fact]
        public void CopyTo_Array_Null()
        {
            WordInfoCollection sut = new WordInfoCollection();
            var ex = Assert.Throws<ArgumentNullException>(() => sut.CopyTo(null, 0));
            Assert.Equal("Value cannot be null.\r\nParameter name: array", ex.Message);
        }

        [Fact]
        public void CopyTo_Index_Negative()
        {
            WordInfoCollection sut = new WordInfoCollection();
            WordInfo[] results = new WordInfo[10];

            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => sut.CopyTo(results, -1));
            Assert.Equal("Index should be greater than or equal to zero and less than the number of items in the array.\r\nParameter name: arrayIndex", ex.Message);
        }

        [Fact]
        public void CopyTo_Index_TooBig()
        {
            WordInfoCollection sut = new WordInfoCollection();
            WordInfo[] results = new WordInfo[10];

            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => sut.CopyTo(results, 10));
            Assert.Equal("Index should be greater than or equal to zero and less than the number of items in the array.\r\nParameter name: arrayIndex", ex.Message);
        }

        [Fact]
        public void CopyTo_Index_TooSmall()
        {
            WordInfoCollection sut = new WordInfoCollection();
            WordInfo mi1 = new WordInfo("home", 0, 0, WordLocation.Content);
            WordInfo mi2 = new WordInfo("taskbar", 100, 0, WordLocation.Content);

            sut.Add(mi1);
            sut.Add(mi2);

            WordInfo[] matches = new WordInfo[1];

            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => sut.CopyTo(matches, 0));
            Assert.Equal("Not enough space for copying the items starting at the specified index.\r\nParameter name: arrayIndex", ex.Message);
        }

        [Fact]
        public void CopyTo_Index_NoSpace()
        {
            WordInfoCollection sut = new WordInfoCollection();

            WordInfo mi1 = new WordInfo("home", 0, 0, WordLocation.Content);
            WordInfo mi2 = new WordInfo("taskbar", 100, 0, WordLocation.Content);

            sut.Add(mi1);
            sut.Add(mi2);

            WordInfo[] matches = new WordInfo[2];

            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => sut.CopyTo(matches, 1));
            Assert.Equal("Not enough space for copying the items starting at the specified index.\r\nParameter name: arrayIndex", ex.Message);
        }

        [Fact]
        public void ReadOnly()
        {
            WordInfoCollection collection = new WordInfoCollection();
            Assert.False(collection.IsReadOnly, "Collection should not be read-only");
        }

        [Fact]
        public void Remove()
        {
            WordInfoCollection collection = new WordInfoCollection();

            WordInfo mi1 = new WordInfo("goose", 1, 0, WordLocation.Content);
            WordInfo mi2 = new WordInfo("hello", 12, 0, WordLocation.Content);

            collection.Add(mi1);
            Assert.True(collection.Contains(mi1), "Collection should contain item");
            Assert.False(collection.Contains(mi2), "Collection should not contain item");

            Assert.False(collection.Contains(null as WordInfo), "Contains should return false");
        }

        [Fact]
        public void Remove_Item_Null()
        {
            WordInfoCollection sut = new WordInfoCollection();
            var ex = Assert.Throws<ArgumentNullException>(() => sut.Remove(null));
            Assert.Equal("Value cannot be null.\r\nParameter name: item", ex.Message);
        }

        [Fact]
        public void GetEnumerator()
        {
            WordInfoCollection collection = new WordInfoCollection();

            WordInfo mi1 = new WordInfo("goose", 1, 0, WordLocation.Content);
            WordInfo mi2 = new WordInfo("hello", 12, 0, WordLocation.Content);

            collection.Add(mi2);
            collection.Add(mi1);

            int count = 0;
            foreach (WordInfo r in collection)
            {
                if (count == 0) Assert.Equal(mi1, r);
                if (count == 1) Assert.Equal(mi2, r);
                count++;
            }
            Assert.Equal(2, count);
        }

        [Fact]
        public void Indexer()
        {
            WordInfoCollection collection = new WordInfoCollection();

            WordInfo mi1 = new WordInfo("taskbar", 1, 0, WordLocation.Content);
            WordInfo mi2 = new WordInfo("goose", 12, 0, WordLocation.Content);

            collection.Add(mi2);
            collection.Add(mi1);

            Assert.Equal(mi1, collection[0]);
            Assert.Equal(mi2, collection[1]);
        }

        [Fact]
        public void Indexer_Index_Negative()
        {
            WordInfoCollection sut = new WordInfoCollection();

            var ex = Assert.Throws<IndexOutOfRangeException>(() => sut[-1]);
            Assert.Equal("Index should be greater than or equal to zero and less than the number of items in the collection.", ex.Message);
        }

        [Fact]
        public void Indexer_Index_TooBig()
        {
            WordInfoCollection sut = new WordInfoCollection();

            var ex = Assert.Throws<IndexOutOfRangeException>(() => sut[1]);
            Assert.Equal("Index should be greater than or equal to zero and less than the number of items in the collection.", ex.Message);
        }
    }
}
