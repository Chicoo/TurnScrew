using System;
using System.Collections.Generic;
using Xunit;
using Moq;

namespace TurnScrew.Wiki.AclEngine.UnitTests
{
    public class AclManagerBaseTests
    {

        private AclManagerBase MockAclManager()
        {
            return new Mock<AclManagerBase>().Object;
        }

        private void AssertAclEntriesAreEqual(AclEntry expected, AclEntry actual)
        {
            Assert.Equal(expected.Resource, actual.Resource);
            Assert.Equal(expected.Action, actual.Action);
            Assert.Equal(expected.Subject, actual.Subject);
            Assert.Equal(expected.Value, actual.Value);
        }

        [Fact]
        public void StoreEntry_RetrieveAllEntries()
        {
            AclManagerBase manager = MockAclManager();

            Assert.Equal(0, manager.TotalEntries);
            Assert.Empty(manager.RetrieveAllEntries());

            Assert.True(manager.StoreEntry("Res", "Action", "U.User", Value.Grant), "StoreEntry should return true");
            Assert.True(manager.StoreEntry("Res", "Action", "G.Group", Value.Deny), "StoreEntry should return true");

            Assert.Equal(2, manager.TotalEntries);

            AclEntry[] allEntries = manager.RetrieveAllEntries();
            Assert.Equal(2, allEntries.Length);

            Array.Sort(allEntries, delegate (AclEntry x, AclEntry y) { return x.Subject.CompareTo(y.Subject); });

            AssertAclEntriesAreEqual(new AclEntry("Res", "Action", "G.Group", Value.Deny), allEntries[0]);
            AssertAclEntriesAreEqual(new AclEntry("Res", "Action", "U.User", Value.Grant), allEntries[1]);
        }

        [Fact]
        public void StoreEntry_Resource_Null()
        {
            AclManagerBase manager = MockAclManager();
            var ex = Assert.Throws<ArgumentNullException>(() => manager.StoreEntry(null, "Action", "U.User", Value.Grant));
            Assert.Equal("Value cannot be null.\r\nParameter name: resource", ex.Message);
        }

        [Fact]
        public void StoreEntry_Resource_Empty()
        {
            AclManagerBase manager = MockAclManager();
            var ex = Assert.Throws<ArgumentException>(() => manager.StoreEntry("", "Action", "U.User", Value.Grant));
            Assert.Equal("Resource cannot be empty.\r\nParameter name: resource", ex.Message);
        }

        [Fact]
        public void StoreEntry_Action_Null()
        {
            AclManagerBase manager = MockAclManager();
            var ex = Assert.Throws<ArgumentNullException>(() => manager.StoreEntry("Res", null, "U.User", Value.Grant));
            Assert.Equal("Value cannot be null.\r\nParameter name: action", ex.Message);
        }

        [Fact]
        public void StoreEntry_Action_Empty()
        {
            AclManagerBase manager = MockAclManager();
            var ex = Assert.Throws<ArgumentException>(() => manager.StoreEntry("Res", "", "U.User", Value.Grant));
            Assert.Equal("Action cannot be empty.\r\nParameter name: action", ex.Message);
        }

        [Fact]
        public void StoreEntry_Subject_Null()
        {
            AclManagerBase manager = MockAclManager();
            var ex = Assert.Throws<ArgumentNullException>(() => manager.StoreEntry("Res", "Action", null, Value.Grant));
            Assert.Equal("Value cannot be null.\r\nParameter name: subject", ex.Message);
        }

        [Fact]
        public void StoreEntry_Subject_Empty()
        {
            AclManagerBase manager = MockAclManager();
            var ex = Assert.Throws<ArgumentException>(() => manager.StoreEntry("Res", "Action", "", Value.Grant));
            Assert.Equal("Subject cannot be empty.\r\nParameter name: subject", ex.Message);
        }

