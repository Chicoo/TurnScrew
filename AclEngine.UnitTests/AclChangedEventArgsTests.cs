using System;
using Xunit;

namespace TurnScrew.Wiki.AclEngine.UnitTests {

	
	public class AclChangedEventArgsTests {

		[Fact]
		public void Constructor()
        {
			AclEntry entry = new AclEntry("Res", "Action", "U.User", Value.Grant);

			AclChangedEventArgs args = new AclChangedEventArgs(new AclEntry[] { entry }, Change.EntryStored);

			Assert.Single(args.Entries);
			Assert.Same(entry, args.Entries[0]);
			Assert.Equal(Change.EntryStored, args.Change);

			args = new AclChangedEventArgs(new AclEntry[] { entry }, Change.EntryDeleted);

			Assert.Single(args.Entries);
			Assert.Same(entry, args.Entries[0]);
			Assert.Equal(Change.EntryDeleted, args.Change);
		}

		[Fact]
		public void Constructor_NullEntries()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new AclChangedEventArgs(null, Change.EntryDeleted));

            Assert.Equal("Value cannot be null.\r\nParameter name: entries", ex.Message);
        }

		[Fact]
		public void Constructor_EmptyEntries()
        {
            var ex = Assert.Throws<ArgumentException>(() => new AclChangedEventArgs(new AclEntry[0], Change.EntryDeleted));

            Assert.Equal("Entries cannot be empty.\r\nParameter name: entries", ex.Message);
		}

	}

}
