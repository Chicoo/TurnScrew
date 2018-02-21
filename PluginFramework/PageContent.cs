
using System;


namespace TurnScrew.Wiki.PluginFramework
{

    /// <summary>
    /// Contains the Content of a Page.
    /// </summary>
    public class PageContent
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="T:PageContent"/> class.
        /// </summary>
        /// <param name="pageInfo">The PageInfo object.</param>
        /// <param name="title">The Title.</param>
        /// <param name="user">The User that last modified the Page.</param>
        /// <param name="lastModified">The last modification Date and Time.</param>
        /// <param name="comment">The Comment of the editor, about this revision.</param>
        /// <param name="content">The <b>unparsed</b> Content.</param>
        /// <param name="keywords">The keywords, usually used for SEO, or <c>null</c>.</param>
        /// <param name="description">The description, usually used for SEO, or <c>null</c>.</param>
        public PageContent(PageInfo pageInfo, string title, string user, DateTime lastModified, string comment, string content,
            string[] keywords, string description)
        {
            PageInfo = pageInfo;
            Title = title;
            User = user;
            LastModified = lastModified;
            Content = content;
            Comment = comment;
            Keywords = keywords != null ? keywords : new string[0];
            Description = description;
        }

        /// <summary>
        /// Gets the PageInfo.
        /// </summary>
        public PageInfo PageInfo { get; protected set; }

        /// <summary>
        /// Gets the Title.
        /// </summary>
        public string Title { get; protected set; }

        /// <summary>
        /// Gets the User.
        /// </summary>
        public string User { get; protected set; }

        /// <summary>
        /// Gets the last modification Date and Time.
        /// </summary>
        public DateTime LastModified { get; protected set; }

        /// <summary>
        /// Gets the Comment of the editor, about this revision.
        /// </summary>
        public string Comment { get; protected set; }

        /// <summary>
        /// Gets the <b>unformatted</b> Content.
        /// </summary>
        public string Content { get; protected set; }

        /// <summary>
        /// Gets the keywords, usually used for SEO.
        /// </summary>
        public string[] Keywords { get; protected set; }

        /// <summary>
        /// Gets the description, usually used for SEO.
        /// </summary>
        public string Description { get; protected set; }

        /// <summary>
        /// Gets or sets the Linked Pages, both existent and inexistent.
        /// </summary>
        public string[] LinkedPages { get; set; } = new string[0];

        /// <summary>
        /// Determines whether the current instance was built using <see cref="M:GetEmpty"/>.
        /// </summary>
        /// <returns><c>True</c> if the instance is empty, <c>false</c> otherwise.</returns>
        public bool IsEmpty()
        {
            return this is EmptyPageContent;
        }

        /// <summary>
        /// Gets an empty instance of <see cref="T:PageContent" />.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <returns>The instance.</returns>
        public static PageContent GetEmpty(PageInfo page)
        {
            return new EmptyPageContent(page);
        }

        /// <summary>
        /// Represents an empty page content.
        /// </summary>
        private class EmptyPageContent : PageContent
        {

            /// <summary>
            /// Initializes a new instance of the <see cref="T:EmptyPageContent"/> class.
            /// </summary>
            /// <param name="page">The page the content refers to.</param>
            public EmptyPageContent(PageInfo page)
                : base(page, string.Empty, string.Empty, DateTime.MinValue, string.Empty, string.Empty, null, string.Empty) { }

        }

    }

}