        [Fact]
        public void StoreEntry_Overwrite()
        {
            AclManagerBase manager = MockAclManager();

            Assert.True(manager.StoreEntry("Res", "Action", "U.User", Value.Grant), "StoreEntry should return true");
            Assert.True(manager.StoreEntry("Res", "Action", "G.Group", Value.Grant), "StoreEntry should return true");

            // Overwrite with a deny
            Assert.True(manager.StoreEntry("Res", "Action", "U.User", Value.Deny), "StoreEntry should return true");

            Assert.Equal(2, manager.TotalEntries);

            AclEntry[] allEntries = manager.RetrieveAllEntries();
            Assert.Equal(2, allEntries.Length);

            Array.Sort(allEntries, delegate (AclEntry x, AclEntry y) { return x.Subject.CompareTo(y.Subject); });

            AssertAclEntriesAreEqual(new AclEntry("Res", "Action", "G.Group", Value.Grant), allEntries[0]);
            AssertAclEntriesAreEqual(new AclEntry("Res", "Action", "U.User", Value.Deny), allEntries[1]);
        }

        [Fact]
        public void DeleteEntry_RetrieveAllEntries()
        {
            AclManagerBase manager = MockAclManager();

            Assert.True(manager.StoreEntry("Res", "Action", "U.User", Value.Grant), "StoreEntry should return true");
            Assert.True(manager.StoreEntry("Res", "Action", "G.Group", Value.Deny), "StoreEntry should return true");

            Assert.Equal(2, manager.TotalEntries);

            Assert.False(manager.DeleteEntry("Res1", "Action", "G.Group"), "DeleteEntry should return false");
            Assert.False(manager.DeleteEntry("Res", "Action1", "G.Group"), "DeleteEntry should return false");
            Assert.False(manager.DeleteEntry("Res", "Action", "G.Group1"), "DeleteEntry should return false");

            Assert.Equal(2, manager.TotalEntries);

            Assert.True(manager.DeleteEntry("Res", "Action", "G.Group"), "DeleteEntry should return true");

            Assert.Equal(1, manager.TotalEntries);

            AclEntry[] allEntries = manager.RetrieveAllEntries();
            Assert.Single(allEntries);

            AssertAclEntriesAreEqual(new AclEntry("Res", "Action", "U.User", Value.Grant), allEntries[0]);
        }

        [Fact]
        public void DeleteEntry_Resource_Null()
        {
            AclManagerBase manager = MockAclManager();
            var ex = Assert.Throws<ArgumentNullException>(() => manager.DeleteEntry(null, "Action", "U.User"));
            Assert.Equal("Value cannot be null.\r\nParameter name: resource", ex.Message);
        }

        [Fact]
        public void DeleteEntry_Resource_Empty()
        {
            AclManagerBase manager = MockAclManager();
            var ex = Assert.Throws<ArgumentException>(() => manager.DeleteEntry("", "Action", "U.User"));
            Assert.Equal("Resource cannot be empty.\r\nParameter name: resource", ex.Message);
        }

        [Fact]
        public void DeleteEntry_Action_Null()
        {
            AclManagerBase manager = MockAclManager();
            var ex = Assert.Throws<ArgumentNullException>(() => manager.DeleteEntry("Res", null, "U.User"));
            Assert.Equal("Value cannot be null.\r\nParameter name: action", ex.Message);
        }

        [Fact]
        public void DeleteEntry_Action_Empty()
        {
            AclManagerBase manager = MockAclManager();
            var ex = Assert.Throws<ArgumentException>(() => manager.DeleteEntry("Res", "", "U.User"));
            Assert.Equal("Action cannot be empty.\r\nParameter name: action", ex.Message);
        }

        [Fact]
        public void DeleteEntry_Subject_Null()
        {
            AclManagerBase manager = MockAclManager();
            var ex = Assert.Throws<ArgumentNullException>(() => manager.DeleteEntry("Res", "Action", null));
            Assert.Equal("Value cannot be null.\r\nParameter name: subject", ex.Message);
        }

