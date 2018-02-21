
using System;
using System.Linq;


namespace TurnScrew.Wiki.PluginFramework
{

    /// <summary>
    /// Contains arguments for Page Activity events.
    /// </summary>
    public class PageActivityEventArgs : EventArgs
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="T:PageActivityEventArgs" /> class.
        /// </summary>
        /// <param name="page">The page the activity refers to.</param>
        /// <param name="pageOldName">The old name of the renamed page, or <c>null</c>.</param>
        /// <param name="author">The author of the activity, if available, <c>null</c> otherwise.</param>
        /// <param name="activity">The activity.</param>
        public PageActivityEventArgs(PageInfo page, string pageOldName, string author, PageActivity activity)
        {
            Page = page;
            PageOldName = pageOldName;
            Author = author;
            Activity = activity;
        }

        /// <summary>
        /// Gets the page the activity refers to.
        /// </summary>
        public PageInfo Page { get; private set; }

        /// <summary>
        /// Gets the old name of the renamed page, or <c>null</c>.
        /// </summary>
        public string PageOldName { get; private set; }

        /// <summary>
        /// Gets the author of the activity.
        /// </summary>
        public string Author { get; private set; }

        /// <summary>
        /// Gets the activity.
        /// </summary>
        public PageActivity Activity { get; private set; }

    }

    /// <summary>
    /// Lists legal Page Activity types.
    /// </summary>
    public enum PageActivity
    {
        /// <summary>
        /// A page has been created.
        /// </summary>
        PageCreated,
        /// <summary>
        /// A page has been modified.
        /// </summary>
        PageModified,
        /// <summary>
        /// A draft for a page has been saved.
        /// </summary>
        PageDraftSaved,
        /// <summary>
        /// A page has been renamed.
        /// </summary>
        PageRenamed,
        /// <summary>
        /// A page has been rolled back.
        /// </summary>
        PageRolledBack,
        /// <summary>
        /// A page's backups have been deleted.
        /// </summary>
        PageBackupsDeleted,
        /// <summary>
        /// A page has been deleted.
        /// </summary>
        PageDeleted,
        /// <summary>
        /// A message has been posted to a page discussion.
        /// </summary>
        MessagePosted,
        /// <summary>
        /// A message has been modified.
        /// </summary>
        MessageModified,
        /// <summary>
        /// A message has been deleted from a page discussion.
        /// </summary>
        MessageDeleted
    }

}
