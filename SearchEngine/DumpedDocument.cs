using System;

namespace TurnScrew.Wiki.SearchEngine
{
    /// <summary>
    /// Represents a document structured for easy dumped on disk or database.
    /// </summary>
    /// <remarks>The class is <b>not thread-safe</b>.</remarks>
    public class DumpedDocument
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DumpedDocument" /> class.
        /// </summary>
        /// <param name="id">The document unique ID.</param>
        /// <param name="name">The document unique name.</param>
        /// <param name="title">The document title.</param>
        /// <param name="typeTag">The document type tag.</param>
        /// <param name="dateTime">The document date/time.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="name"/>, <paramref name="title"/> or <paramref name="typeTag"/> are <c>null</c>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="name"/>, <paramref name="title"/> or <paramref name="typeTag"/> are empty.</exception>
        public DumpedDocument(uint id, string name, string title, string typeTag, DateTime dateTime)
        {
            if (name == null) throw new ArgumentNullException("name");
            if (title == null) throw new ArgumentNullException("title");
            if (typeTag == null) throw new ArgumentNullException("typeTag");

            if (name.Length == 0) throw new ArgumentException("Name cannot be empty.", "name");
            if (title.Length == 0) throw new ArgumentException("Title cannot be empty.", "title");
            if (typeTag.Length == 0) throw new ArgumentException("Type Tag cannot be empty.", "typeTag");

            ID = id;
            Name = name;
            Title = title;
            TypeTag = typeTag;
            DateTime = dateTime;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DumpedDocument" /> class.
        /// </summary>
        /// <param name="document">The document do wrap for dumping.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="document"/> is <c>null</c>.</exception>
        public DumpedDocument(IDocument document)
        {
            if (document == null) throw new ArgumentNullException("document");

            ID = document.ID;
            Name = document.Name;
            Title = document.Title;
            TypeTag = document.TypeTag;
            DateTime = document.DateTime;
        }

        /// <summary>
        /// Gets or sets the document unique ID.
        /// </summary>
        public uint ID { get; set; }

        /// <summary>
        /// Gets the document unique name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the title of the document.
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Gets the document type tag.
        /// </summary>
        public string TypeTag { get; }

        /// <summary>
        /// Gets the document date/time.
        /// </summary>
        public DateTime DateTime { get; }
    }
}