        [Fact]
        public void DeleteEntry_Subject_Empty()
        {
            AclManagerBase manager = MockAclManager();
            var ex = Assert.Throws<ArgumentException>(() => manager.DeleteEntry("Res", "Action", ""));
            Assert.Equal("Subject cannot be empty.\r\nParameter name: subject", ex.Message);
        }

        [Fact]
        public void DeleteEntriesForResource_RetrieveAllEntries()
        {
            AclManagerBase manager = MockAclManager();

            Assert.True(manager.StoreEntry("Res", "Action", "U.User", Value.Grant), "StoreEntry should return true");
            Assert.True(manager.StoreEntry("Res", "Action", "G.Group", Value.Deny), "StoreEntry should return true");
            Assert.True(manager.StoreEntry("Res2", "Action2", "G.Group2", Value.Grant), "StoreEntry should return true");
            Assert.True(manager.StoreEntry("Res2", "Action3", "U.User", Value.Deny), "StoreEntry should return true");

            Assert.Equal(4, manager.TotalEntries);

            Assert.False(manager.DeleteEntriesForResource("Inexistent"), "DeleteEntriesForResource should return false");

            Assert.Equal(4, manager.TotalEntries);

            Assert.True(manager.DeleteEntriesForResource("Res2"), "DeleteEntriesForResource should return true");

            Assert.Equal(2, manager.TotalEntries);

            AclEntry[] allEntries = manager.RetrieveAllEntries();
            Assert.Equal(2, allEntries.Length);

            Array.Sort(allEntries, delegate (AclEntry x, AclEntry y) { return x.Subject.CompareTo(y.Subject); });

            AssertAclEntriesAreEqual(new AclEntry("Res", "Action", "G.Group", Value.Deny), allEntries[0]);
            AssertAclEntriesAreEqual(new AclEntry("Res", "Action", "U.User", Value.Grant), allEntries[1]);
        }

        [Fact]
        public void DeleteEntriesForResource_Resource_Null()
        {
            AclManagerBase manager = MockAclManager();
            var ex = Assert.Throws<ArgumentNullException>(() => manager.DeleteEntriesForResource(null));
            Assert.Equal("Value cannot be null.\r\nParameter name: resource", ex.Message);
        }

        [Fact]
        public void DeleteEntriesForResource_Resource_Empty()
        {
            AclManagerBase manager = MockAclManager();
            var ex = Assert.Throws<ArgumentException>(() => manager.DeleteEntriesForResource(""));
            Assert.Equal("Resource cannot be empty.\r\nParameter name: resource", ex.Message);
        }

        [Fact]
        public void DeleteEntriesForSubject_RetrieveAllEntries()
        {
            AclManagerBase manager = MockAclManager();

            Assert.True(manager.StoreEntry("Res", "Action", "U.User", Value.Grant), "StoreEntry should return true");
            Assert.True(manager.StoreEntry("Res", "Action", "G.Group", Value.Deny), "StoreEntry should return true");
            Assert.True(manager.StoreEntry("Res2", "Action2", "G.Group2", Value.Grant), "StoreEntry should return true");
            Assert.True(manager.StoreEntry("Res2", "Action3", "G.Group2", Value.Deny), "StoreEntry should return true");

            Assert.Equal(4, manager.TotalEntries);

            Assert.False(manager.DeleteEntriesForSubject("I.Inexistent"), "DeleteEntriesForSubject should return false");

            Assert.Equal(4, manager.TotalEntries);

            Assert.True(manager.DeleteEntriesForSubject("G.Group2"), "DeleteEntriesForSubject should return true");

            Assert.Equal(2, manager.TotalEntries);

            AclEntry[] allEntries = manager.RetrieveAllEntries();
            Assert.Equal(2, allEntries.Length);

            Array.Sort(allEntries, delegate (AclEntry x, AclEntry y) { return x.Subject.CompareTo(y.Subject); });

            AssertAclEntriesAreEqual(new AclEntry("Res", "Action", "G.Group", Value.Deny), allEntries[0]);
            AssertAclEntriesAreEqual(new AclEntry("Res", "Action", "U.User", Value.Grant), allEntries[1]);
        }

