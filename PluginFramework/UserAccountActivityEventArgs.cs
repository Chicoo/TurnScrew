﻿
using System;
using System.Linq;


namespace TurnScrew.Wiki.PluginFramework
{

    /// <summary>
    /// Contains arguments for User Account Activity events.
    /// </summary>
    public class UserAccountActivityEventArgs : EventArgs
    {

        /// <summary>
        /// Initializes a new instance of the <b>UserAccountActivityEventArgs</b> class.
        /// </summary>
        /// <param name="user">The User Info the activity refers to.</param>
        /// <param name="activity">The activity performed.</param>
        public UserAccountActivityEventArgs(UserInfo user, UserAccountActivity activity)
        {
            User = user;
            Activity = activity;
        }

        /// <summary>
        /// Gets the user the activity refers to.
        /// </summary>
        public UserInfo User { get; private set; }

        /// <summary>
        /// Gets the activity performed.
        /// </summary>
        public UserAccountActivity Activity { get; private set; }

    }

    /// <summary>
    /// Lists legal User Account Activity types.
    /// </summary>
    public enum UserAccountActivity
    {
        /// <summary>
        /// A user account has been added.
        /// </summary>
        AccountAdded,
        /// <summary>
        /// A user account has been activated.
        /// </summary>
        AccountActivated,
        /// <summary>
        /// A user account has been deactivated.
        /// </summary>
        AccountDeactivated,
        /// <summary>
        /// A user account has been modified (email, password).
        /// </summary>
        AccountModified,
        /// <summary>
        /// A user account has been removed.
        /// </summary>
        AccountRemoved,
        /// <summary>
        /// A user account group membership has been changed.
        /// </summary>
        AccountMembershipChanged
    }

}
