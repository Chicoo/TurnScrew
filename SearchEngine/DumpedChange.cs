using System;
using System.Collections.Generic;

namespace TurnScrew.Wiki.SearchEngine
{
    /// <summary>
    /// Represents a change occurred to the index, structured for easy dumping to disk or database.
    /// </summary>
    /// <remarks>The class is <b>not thread-safe</b>.</remarks>
    public class DumpedChange
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="DumpedChange" /> class.
        /// </summary>
        /// <param name="document">The dumped document data.</param>
        /// <param name="words">The list of dumped words data.</param>
        /// <param name="mappings">The list of dumped mappings data.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="document"/>, <paramref name="words"/> or <paramref name="mappings"/> are <c>null</c>.</exception>
        public DumpedChange(DumpedDocument document, List<DumpedWord> words, List<DumpedWordMapping> mappings)
        {
            Document = document ?? throw new ArgumentNullException("document");
            Words = words ?? throw new ArgumentNullException("words");
            Mappings = mappings ?? throw new ArgumentNullException("mappings");
        }

        /// <summary>
        /// Gets the dumped document data.
        /// </summary>
        public DumpedDocument Document { get; }

        /// <summary>
        /// Gets the list of dumped words data.
        /// </summary>
        public List<DumpedWord> Words { get; }

        /// <summary>
        /// Gets the list of dumped mappings data.
        /// </summary>
        public List<DumpedWordMapping> Mappings { get; }
    }
}