        [Fact]
        public void DeleteEntriesForSubject_Subject_Null()
        {
            AclManagerBase manager = MockAclManager();
            var ex = Assert.Throws<ArgumentNullException>(() => manager.DeleteEntriesForSubject(null));
            Assert.Equal("Value cannot be null.\r\nParameter name: subject", ex.Message);
        }

        [Fact]
        public void DeleteEntriesForSubject_Subject_Empty()
        {
            AclManagerBase manager = MockAclManager();
            var ex = Assert.Throws<ArgumentException>(() => manager.DeleteEntriesForSubject(""));
            Assert.Equal("Subject cannot be empty.\r\nParameter name: subject", ex.Message);
        }

        [Fact]
        public void RetrieveEntriesForResource()
        {
            AclManagerBase manager = MockAclManager();

            Assert.True(manager.StoreEntry("Res", "Action", "U.User", Value.Grant), "StoreEntry should return true");
            Assert.True(manager.StoreEntry("Res", "Action", "G.Group", Value.Deny), "StoreEntry should return true");
            Assert.True(manager.StoreEntry("Res2", "Action2", "G.Group2", Value.Grant), "StoreEntry should return true");
            Assert.True(manager.StoreEntry("Res2", "Action3", "G.Group2", Value.Deny), "StoreEntry should return true");

            Assert.Empty(manager.RetrieveEntriesForResource("Inexistent"));

            AclEntry[] allEntries = manager.RetrieveEntriesForResource("Res");
            Assert.Equal(2, allEntries.Length);

            Array.Sort(allEntries, delegate (AclEntry x, AclEntry y) { return x.Subject.CompareTo(y.Subject); });

            AssertAclEntriesAreEqual(new AclEntry("Res", "Action", "G.Group", Value.Deny), allEntries[0]);
            AssertAclEntriesAreEqual(new AclEntry("Res", "Action", "U.User", Value.Grant), allEntries[1]);
        }

        [Fact]
        public void RetrieveEntriesForResource_Resource_Null()
        {
            AclManagerBase manager = MockAclManager();
            var ex = Assert.Throws<ArgumentNullException>(() => manager.RetrieveEntriesForResource(null));
            Assert.Equal("Value cannot be null.\r\nParameter name: resource", ex.Message);
        }

        [Fact]
        public void RetrieveEntriesForResource_Resource_Empty()
        {
            AclManagerBase manager = MockAclManager();
            var ex = Assert.Throws<ArgumentException>(() => manager.RetrieveEntriesForResource(""));
            Assert.Equal("Resource cannot be empty.\r\nParameter name: resource", ex.Message);
        }
        [Fact]
        public void RetrieveEntriesForSubject()
        {
            AclManagerBase manager = MockAclManager();

            Assert.True(manager.StoreEntry("Res", "Action", "U.User", Value.Grant), "StoreEntry should return true");
            Assert.True(manager.StoreEntry("Res", "Action", "G.Group", Value.Deny), "StoreEntry should return true");
            Assert.True(manager.StoreEntry("Res2", "Action2", "G.Group2", Value.Grant), "StoreEntry should return true");
            Assert.True(manager.StoreEntry("Res2", "Action3", "G.Group2", Value.Deny), "StoreEntry should return true");

            Assert.Empty(manager.RetrieveEntriesForSubject("I.Inexistent"));

            AclEntry[] allEntries = manager.RetrieveEntriesForSubject("G.Group2");
            Assert.Equal(2, allEntries.Length);

            Array.Sort(allEntries, delegate (AclEntry x, AclEntry y) { return x.Action.CompareTo(y.Action); });

            AssertAclEntriesAreEqual(new AclEntry("Res2", "Action2", "G.Group2", Value.Grant), allEntries[0]);
            AssertAclEntriesAreEqual(new AclEntry("Res2", "Action3", "G.Group2", Value.Deny), allEntries[1]);
        }

