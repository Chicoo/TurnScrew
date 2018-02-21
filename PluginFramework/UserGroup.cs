
using System;
using System.Collections.Generic;


namespace TurnScrew.Wiki.PluginFramework
{

    /// <summary>
    /// Represents a group of users.
    /// </summary>
    public class UserGroup
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="T:UserGroup" /> class.
        /// </summary>
        /// <param name="name">The name of the group.</param>
        /// <param name="description">The description of the group.</param>
        /// <param name="provider">The Users Storage Provider that handles the user group.</param>
        public UserGroup(string name, string description, IUsersStorageProviderV30 provider)
        {
            Name = name;
            Description = description;
            Provider = provider;
        }

        /// <summary>
        /// Gets or sets the name of the user group.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the group.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the users in the group.
        /// </summary>
        public string[] Users { get; set; } = new string[0];

        /// <summary>
        /// Gets or sets the provider that handles the user group.
        /// </summary>
        public IUsersStorageProviderV30 Provider { get; set; }

    }

    /// <summary>
    /// Implements a comparer for <see cref="T:UserGroup" /> objects, using <b>Name</b> as parameter.
    /// </summary>
    public class UserGroupComparer : IComparer<UserGroup>
    {

        /// <summary>
        /// Compares two <see cref="T:UserGroup" /> objects, using <b>Name</b> as parameter.
        /// </summary>
        /// <param name="x">The first object.</param>
        /// <param name="y">The second object.</param>
        /// <returns>The comparison result.</returns>
        public int Compare(UserGroup x, UserGroup y)
        {
            return StringComparer.OrdinalIgnoreCase.Compare(x.Name, y.Name);
        }

    }

}
