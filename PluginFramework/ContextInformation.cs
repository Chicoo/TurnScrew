using Microsoft.AspNetCore.Http;

namespace TurnScrew.Wiki.PluginFramework
{
    /// <summary>
    /// Contains information about the Context of the page formatting.
    /// </summary>
    public class ContextInformation
    {
        /// <summary>
        /// Initializes a new instance of the <b>FormatContext</b> class.
        /// </summary>
        /// <param name="forIndexing">A value indicating whether the formatting is being done for content indexing.</param>
        /// <param name="forWysiwyg">A value indicating whether the formatting is being done for display in the WYSIWYG editor.</param>
        /// <param name="context">The formatting context.</param>
        /// <param name="page">The Page Information, if any, <c>null</c> otherwise.</param>
        /// <param name="language">The current Thread's language (for example "en-US").</param>
        /// <param name="httpContext">The current HTTP Context object.</param>
        /// <param name="username">The current User's Username (or <c>null</c>).</param>
        /// <param name="groups">The groups the user is member of (or <c>null</c>).</param>
        public ContextInformation(bool forIndexing, bool forWysiwyg, FormattingContext context, PageInfo page, string language, HttpContext httpContext, string username, string[] groups)
        {
            ForIndexing = forIndexing;
            ForWysiwyg = forWysiwyg;
            Context = context;
            Page = page;
            Language = language;
            HttpContext = httpContext;
            Username = username;
            Groups = groups;
        }

        /// <summary>
        /// Gets a value indicating whether the formatting is being done for content indexing.
        /// </summary>
        public bool ForIndexing { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the formatting is being done for display in the WYSIWYG editor.
        /// </summary>
        public bool ForWysiwyg { get; private set; }

        /// <summary>
        /// Gets the formatting context.
        /// </summary>
        public FormattingContext Context { get; private set; }

        /// <summary>
        /// Gets the Page Information.
        /// </summary>
        public PageInfo Page { get; private set; }

        /// <summary>
        /// Gets the current Thread's Language (for example en-US).
        /// </summary>
        public string Language { get; private set; }

        /// <summary>
        /// Gets the current HTTP Context object.
        /// </summary>
        /// <remarks>The context might not be valid when doing an index rebuild (see <see cref="ForIndexing"/>).</remarks>
        public HttpContext HttpContext { get; private set; }

        /// <summary>
        /// Gets the Username of the current User (or <c>null</c>).
        /// </summary>
        /// <remarks>If the Username is not available, the return value is <c>null</c>.</remarks>
        public string Username { get; private set; }

        /// <summary>
        /// Gets the groups the user is member of (or <c>null</c>).
        /// </summary>
        public string[] Groups { get; private set; }

    }

    /// <summary>
    /// Lists formatting contexts.
    /// </summary>
    public enum FormattingContext
    {
        /// <summary>
        /// The overall header.
        /// </summary>
        Header,
        /// <summary>
        /// The overall footer.
        /// </summary>
        Footer,
        /// <summary>
        /// The sidebar.
        /// </summary>
        Sidebar,
        /// <summary>
        /// The page header.
        /// </summary>
        PageHeader,
        /// <summary>
        /// The page footer.
        /// </summary>
        PageFooter,
        /// <summary>
        /// The page content.
        /// </summary>
        PageContent,
        /// <summary>
        /// Transcluded page content.
        /// </summary>
        TranscludedPageContent,
        /// <summary>
        /// The body of a message.
        /// </summary>
        MessageBody,
        /// <summary>
        /// Any other context.
        /// </summary>
        Other,
        /// <summary>
        /// No know context.
        /// </summary>
        Unknown
    }

}