        [Fact]
        public void RetrieveEntriesForSubject_Subject_Null()
        {
            AclManagerBase manager = MockAclManager();
            var ex = Assert.Throws<ArgumentNullException>(() => manager.RetrieveEntriesForSubject(null));
            Assert.Equal("Value cannot be null.\r\nParameter name: subject", ex.Message);
        }

        [Fact]
        public void RetrieveEntriesForSubject_Subject_Empty()
        {
            AclManagerBase manager = MockAclManager();
            var ex = Assert.Throws<ArgumentException>(() => manager.RetrieveEntriesForSubject(""));
            Assert.Equal("Subject cannot be empty.\r\nParameter name: subject", ex.Message);
        }
        [Fact]
        public void InitializeData()
        {
            AclManagerBase manager = MockAclManager();

            List<AclEntry> entries = new List<AclEntry>();
            entries.Add(new AclEntry("Res", "Action", "U.User", Value.Grant));
            entries.Add(new AclEntry("Res", "Action", "G.Group", Value.Deny));

            manager.InitializeData(entries.ToArray());

            Assert.Equal(2, manager.TotalEntries);

            AclEntry[] allEntries = manager.RetrieveAllEntries();
            Assert.Equal(2, allEntries.Length);

            Array.Sort(allEntries, delegate (AclEntry x, AclEntry y) { return x.Subject.CompareTo(y.Subject); });

            AssertAclEntriesAreEqual(new AclEntry("Res", "Action", "G.Group", Value.Deny), allEntries[0]);
            AssertAclEntriesAreEqual(new AclEntry("Res", "Action", "U.User", Value.Grant), allEntries[1]);
        }

        [Fact]
        public void InitializeData_NullData()
        {
            AclManagerBase manager = MockAclManager();
            var ex = Assert.Throws<ArgumentNullException>(() => manager.InitializeData(null));
            Assert.Equal("Value cannot be null.\r\nParameter name: entries", ex.Message);
        }

        [Fact]
        public void Event_AclChanged_StoreEntry()
        {
            AclManagerBase manager = MockAclManager();

            AclEntry entry = new AclEntry("Res", "Action", "U.User", Value.Grant);

            bool invoked = false;
            manager.AclChanged += delegate (object sender, AclChangedEventArgs e)
            {
                invoked = true;
                Assert.Single(e.Entries);
                AssertAclEntriesAreEqual(entry, e.Entries[0]);
                Assert.Equal(Change.EntryStored, e.Change);
            };

            manager.StoreEntry(entry.Resource, entry.Action, entry.Subject, entry.Value);

            Assert.True(invoked, "Store event not invoked");
        }

        [Fact]
        public void Event_AclChanged_OverwriteEntry()
        {
            AclManagerBase manager = MockAclManager();

            AclEntry entryOld = new AclEntry("Res", "Action", "U.User", Value.Deny);

            AclEntry entryNew = new AclEntry("Res", "Action", "U.User", Value.Grant);

            manager.StoreEntry(entryOld.Resource, entryOld.Action, entryOld.Subject, entryOld.Value);

            bool invokedStore = false;
            bool invokedDelete = false;
            manager.AclChanged += delegate (object sender, AclChangedEventArgs e)
            {
                if (e.Change == Change.EntryStored)
                {
                    invokedStore = true;
                    Assert.Single(e.Entries);
                    AssertAclEntriesAreEqual(entryNew, e.Entries[0]);
                    Assert.Equal(Change.EntryStored, e.Change);
                }
                else
                {
                    invokedDelete = true;
                    Assert.Single(e.Entries);
                    AssertAclEntriesAreEqual(entryOld, e.Entries[0]);
                    Assert.Equal(Change.EntryDeleted, e.Change);
                }
            };

            manager.StoreEntry(entryNew.Resource, entryNew.Action, entryNew.Subject, entryNew.Value);

            Assert.True(invokedStore, "Store event not invoked");
            Assert.True(invokedDelete, "Delete event not invoked");
        }

