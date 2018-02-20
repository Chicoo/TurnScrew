using System;

namespace TurnScrew.Wiki.SearchEngine
{
    /// <summary>
    /// Contains a word mapping data, structured for easy dumping on disk or database.
    /// </summary>
    /// <remarks>The class is <b>not thread-safe</b>.</remarks>
    public class DumpedWordMapping
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DumpedWordMapping" /> class.
        /// </summary>
        /// <param name="wordId">The word unique ID.</param>
        /// <param name="documentId">The document unique ID.</param>
        /// <param name="firstCharIndex">The index of the first character the word.</param>
        /// <param name="wordIndex">The index of the word in the original index.</param>
        /// <param name="location">The location identifier.</param>
        public DumpedWordMapping(uint wordId, uint documentId, ushort firstCharIndex, ushort wordIndex, byte location)
        {
            WordID = wordId;
            DocumentID = documentId;
            FirstCharIndex = firstCharIndex;
            WordIndex = wordIndex;
            Location = location;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DumpedWordMapping" /> class.
        /// </summary>
        /// <param name="wordId">The word unique ID.</param>
        /// <param name="documentId">The document unique ID.</param>
        /// <param name="info">The <see cref="BasicWordInfo" />.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="info"/> is <c>null</c>.</exception>
        public DumpedWordMapping(uint wordId, uint documentId, BasicWordInfo info)
        {
            if (info == null) throw new ArgumentNullException("info");

            WordID = wordId;
            DocumentID = documentId;
            FirstCharIndex = info.FirstCharIndex;
            WordIndex = info.WordIndex;
            Location = info.Location.Location;
        }

        /// <summary>
        /// Gets or sets the word unique ID.
        /// </summary>
        public uint WordID { get; set; }

        /// <summary>
        /// Gets the document unique ID.
        /// </summary>
        public uint DocumentID { get; }

        /// <summary>
        /// Gets the index of the first character of the word.
        /// </summary>
        public ushort FirstCharIndex { get; }

        /// <summary>
        /// Gets the index of the word in the original document.
        /// </summary>
        public ushort WordIndex { get; }

        /// <summary>
        /// Gets the location identifier.
        /// </summary>
        public byte Location { get; }
    }
}
