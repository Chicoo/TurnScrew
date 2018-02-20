using System;

namespace TurnScrew.Wiki.SearchEngine
{
    /// <summary>
    /// Contains arguments for the <b>IndexChanged</b> event of the <see cref="IInMemoryIndex" /> interface.
    /// </summary>
    public class IndexChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IndexChangedEventArgs" /> class.
        /// </summary>
        /// <param name="document">The affected document.</param>
        /// <param name="change">The change performed.</param>
        /// <param name="changeData">The dumped change data.</param>
        /// <param name="state">A state object that is passed to the IndexStorer SaveDate/DeleteData function.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="change"/> is not <see cref="IndexChangeType.IndexCleared"/> and <paramref name="document"/> or <paramref name="changeData"/> are <c>null</c>.</exception>
        public IndexChangedEventArgs(IDocument document, IndexChangeType change, DumpedChange changeData, object state)
        {
            if (change != IndexChangeType.IndexCleared)
            {
                if (document == null) throw new ArgumentNullException("document");
                if (changeData == null) throw new ArgumentNullException("changeData");
            }

            Document = document;
            Change = change;
            ChangeData = changeData;
            State = state;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IndexChangedEventArgs" /> class.
        /// </summary>
        /// <param name="document">The affected document.</param>
        /// <param name="change">The change performed.</param>
        /// <param name="changeData">The dumped change data.</param>
        /// <param name="state">A state object that is passed to the IndexStorer SaveDate/DeleteData function.</param>
        /// <param name="result">The storer result, if any.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="change"/> is not <see cref="IndexChangeType.IndexCleared"/> and <paramref name="document"/> or <paramref name="changeData"/> are <c>null</c>.</exception>
        public IndexChangedEventArgs(IDocument document, IndexChangeType change, DumpedChange changeData, object state, IndexStorerResult result) : this(document, change, changeData, state)
        {

            Result = result;
        }

        /// <summary>
        /// Gets the affected document.
        /// </summary>
        public IDocument Document { get; }

        /// <summary>
        /// Gets the change performed.
        /// </summary>
        public IndexChangeType Change { get; }

        /// <summary>
        /// Gets the dumped change data.
        /// </summary>
        public DumpedChange ChangeData { get; }

        /// <summary>
        /// Gets the state object that is passed to the IndexStorer SaveDate/DeleteData function.
        /// </summary>
        public object State { get; }

        /// <summary>
        /// Gets or sets the index storer result, if any.
        /// </summary>
        public IndexStorerResult Result { get; set; } = null;
    }

    /// <summary>
    /// Lists valid index changes.
    /// </summary>
    public enum IndexChangeType
    {
        /// <summary>
        /// A document is added.
        /// </summary>
        DocumentAdded,
        /// <summary>
        /// A document is removed.
        /// </summary>
        DocumentRemoved,
        /// <summary>
        /// The index is cleared.
        /// </summary>
        IndexCleared
    }
}