        [Fact]
        public void Event_AclChanged_DeleteEntry()
        {
            AclManagerBase manager = MockAclManager();

            AclEntry entry = new AclEntry("Res", "Action", "U.User", Value.Grant);

            manager.StoreEntry(entry.Resource, entry.Action, entry.Subject, entry.Value);

            bool invokedDelete = false;
            manager.AclChanged += delegate (object sender, AclChangedEventArgs e)
            {
                invokedDelete = true;
                Assert.Single(e.Entries);
                AssertAclEntriesAreEqual(entry, e.Entries[0]);
                Assert.Equal(Change.EntryDeleted, e.Change);
            };

            manager.DeleteEntry(entry.Resource, entry.Action, entry.Subject);

            Assert.True(invokedDelete, "Delete event not invoked");
        }

        [Fact]
        public void Event_AclChanged_DeleteEntriesForResource()
        {
            AclManagerBase manager = MockAclManager();

            AclEntry entry = new AclEntry("Res", "Action", "U.User", Value.Grant);

            manager.StoreEntry(entry.Resource, entry.Action, entry.Subject, entry.Value);
            manager.StoreEntry("Res2", "Action", "G.Group", Value.Deny);

            bool invokedDelete = false;
            manager.AclChanged += delegate (object sender, AclChangedEventArgs e)
            {
                invokedDelete = true;
                Assert.Single(e.Entries);
                AssertAclEntriesAreEqual(entry, e.Entries[0]);
                Assert.Equal(Change.EntryDeleted, e.Change);
            };

            manager.DeleteEntriesForResource(entry.Resource);

            Assert.True(invokedDelete, "Delete event not invoked");
        }

        [Fact]
        public void Event_AclChanged_DeleteEntriesForSubject()
        {
            AclManagerBase manager = MockAclManager();

            AclEntry entry = new AclEntry("Res", "Action", "U.User", Value.Grant);

            manager.StoreEntry(entry.Resource, entry.Action, entry.Subject, entry.Value);
            manager.StoreEntry("Res2", "Action", "G.Group", Value.Deny);

            bool invokedDelete = false;
            manager.AclChanged += delegate (object sender, AclChangedEventArgs e)
            {
                invokedDelete = true;
                Assert.Single(e.Entries);
                AssertAclEntriesAreEqual(entry, e.Entries[0]);
                Assert.Equal(Change.EntryDeleted, e.Change);
            };

            manager.DeleteEntriesForSubject(entry.Subject);

            Assert.True(invokedDelete, "Delete event not invoked");
        }

