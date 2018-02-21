using System;
using System.Collections.Generic;
using Xunit;

namespace TurnScrew.Wiki.SearchEngine.UnitTests
{
    public class OccurrenceDictionaryTests : TestsBase
    {
        [Fact]
        public void Constructor_NoCapacity()
        {
            OccurrenceDictionary sut = new OccurrenceDictionary();
            Assert.Empty(sut);
        }

        [Fact]
        public void Constructor_WithCapacity()
        {
            OccurrenceDictionary sut = new OccurrenceDictionary(10);
            Assert.Empty(sut);
        }

        [Fact]
        public void Constructor_InvalidCapacity()
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => new OccurrenceDictionary(-1));
            Assert.Equal("Capacity must be greater than zero.\r\nParameter name: capacity", ex.Message);
        }

        [Fact]
        public void Add_KV()
        {
            OccurrenceDictionary sut = new OccurrenceDictionary();
            sut.Add(MockDocument("Doc1", "Doc 1", "d", DateTime.Now), new SortedBasicWordInfoSet());
            Assert.Single(sut);
            sut.Add(MockDocument("Doc2", "Doc 2", "d", DateTime.Now), new SortedBasicWordInfoSet());
            Assert.Equal(2, sut.Count);
        }

        [Fact]
        public void Add_KV_ExistingKey()
        {
            OccurrenceDictionary sut = new OccurrenceDictionary();
            sut.Add(MockDocument("Doc1", "Doc 1", "d", DateTime.Now), new SortedBasicWordInfoSet());
            var ex = Assert.Throws<ArgumentException>(() => sut.Add(MockDocument("Doc1", "Doc 2", "d2", DateTime.Now.AddHours(1)), new SortedBasicWordInfoSet()));
            Assert.Equal("The specified key is already contained in the dictionary.\r\nParameter name: key", ex.Message);
        }

        [Fact]
        public void Add_KV_Key_Null()
        {
            OccurrenceDictionary sut = new OccurrenceDictionary();
            var ex = Assert.Throws<ArgumentNullException>(() => sut.Add(null, new SortedBasicWordInfoSet()));
            Assert.Equal("Value cannot be null.\r\nParameter name: key", ex.Message);
        }

        [Fact]
        public void Add_KV_Value_Null()
        {
            OccurrenceDictionary sut = new OccurrenceDictionary();
            var ex = Assert.Throws<ArgumentNullException>(() => sut.Add(MockDocument("Doc1", "Doc 1", "d", DateTime.Now), null));
            Assert.Equal("Value cannot be null.\r\nParameter name: value", ex.Message);
        }

        [Fact]
        public void Add_Pair()
        {
            OccurrenceDictionary dic = new OccurrenceDictionary();
            dic.Add(new KeyValuePair<IDocument, SortedBasicWordInfoSet>(
                MockDocument("Doc1", "Doc 1", "d", DateTime.Now), new SortedBasicWordInfoSet()));
            Assert.Single(dic);
            dic.Add(MockDocument("Doc2", "Doc 2", "d", DateTime.Now), new SortedBasicWordInfoSet());
            Assert.Equal(2, dic.Count);
        }

        [Fact]
        public void ContainsKey()
        {
            OccurrenceDictionary dic = new OccurrenceDictionary();
            IDocument doc = MockDocument("Doc", "Doc", "d", DateTime.Now);
            Assert.False(dic.ContainsKey(doc), "ContainsKey should return false");
            dic.Add(doc, new SortedBasicWordInfoSet());
            Assert.True(dic.ContainsKey(doc), "ContainsKey should return true");
            Assert.False(dic.ContainsKey(MockDocument("Doc2", "Doc 2", "d", DateTime.Now)), "ContainsKey should return false");

            IDocument doc2 = MockDocument("Doc", "Doc", "d", DateTime.Now);
            Assert.True(dic.ContainsKey(doc2), "ContainsKey should return true");
        }

        [Fact]
        public void ContainsKey_Key_Null()
        {
            OccurrenceDictionary sut = new OccurrenceDictionary();
            var ex = Assert.Throws<ArgumentNullException>(() => sut.ContainsKey(null));
            Assert.Equal("Value cannot be null.\r\nParameter name: key", ex.Message);
        }
        [Fact]
        public void Keys()
        {
            OccurrenceDictionary dic = new OccurrenceDictionary();
            IDocument doc1 = MockDocument("Doc1", "Doc1", "d", DateTime.Now);
            IDocument doc2 = MockDocument("Doc2", "Doc2", "d", DateTime.Now);
            dic.Add(doc1, new SortedBasicWordInfoSet());
            dic.Add(doc2, new SortedBasicWordInfoSet());

            Assert.Equal(2, dic.Keys.Count);

            bool doc1Found = false, doc2Found = false;
            foreach (IDocument d in dic.Keys)
            {
                if (d.Name == "Doc1") doc1Found = true;
                if (d.Name == "Doc2") doc2Found = true;
            }

            Assert.True(doc1Found, "Doc1 not found");
            Assert.True(doc2Found, "Doc2 not found");
        }

        [Fact]
        public void Remove_KV()
        {
            OccurrenceDictionary dic = new OccurrenceDictionary();
            SortedBasicWordInfoSet set = new SortedBasicWordInfoSet();
            set.Add(new BasicWordInfo(5, 0, WordLocation.Content));
            dic.Add(MockDocument("Doc1", "Doc1", "d", DateTime.Now), set);
            dic.Add(MockDocument("Doc2", "Doc2", "d", DateTime.Now), new SortedBasicWordInfoSet());
            Assert.Equal(2, dic.Count);
            Assert.False(dic.Remove(MockDocument("Doc3", "Doc3", "d", DateTime.Now)), "Remove should return false");
            Assert.True(dic.Remove(MockDocument("Doc1", "Doc1", "d", DateTime.Now)), "Remove should return true");
            Assert.Single(dic);
        }

        [Fact]
        public void Remove_KV_Key_Null()
        {
            OccurrenceDictionary sut = new OccurrenceDictionary();
            var ex = Assert.Throws<ArgumentNullException>(() => sut.Remove(null as IDocument));
            Assert.Equal("Value cannot be null.\r\nParameter name: key", ex.Message);
        }

        [Fact]
        public void Remove_Pair()
        {
            OccurrenceDictionary dic = new OccurrenceDictionary();
            dic.Add(MockDocument("Doc1", "Doc1", "d", DateTime.Now), new SortedBasicWordInfoSet());
            dic.Add(MockDocument("Doc2", "Doc2", "d", DateTime.Now), new SortedBasicWordInfoSet());
            Assert.Equal(2, dic.Count);
            Assert.False(dic.Remove(new KeyValuePair<IDocument, SortedBasicWordInfoSet>(MockDocument("Doc3", "Doc3", "d", DateTime.Now), new SortedBasicWordInfoSet())), "Remove should return false");
            Assert.True(dic.Remove(new KeyValuePair<IDocument, SortedBasicWordInfoSet>(MockDocument("Doc2", "Doc2", "d", DateTime.Now), new SortedBasicWordInfoSet())), "Remove should return true");
            Assert.Single(dic);
        }

        [Fact]
        public void RemoveExtended()
        {
            OccurrenceDictionary dic = new OccurrenceDictionary();
            SortedBasicWordInfoSet set1 = new SortedBasicWordInfoSet();
            set1.Add(new BasicWordInfo(5, 0, WordLocation.Content));
            set1.Add(new BasicWordInfo(12, 1, WordLocation.Keywords));
            SortedBasicWordInfoSet set2 = new SortedBasicWordInfoSet();
            set2.Add(new BasicWordInfo(1, 0, WordLocation.Content));
            set2.Add(new BasicWordInfo(4, 1, WordLocation.Title));
            dic.Add(MockDocument("Doc1", "Doc", "doc", DateTime.Now), set1);
            dic.Add(MockDocument("Doc2", "Doc", "doc", DateTime.Now), set2);

            List<DumpedWordMapping> dm = dic.RemoveExtended(MockDocument("Doc1", "Doc", "doc", DateTime.Now), 1);
            Assert.Equal(2, dm.Count);

            Assert.True(dm.Find(delegate (DumpedWordMapping m)
            {
                return m.WordID == 1 && m.DocumentID == 1 &&
                    m.FirstCharIndex == 5 && m.WordIndex == 0 &&
                    m.Location == WordLocation.Content.Location;
            }) != null, "Mapping not found");

            Assert.True(dm.Find(delegate (DumpedWordMapping m)
            {
                return m.WordID == 1 && m.DocumentID == 1 &&
                    m.FirstCharIndex == 12 && m.WordIndex == 1 &&
                    m.Location == WordLocation.Keywords.Location;
            }) != null, "Mapping not found");
        }

        [Fact]
        public void RemoveExtended_Document_Null()
        {
            OccurrenceDictionary sut = new OccurrenceDictionary();
            var ex = Assert.Throws<ArgumentNullException>(() => sut.RemoveExtended(null, 1));
            Assert.Equal("Value cannot be null.\r\nParameter name: key", ex.Message);
        }

        [Fact]
        public void TryGetValue()
        {
            OccurrenceDictionary dic = new OccurrenceDictionary();
            IDocument doc1 = MockDocument("Doc1", "Doc1", "d", DateTime.Now);
            IDocument doc2 = MockDocument("Doc2", "Doc2", "d", DateTime.Now);

            SortedBasicWordInfoSet set = null;

            Assert.False(dic.TryGetValue(doc1, out set), "TryGetValue should return false");
            Assert.Null(set);

            dic.Add(doc1, new SortedBasicWordInfoSet());
            Assert.True(dic.TryGetValue(MockDocument("Doc1", "Doc1", "d", DateTime.Now), out set), "TryGetValue should return true");
            Assert.NotNull(set);

            Assert.False(dic.TryGetValue(doc2, out set), "TryGetValue should return false");
            Assert.Null(set);
        }

        [Fact]
        public void TryGetValue_Key_Null()
        {
            OccurrenceDictionary sut = new OccurrenceDictionary();
            SortedBasicWordInfoSet set = null;
            var ex = Assert.Throws<ArgumentNullException>("key", () => sut.TryGetValue(null, out set));
        }

        [Fact]
        public void Values()
        {
            OccurrenceDictionary dic = new OccurrenceDictionary();
            IDocument doc1 = MockDocument("Doc1", "Doc1", "d", DateTime.Now);
            IDocument doc2 = MockDocument("Doc2", "Doc2", "d", DateTime.Now);
            SortedBasicWordInfoSet set1 = new SortedBasicWordInfoSet();
            set1.Add(new BasicWordInfo(0, 0, WordLocation.Content));
            SortedBasicWordInfoSet set2 = new SortedBasicWordInfoSet();
            set2.Add(new BasicWordInfo(1, 1, WordLocation.Title));
            dic.Add(doc1, set1);
            dic.Add(doc2, set2);

            Assert.Equal(2, dic.Values.Count);

            bool set1Found = false, set2Found = false;
            foreach (SortedBasicWordInfoSet set in dic.Values)
            {
                if (set[0].FirstCharIndex == 0) set1Found = true;
                if (set[0].FirstCharIndex == 1) set2Found = true;
            }

            Assert.True(set1Found, "Set1 not found");
            Assert.True(set2Found, "Set2 not found");
        }

        [Fact]
        public void Indexer_Get()
        {
            OccurrenceDictionary dic = new OccurrenceDictionary();
            IDocument doc1 = MockDocument("Doc1", "Doc1", "d", DateTime.Now);
            SortedBasicWordInfoSet set1 = new SortedBasicWordInfoSet();
            set1.Add(new BasicWordInfo(1, 1, WordLocation.Content));

            dic.Add(doc1, set1);

            SortedBasicWordInfoSet output = dic[MockDocument("Doc1", "Doc1", "d", DateTime.Now)];
            Assert.NotNull(output);
            Assert.Equal(1, set1.Count);
            Assert.Equal(1, set1[0].FirstCharIndex);
        }

        [Fact]
        public void Indexer_Get_Key_Null()
        {
            OccurrenceDictionary sut = new OccurrenceDictionary();
            var ex = Assert.Throws<ArgumentNullException>("key", () => sut[null]);
        }

        [Fact]
        public void Indexer_Get_Index_DoesNotExist()
        {
            OccurrenceDictionary sut = new OccurrenceDictionary();
            var ex = Assert.Throws<IndexOutOfRangeException>(() => sut[MockDocument("Doc", "Doc", "d", DateTime.Now)]);
            Assert.Equal("The specified key was not found.", ex.Message);
        }

        [Fact]
        public void Indexer_Set()
        {
            OccurrenceDictionary dic = new OccurrenceDictionary();
            dic.Add(MockDocument("Doc1", "Doc1", "d", DateTime.Now), new SortedBasicWordInfoSet());

            SortedBasicWordInfoSet set1 = new SortedBasicWordInfoSet();
            set1.Add(new BasicWordInfo(1, 1, WordLocation.Content));

            dic[MockDocument("Doc1", "Doc1", "d", DateTime.Now)] = set1;

            SortedBasicWordInfoSet output = dic[MockDocument("Doc1", "Doc1", "d", DateTime.Now)];
            Assert.Equal(1, output.Count);
            Assert.Equal(1, output[0].FirstCharIndex);
        }

        [Fact]
        public void Indexer_Set_Key_Null()
        {
            OccurrenceDictionary sut = new OccurrenceDictionary();
            var ex = Assert.Throws<ArgumentNullException>("key", () => sut[null] = new SortedBasicWordInfoSet());
        }

        [Fact]
        public void Indexer_set_Index_DoesNotExist()
        {
            OccurrenceDictionary sut = new OccurrenceDictionary();
            var ex = Assert.Throws<IndexOutOfRangeException>(() => sut[MockDocument("Doc", "Doc", "d", DateTime.Now)] = new SortedBasicWordInfoSet());
            Assert.Equal("The specified key was not found.", ex.Message);
        }

        [Fact]
        public void Indexer_Set_Value_Null()
        {
            OccurrenceDictionary sut = new OccurrenceDictionary();
            sut.Add(MockDocument("Doc1", "Doc1", "d", DateTime.Now), new SortedBasicWordInfoSet());
            var ex = Assert.Throws<ArgumentNullException>("value", () => sut[MockDocument("Doc1", "Doc1", "d", DateTime.Now)] = null);
        }

        [Fact]
        public void Clear()
        {
            OccurrenceDictionary dic = new OccurrenceDictionary();
            dic.Add(MockDocument("Doc1", "Doc1", "d", DateTime.Now), new SortedBasicWordInfoSet());
            dic.Clear();
            Assert.Empty(dic);
        }

        [Fact]
        public void Contains()
        {
            OccurrenceDictionary dic = new OccurrenceDictionary();
            IDocument doc = MockDocument("Doc", "Doc", "d", DateTime.Now);
            Assert.False(dic.Contains(new KeyValuePair<IDocument, SortedBasicWordInfoSet>(doc, new SortedBasicWordInfoSet())), "Contains should return false");
            dic.Add(doc, new SortedBasicWordInfoSet());
            Assert.True(dic.Contains(new KeyValuePair<IDocument, SortedBasicWordInfoSet>(doc, new SortedBasicWordInfoSet())), "Contains should return true");
            Assert.False(dic.Contains(new KeyValuePair<IDocument, SortedBasicWordInfoSet>(MockDocument("Doc2", "Doc 2", "d", DateTime.Now), new SortedBasicWordInfoSet())), "Contains should return false");

            IDocument doc2 = MockDocument("Doc", "Doc", "d", DateTime.Now);
            Assert.True(dic.Contains(new KeyValuePair<IDocument, SortedBasicWordInfoSet>(doc, new SortedBasicWordInfoSet())), "Contains should return true");
        }

        [Fact]
        public void IsReadOnly()
        {
            OccurrenceDictionary dic = new OccurrenceDictionary();
            Assert.False(dic.IsReadOnly, "IsReadOnly should always return false");
        }

        [Fact]
        public void CopyTo()
        {
            OccurrenceDictionary dic = new OccurrenceDictionary();
            SortedBasicWordInfoSet set = new SortedBasicWordInfoSet();
            set.Add(new BasicWordInfo(1, 1, WordLocation.Title));
            dic.Add(MockDocument("Doc", "Doc", "d", DateTime.Now), set);
            KeyValuePair<IDocument, SortedBasicWordInfoSet>[] array = new KeyValuePair<IDocument, SortedBasicWordInfoSet>[1];
            dic.CopyTo(array, 0);
            Assert.Equal("Doc", array[0].Key.Name);
            Assert.Equal(1, array[0].Value.Count);
            Assert.Equal(1, array[0].Value[0].FirstCharIndex);
        }

        [Fact]
        public void CopyTo_Array_Null()
        {
            OccurrenceDictionary sut = new OccurrenceDictionary();
            var ex = Assert.Throws<ArgumentNullException>(() => sut.CopyTo(null, 0));
            Assert.Equal("Value cannot be null.\r\nParameter name: array", ex.Message);
        }

        [Fact]
        public void CopyTo_Array_Short()
        {
            OccurrenceDictionary sut = new OccurrenceDictionary();
            sut.Add(MockDocument("Doc", "Doc", "d", DateTime.Now), new SortedBasicWordInfoSet());
            KeyValuePair<IDocument, SortedBasicWordInfoSet>[] array = new KeyValuePair<IDocument, SortedBasicWordInfoSet>[0];
            var ex = Assert.Throws<IndexOutOfRangeException>(() => sut.CopyTo(array, 0));
            Assert.Equal("Index was outside the bounds of the array.", ex.Message);
        }

        [Fact]
        public void CopyTo_Index_Nagative()
        {
            OccurrenceDictionary sut = new OccurrenceDictionary();
            sut.Add(MockDocument("Doc", "Doc", "d", DateTime.Now), new SortedBasicWordInfoSet());
            KeyValuePair<IDocument, SortedBasicWordInfoSet>[] array = new KeyValuePair<IDocument, SortedBasicWordInfoSet>[1];
            var ex = Assert.Throws<IndexOutOfRangeException>(() => sut.CopyTo(array, -1));
            Assert.Equal("Index was outside the bounds of the array.", ex.Message);
        }

        [Fact]
        public void CopyTo_Index_TooBig()
        {
            OccurrenceDictionary sut = new OccurrenceDictionary();
            sut.Add(MockDocument("Doc", "Doc", "d", DateTime.Now), new SortedBasicWordInfoSet());
            KeyValuePair<IDocument, SortedBasicWordInfoSet>[] array = new KeyValuePair<IDocument, SortedBasicWordInfoSet>[1];
            var ex = Assert.Throws<IndexOutOfRangeException>(() => sut.CopyTo(array, 1));
            Assert.Equal("Index was outside the bounds of the array.", ex.Message);
        }

        [Fact]
        public void GetEnumerator()
        {
            OccurrenceDictionary dic = new OccurrenceDictionary();
            IDocument doc1 = MockDocument("Doc1", "Doc1", "d", DateTime.Now);
            IDocument doc2 = MockDocument("Doc2", "Doc2", "d", DateTime.Now);
            dic.Add(doc1, new SortedBasicWordInfoSet());
            dic.Add(doc2, new SortedBasicWordInfoSet());

            Assert.NotNull(dic.GetEnumerator());

            int count = 0;
            foreach (KeyValuePair<IDocument, SortedBasicWordInfoSet> pair in dic)
            {
                count++;
            }

            Assert.Equal(2, count);
        }

    }
}