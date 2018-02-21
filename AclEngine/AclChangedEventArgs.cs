﻿using System;

namespace TurnScrew.Wiki.AclEngine
{

    /// <summary>
    /// Contains arguments for the <see cref="IAclManager.AclChanged" /> event.
    /// </summary>
    public class AclChangedEventArgs : EventArgs
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="T:AclChangedEventArgs" /> class.
        /// </summary>
        /// <param name="entries">The entries that changed.</param>
        /// <param name="change">The change.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="entries"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="entries"/> is empty.</exception>
        public AclChangedEventArgs(AclEntry[] entries, Change change)
        {
            if (entries == null) throw new ArgumentNullException("entries");
            if (entries.Length == 0) throw new ArgumentException("Entries cannot be empty.", "entries");

            Entries = entries;
            Change = change;
        }

        /// <summary>
        /// Gets the entries that changed.
        /// </summary>
        public AclEntry[] Entries { get; }

        /// <summary>
        /// Gets the change.
        /// </summary>
        public Change Change { get; }

    }

    /// <summary>
    /// Lists legal changes for ACL entries.
    /// </summary>
    public enum Change
    {
        /// <summary>
        /// An entry is stored.
        /// </summary>
        EntryStored,
        /// <summary>
        /// An entry is deleted.
        /// </summary>
        EntryDeleted
    }

}