        [Fact]
        public void RenameResource()
        {
            AclManagerBase manager = MockAclManager();

            Assert.False(manager.RenameResource("Res", "Res_Renamed"), "RenameResource should return false");

            AclEntry entry1 = new AclEntry("Res", "Action", "U.User", Value.Grant);
            AclEntry newEntry1 = new AclEntry("Res_Renamed", "Action", "U.User", Value.Grant);
            AclEntry entry2 = new AclEntry("Res", "Action2", "U.User2", Value.Deny);
            AclEntry newEntry2 = new AclEntry("Res_Renamed", "Action2", "U.User", Value.Deny);

            manager.StoreEntry(entry1.Resource, entry1.Action, entry1.Subject, entry1.Value);
            manager.StoreEntry(entry2.Resource, entry2.Action, entry2.Subject, entry2.Value);
            manager.StoreEntry("Res2", "Action", "G.Group", Value.Deny);

            bool invokedDelete1 = false;
            bool invokedStore1 = false;
            bool invokedDelete2 = false;
            bool invokedStore2 = false;
            manager.AclChanged += delegate (object sender, AclChangedEventArgs e)
            {
                if (e.Change == Change.EntryDeleted)
                {
                    Assert.Single(e.Entries);
                    Assert.Equal("Res", e.Entries[0].Resource);

                    if (e.Entries[0].Action == entry1.Action) invokedDelete1 = true;
                    if (e.Entries[0].Action == entry2.Action) invokedDelete2 = true;
                }
                else
                {
                    Assert.Single(e.Entries);
                    Assert.Equal("Res_Renamed", e.Entries[0].Resource);

                    if (e.Entries[0].Action == entry1.Action) invokedStore1 = true;
                    if (e.Entries[0].Action == entry2.Action) invokedStore2 = true;
                }
            };

            Assert.True(manager.RenameResource("Res", "Res_Renamed"), "RenameResource should return true");

            Assert.True(invokedDelete1, "Delete event 1 not invoked");
            Assert.True(invokedStore1, "Store event 1 not invoked");
            Assert.True(invokedDelete2, "Delete event 2 not invoked");
            Assert.True(invokedStore2, "Store event 2 not invoked");

            AclEntry[] entries = manager.RetrieveAllEntries();

            Assert.Equal(3, entries.Length);
            Array.Sort(entries, delegate (AclEntry x, AclEntry y) { return x.Resource.CompareTo(y.Resource); });

            Assert.Equal("Res_Renamed", entries[0].Resource);
            if (entries[0].Value == Value.Grant)
            {
                Assert.Equal("Action", entries[0].Action);
                Assert.Equal("U.User", entries[0].Subject);
            }
            else
            {
                Assert.Equal("Action2", entries[0].Action);
                Assert.Equal("U.User2", entries[0].Subject);
            }

            Assert.Equal("Res_Renamed", entries[1].Resource);
            if (entries[1].Value == Value.Grant)
            {
                Assert.Equal("Action", entries[1].Action);
                Assert.Equal("U.User", entries[1].Subject);
            }
            else
            {
                Assert.Equal("Action2", entries[1].Action);
                Assert.Equal("U.User2", entries[1].Subject);
            }

            Assert.Equal("Res2", entries[2].Resource);
            Assert.Equal("Action", entries[2].Action);
            Assert.Equal("G.Group", entries[2].Subject);
            Assert.Equal(Value.Deny, entries[2].Value);
        }

        [Fact]
        public void RenameResource_Resource_Null()
        {
            AclManagerBase manager = MockAclManager();
            var ex = Assert.Throws<ArgumentNullException>(() => manager.RenameResource(null, "new name"));
            Assert.Equal("Value cannot be null.\r\nParameter name: resource", ex.Message);
        }

        [Fact]
        public void RenameResource_Resource_Empty()
        {
            AclManagerBase manager = MockAclManager();
            var ex = Assert.Throws<ArgumentException>(() => manager.RenameResource("", "new name"));
            Assert.Equal("Resource cannot be empty.\r\nParameter name: resource", ex.Message);
        }

        [Fact]
        public void RenameResource_NewName_Null()
        {
            AclManagerBase manager = MockAclManager();
            var ex = Assert.Throws<ArgumentNullException>(() => manager.RenameResource("Res", null));
            Assert.Equal("Value cannot be null.\r\nParameter name: newName", ex.Message);
        }

        [Fact]
        public void RenameResource_NewName_Empty()
        {
            AclManagerBase manager = MockAclManager();
            var ex = Assert.Throws<ArgumentException>(() => manager.RenameResource("Res", ""));
            Assert.Equal("New Name cannot be empty.\r\nParameter name: newName", ex.Message);
        }
    }

}
