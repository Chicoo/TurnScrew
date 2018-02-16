using System;
using Xunit;

namespace TurnScrew.Wiki.AclEngine.UnitTests
{
    public class AclEntryTests
    {
        [Fact]
        public void Constructor()
        {
            AclEntry entry = new AclEntry("Res", "Action", "U.User", Value.Grant);

            Assert.Equal("Res", entry.Resource);
            Assert.Equal("Action", entry.Action);
            Assert.Equal("U.User", entry.Subject);
            Assert.Equal(Value.Grant, entry.Value);

            entry = new AclEntry("Res", "Action", "G.Group", Value.Deny);

            Assert.Equal("Res", entry.Resource);
            Assert.Equal("Action", entry.Action);
            Assert.Equal("G.Group", entry.Subject);
            Assert.Equal(Value.Deny, entry.Value);
        }

        [Fact]
        public void Constructor_Resource_Null()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new AclEntry(null, "Action", "U.USer", Value.Grant));
            Assert.Equal("Value cannot be null.\r\nParameter name: resource", ex.Message);
        }

        [Fact]
        public void Constructor_Resource_Empty()
        {
            var ex = Assert.Throws<ArgumentException>(() => new AclEntry("", "Action", "U.User", Value.Grant));
            Assert.Equal("Resource cannot be empty.\r\nParameter name: resource", ex.Message);
        }

        [Fact]
        public void Constructor_Action_Null()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new AclEntry("Res", null, "U.User", Value.Grant));
            Assert.Equal("Value cannot be null.\r\nParameter name: action", ex.Message);
        }

        [Fact]
        public void Constructor_Action_Empty()
        {
            var ex = Assert.Throws<ArgumentException>(() => new AclEntry("Res", "", "U.User", Value.Grant));
            Assert.Equal("Action cannot be empty.\r\nParameter name: action", ex.Message);
        }

        [Fact]
        public void Constructor_Subject_Null()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new AclEntry("Res", "Action", null, Value.Grant));
            Assert.Equal("Value cannot be null.\r\nParameter name: subject", ex.Message);
        }

        [Fact]
        public void StoreEntry_Subject_Empty()
        {
            var ex = Assert.Throws<ArgumentException>(() => new AclEntry("Res", "Action", "", Value.Grant));
            Assert.Equal("Subject cannot be empty.\r\nParameter name: subject", ex.Message);
        }

        [Fact]
        public void Equal()
        {
            AclEntry entry = new AclEntry("Res", "Action", "U.User", Value.Grant);

            Assert.False(entry.Equals(null), "Equals should return false (testing null)");
            Assert.False(entry.Equals("blah"), "Equals should return false (testing a string)");
            Assert.False(entry.Equals(new AclEntry("Res1", "Action", "U.User", Value.Grant)), "Equals should return false");
            Assert.False(entry.Equals(new AclEntry("Res", "Action1", "U.User", Value.Grant)), "Equals should return false");
            Assert.False(entry.Equals(new AclEntry("Res", "Action", "U.User1", Value.Grant)), "Equals should return false");
            Assert.True(entry.Equals(new AclEntry("Res", "Action", "U.User", Value.Deny)), "Equals should return true");
            Assert.True(entry.Equals(new AclEntry("Res", "Action", "U.User", Value.Grant)), "Equals should return true");
            Assert.True(entry.Equals(entry), "Equals should return true");
        }

        [Fact]
        public void Static_Equals()
        {
            AclEntry entry = new AclEntry("Res", "Action", "U.User", Value.Grant);

            Assert.False(AclEntry.Equals(entry, null), "Equals should return false (testing null)");
            Assert.False(AclEntry.Equals(entry, "blah"), "Equals should return false (testing a string)");
            Assert.False(AclEntry.Equals(entry, new AclEntry("Res1", "Action", "U.User", Value.Grant)), "Equals should return false");
            Assert.False(AclEntry.Equals(entry, new AclEntry("Res", "Action1", "U.User", Value.Grant)), "Equals should return false");
            Assert.False(AclEntry.Equals(entry, new AclEntry("Res", "Action", "U.User1", Value.Grant)), "Equals should return false");
            Assert.True(AclEntry.Equals(entry, new AclEntry("Res", "Action", "U.User", Value.Deny)), "Equals should return true");
            Assert.True(AclEntry.Equals(entry, new AclEntry("Res", "Action", "U.User", Value.Grant)), "Equals should return true");
            Assert.True(AclEntry.Equals(entry, entry), "Equals should return true");
        }
    }
}
