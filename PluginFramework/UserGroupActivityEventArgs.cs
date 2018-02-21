
using System;
using System.Linq;


namespace TurnScrew.Wiki.PluginFramework
{

    /// <summary>
    /// Contains arguments for the User Group Activity events.
    /// </summary>
    public class UserGroupActivityEventArgs : EventArgs
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="T:UserGroupActivityEventArgs" /> class.
        /// </summary>
        /// <param name="group">The user group the activity refers to.</param>
        /// <param name="activity">The activity performed.</param>
        public UserGroupActivityEventArgs(UserGroup group, UserGroupActivity activity)
        {
            Group = group;
            Activity = activity;
        }

        /// <summary>
        /// Gets the user group the activity refers to.
        /// </summary>
        public UserGroup Group { get; private set; }

        /// <summary>
        /// Gets the activity performed.
        /// </summary>
        public UserGroupActivity Activity { get; private set; }

    }

    /// <summary>
    /// Lists legal user group activity types.
    /// </summary>
    public enum UserGroupActivity
    {
        /// <summary>
        /// A group has been added.
        /// </summary>
        GroupAdded,
        /// <summary>
        /// A group has been removed.
        /// </summary>
        GroupRemoved,
        /// <summary>
        /// A group has been modified.
        /// </summary>
        GroupModified
    }

}
