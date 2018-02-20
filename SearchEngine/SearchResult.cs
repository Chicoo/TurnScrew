using System;

namespace TurnScrew.Wiki.SearchEngine
{
    /// <summary>
    /// Represents a search result.
    /// </summary>
    /// <remarks>Instance and static members are <b>not thread-safe</b>.</remarks>
    public class SearchResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SearchResult" /> class.
        /// </summary>
        /// <param name="document">The document the result refers to.</param>
        /// <remarks>The relevance is initially set to <b>0</b>.</remarks>
        /// <exception cref="ArgumentNullException">If <paramref name="document"/> is <c>null</c>.</exception>
        public SearchResult(IDocument document)
        {
            Document = document ?? throw new ArgumentNullException("document");
            Matches = new WordInfoCollection();
            Relevance = new Relevance(0);
        }

        /// <summary>
        /// Gets the document the result refers to.
        /// </summary>
        public IDocument Document { get; }

        /// <summary>
        /// Gets the matches in the document.
        /// </summary>
        public WordInfoCollection Matches { get; }

        /// <summary>
        /// Gets the relevance of the search result.
        /// </summary>
        public Relevance Relevance { get; }

        /// <summary>
        /// Gets a string representation of the current instance.
        /// </summary>
        /// <returns>The string representation.</returns>
        public override string ToString()
        {
            return Document.Name + "(" + Matches.Count.ToString() + " matches)";
        }
    }
}
