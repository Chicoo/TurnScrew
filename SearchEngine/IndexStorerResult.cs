using System.Collections.Generic;

namespace TurnScrew.Wiki.SearchEngine
{
    /// <summary>
    /// Contains the results of an index storer operation.
    /// </summary>
    public class IndexStorerResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:IndexStorerResult" /> class.
        /// </summary>
        /// <param name="documentId">The ID of the document just stored, if any.</param>
        /// <param name="wordIds">The IDs of the words just stored, if any.</param>
        public IndexStorerResult(uint? documentId, List<WordId> wordIds)
        {
            DocumentID = documentId;
            WordIDs = wordIds;
        }

        /// <summary>
        /// Gets or sets the ID of the document just stored, if any.
        /// </summary>
        public uint? DocumentID { get; set; }

        /// <summary>
        /// Gets or sets the IDs of the words
        /// </summary>
        public List<WordId> WordIDs { get; set; }
    }

    /// <summary>
    /// Describes the ID of a word.
    /// </summary>
    public class WordId
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:WordId" /> class.
        /// </summary>
        /// <param name="text">The word text, lowercase.</param>
        /// <param name="id">The word ID.</param>
        public WordId(string text, uint id)
        {
            Text = text;
            ID = id;
        }

        /// <summary>
        /// Gets the word text.
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// Gets the word ID.
        /// </summary>
        public uint ID { get; }
    }
}
