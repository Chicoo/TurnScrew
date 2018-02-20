using System;
using Xunit;

namespace TurnScrew.Wiki.SearchEngine.UnitTests
{
    public class SortedBasicWordInfoSetTests : TestsBase
    {
        [Fact]
        public void Constructor_Default()
        {
            SortedBasicWordInfoSet sut = new SortedBasicWordInfoSet();
            Assert.Equal(0, sut.Count);
        }

        [Fact]
        public void Constructor_WithCapacity()
        {
            SortedBasicWordInfoSet sut = new SortedBasicWordInfoSet(10);
            Assert.Equal(0, sut.Count);
            Assert.Equal(10, sut.Capacity);
        }

        [Fact]
        public void Constructor_Capacity_Empty()
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => new SortedBasicWordInfoSet(0));
            Assert.Equal("Invalid capacity.\r\nParameter name: capacity", ex.Message);
        }

        [Fact]
        public void Add_NewItem()
        {
            SortedBasicWordInfoSet sut = new SortedBasicWordInfoSet();
            Assert.True(sut.Add(new BasicWordInfo(10, 1, WordLocation.Content)), "Add should return true (adding new item)");
            Assert.Equal(1, sut.Count);
        }

        [Fact]
        public void Add_ExistingItem()
        {
            SortedBasicWordInfoSet sut = new SortedBasicWordInfoSet();
            Assert.True(sut.Add(new BasicWordInfo(2, 0, WordLocation.Content)), "Add should return true (adding new item)");
            Assert.Equal(1, sut.Count);
            Assert.False(sut.Add(new BasicWordInfo(2, 0, WordLocation.Content)), "Add should return false (adding existing item)");
            Assert.Equal(1, sut.Count);
        }

        [Fact]
        public void Contains()
        {
            SortedBasicWordInfoSet sut = new SortedBasicWordInfoSet();
            Assert.False(sut.Contains(new BasicWordInfo(1, 0, WordLocation.Content)), "Contains should return false (inexistent item)");
            Assert.True(sut.Add(new BasicWordInfo(1, 0, WordLocation.Content)), "Add should return true (adding new item)");
            Assert.True(sut.Contains(new BasicWordInfo(1, 0, WordLocation.Content)), "Contains should return true (item exists)");
            Assert.Equal(1, sut.Count);
            Assert.False(sut.Contains(new BasicWordInfo(10, 2, WordLocation.Content)), "Contains should return false (inexistent item)");
        }

        [Fact]
        public void Remove()
        {
            SortedBasicWordInfoSet sut = new SortedBasicWordInfoSet();
            Assert.False(sut.Remove(new BasicWordInfo(1, 0, WordLocation.Content)), "Remove should return false (removing inexistent item");
            Assert.True(sut.Add(new BasicWordInfo(1, 0, WordLocation.Content)), "Add should return true (adding new item)");
            Assert.Equal(1, sut.Count);
            Assert.True(sut.Contains(new BasicWordInfo(1, 0, WordLocation.Content)), "Contains should return true (item exists)");
            Assert.True(sut.Remove(new BasicWordInfo(1, 0, WordLocation.Content)), "Remove should return true (removing existing item)");
            Assert.False(sut.Contains(new BasicWordInfo(1, 0, WordLocation.Content)), "Contains should return false (inexistent item)");
            Assert.Equal(0, sut.Count);
        }

        [Fact]
        public void Clear()
        {
            SortedBasicWordInfoSet sut = new SortedBasicWordInfoSet();
            Assert.True(sut.Add(new BasicWordInfo(10, 2, WordLocation.Content)), "Add should return true (adding new item)");
            Assert.True(sut.Add(new BasicWordInfo(2, 1, WordLocation.Content)), "Add should return true (adding new item)");
            Assert.Equal(2, sut.Count);
            sut.Clear();
            Assert.Equal(0, sut.Count);
            Assert.False(sut.Contains(new BasicWordInfo(10, 2, WordLocation.Content)), "Contains should return false (empty set)");
            Assert.False(sut.Contains(new BasicWordInfo(2, 1, WordLocation.Content)), "Contains should return false (empty set)");
        }

        [Fact]
        public void GetEnumerator()
        {
            SortedBasicWordInfoSet sut = new SortedBasicWordInfoSet();
            Assert.True(sut.Add(new BasicWordInfo(1, 0, WordLocation.Content)), "Add should return true (adding new item)");
            Assert.True(sut.Add(new BasicWordInfo(3, 1, WordLocation.Content)), "Add should return true (adding new item)");
            Assert.Equal(2, sut.Count);
            int count = 0;
            foreach (BasicWordInfo item in sut)
            {
                if (count == 0) Assert.Equal(1, item.FirstCharIndex);
                if (count == 0) Assert.Equal(0, item.WordIndex);
                if (count == 1) Assert.Equal(3, item.FirstCharIndex);
                if (count == 1) Assert.Equal(1, item.WordIndex);
                count++;
            }
            Assert.Equal(2, count);
        }

        [Fact]
        public void Indexer()
        {
            SortedBasicWordInfoSet sut = new SortedBasicWordInfoSet();
            Assert.True(sut.Add(new BasicWordInfo(1, 0, WordLocation.Content)), "Add should return true (adding new item)");
            Assert.True(sut.Add(new BasicWordInfo(10, 1, WordLocation.Content)), "Add should return true (adding new item)");
            Assert.True(sut.Add(new BasicWordInfo(3, 2, WordLocation.Content)), "Add should return true (adding new item)");
            Assert.Equal(3, sut.Count);
            Assert.Equal(1, sut[0].FirstCharIndex);
            Assert.Equal(0, sut[0].WordIndex);
            Assert.Equal(10, sut[1].FirstCharIndex);
            Assert.Equal(1, sut[1].WordIndex);
            Assert.Equal(3, sut[2].FirstCharIndex);
            Assert.Equal(2, sut[2].WordIndex);
        }

        [Fact]
        public void Indexer_Index_Negative()
        {
            SortedBasicWordInfoSet sut = new SortedBasicWordInfoSet();
            var ex = Assert.Throws<IndexOutOfRangeException>(() => sut[-1]);
            Assert.Equal("Index should be greater than or equal to zero and less than the number of items in the set.", ex.Message);
        }

        [Fact]
        public void Indexer_Index_TooBig()
        {
            SortedBasicWordInfoSet sut = new SortedBasicWordInfoSet();
            var ex = Assert.Throws<IndexOutOfRangeException>(() => sut[1]);
            Assert.Equal("Index should be greater than or equal to zero and less than the number of items in the set.", ex.Message);
        }
    }
}
