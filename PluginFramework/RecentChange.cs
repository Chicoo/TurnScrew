
using System;


namespace TurnScrew.Wiki.PluginFramework
{

    /// <summary>
    /// Represents a Change.
    /// </summary>
    public class RecentChange
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="T:RecentChange" /> class.
        /// </summary>
        /// <param name="page">The page pame.</param>
        /// <param name="title">The page title.</param>
        /// <param name="messageSubject">The message subject (or <c>null</c>).</param>
        /// <param name="dateTime">The date/time.</param>
        /// <param name="user">The user.</param>
        /// <param name="change">The change.</param>
        /// <param name="descr">The description (optional).</param>
        public RecentChange(string page, string title, string messageSubject, DateTime dateTime, string user, Change change, string descr)
        {
            Page = page;
            Title = title;
            MessageSubject = messageSubject;
            DateTime = dateTime;
            User = user;
            Change = change;
            Description = descr;
        }

        /// <summary>
        /// Gets the page name.
        /// </summary>
        public string Page { get; private set; }

        /// <summary>
        /// Gets the page title.
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// Gets the message subject (or <c>null</c>).
        /// </summary>
        public string MessageSubject { get; private set; } = null;

        /// <summary>
        /// Gets the date/time.
        /// </summary>
        public DateTime DateTime { get; private set; }

        /// <summary>
        /// Gets the user.
        /// </summary>
        public string User { get; private set; }

        /// <summary>
        /// Gets the change.
        /// </summary>
        public Change Change { get; private set; }

        /// <summary>
        /// Gets the description (optional).
        /// </summary>
        public string Description { get; private set; } = null;

    }

    /// <summary>
    /// Lists possible changes.
    /// </summary>
    public enum Change
    {
        /// <summary>
        /// A page was updated.
        /// </summary>
        PageUpdated,
        /// <summary>
        /// A page was deleted.
        /// </summary>
        PageDeleted,
        /// <summary>
        /// A page was rolled back.
        /// </summary>
        PageRolledBack,
        /// <summary>
        /// A page was renamed.
        /// </summary>
        PageRenamed,
        /// <summary>
        /// A message was posted to a page discussion.
        /// </summary>
        MessagePosted,
        /// <summary>
        /// A message was deleted from a page discussion.
        /// </summary>
        MessageDeleted,
        /// <summary>
        /// A message was edited in a page discussion.
        /// </summary>
        MessageEdited
    }

}
