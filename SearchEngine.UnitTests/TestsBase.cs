using Moq;
using System;
using System.Collections.Generic;

namespace TurnScrew.Wiki.SearchEngine.UnitTests
{
    public class TestsBase
    {
        /// <summary>
        /// Demo content for a plain-text document.
        /// </summary>
        protected const string PlainTextDocumentContent = "This is some content.";

        /// <summary>
        /// Demo content for a plain-text document.
        /// </summary>
        protected const string PlainTextDocumentContent2 = "Dummy text used for testing purposes.";

        /// <summary>
        /// Demo content for a plain-text document.
        /// </summary>
        protected const string PlainTextDocumentContent3 = "Todo.";

        /// <summary>
        /// Demo content for a plain-text document.
        /// </summary>
        protected const string PlainTextDocumentContent4 = "Content with repeated content.";

        /// <summary>
        /// The words contained in the demo content (<b>PlainTextDocumentContent</b>).
        /// </summary>
        protected WordInfo[] PlainTextDocumentWords = new WordInfo[] {
            new WordInfo("This", 0, 0, WordLocation.Content),
            new WordInfo("is", 5, 1, WordLocation.Content),
            new WordInfo("some", 8, 2, WordLocation.Content),
            new WordInfo("content", 13, 3, WordLocation.Content)
        };

        /// <summary>
        /// The words contained in the demo content (<b>PlainTextDocumentContent2</b>).
        /// </summary>
        protected WordInfo[] PlainTextDocumentWords2 = new WordInfo[] {
            new WordInfo("Dummy", 0, 0, WordLocation.Content),
            new WordInfo("text", 6, 1, WordLocation.Content),
            new WordInfo("used", 11, 2, WordLocation.Content),
            new WordInfo("for", 16, 3, WordLocation.Content),
            new WordInfo("testing", 20, 4, WordLocation.Content),
            new WordInfo("purposes", 28, 5, WordLocation.Content)
        };

        /// <summary>
        /// The words contained in the demo content (<b>PlainTextDocumentContent3</b>).
        /// </summary>
        protected WordInfo[] PlainTextDocumentWords3 = new WordInfo[] {
            new WordInfo("Todo", 0, 0, WordLocation.Content)
        };

        /// <summary>
        /// The words contained in the demo content (<b>PlainTextDocumentContent4</b>).
        /// </summary>
        protected WordInfo[] PlainTextDocumentWords4 = new WordInfo[] {
            new WordInfo("Content", 0, 0, WordLocation.Content),
            new WordInfo("with", 8, 1, WordLocation.Content),
            new WordInfo("repeated", 13, 2, WordLocation.Content),
            new WordInfo("content", 22, 3, WordLocation.Content)
        };

        /// <summary>
        /// Mocks an index, inheriting from IndexBase.
        /// </summary>
        /// <returns>The index.</returns>
        public IInMemoryIndex MockInMemoryIndex()
        {
            InMemoryIndexBase index = new Mock<InMemoryIndexBase>().Object;
            return index;
        }

        /// <summary>
        /// Mocks a document with a fixed content.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="title">The title.</param>
        /// <param name="typeTag">The type tag.</param>
        /// <param name="dateTime">The date/time.</param>
        /// <returns>The mocked document.</returns>
        public IDocument MockDocument(string name, string title, string typeTag, DateTime dateTime)
        {
            var moqDocument = new Mock<IDocument>();
            moqDocument.Setup(d => d.Name).Returns(name);
            moqDocument.Setup(d => d.ID).Returns(1);
            moqDocument.Setup(d => d.Title).Returns(title);
            moqDocument.Setup(d => d.TypeTag).Returns(typeTag);
            moqDocument.Setup(d => d.DateTime).Returns(dateTime);

            moqDocument.Setup(d => d.Tokenize(It.IsIn(title))).Returns(Tools.Tokenize(title, WordLocation.Title));
            moqDocument.Setup(d => d.Tokenize(It.IsNotIn(title))).Returns(PlainTextDocumentWords);

            return moqDocument.Object;
        }

        /// <summary>
        /// Mocks a document with a fixed content.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="title">The title.</param>
        /// <param name="typeTag">The type tag.</param>
        /// <param name="dateTime">The date/time.</param>
        /// <returns>The mocked document.</returns>
        public IDocument MockDocument2(string name, string title, string typeTag, DateTime dateTime)
        {
            var moqDocument = new Mock<IDocument>();
            moqDocument.Setup(d => d.Name).Returns(name);
            moqDocument.Setup(d => d.ID).Returns(2);
            moqDocument.Setup(d => d.Title).Returns(title);
            moqDocument.Setup(d => d.TypeTag).Returns(typeTag);
            moqDocument.Setup(d => d.DateTime).Returns(dateTime);

            moqDocument.Setup(d => d.Tokenize(It.IsIn(title))).Returns(Tools.Tokenize(title, WordLocation.Title));
            moqDocument.Setup(d => d.Tokenize(It.IsNotIn(title))).Returns(PlainTextDocumentWords2);

            return moqDocument.Object;
        }

        /// <summary>
        /// Mocks a document with a fixed content.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="title">The title.</param>
        /// <param name="typeTag">The type tag.</param>
        /// <param name="dateTime">The date/time.</param>
        /// <returns>The mocked document.</returns>
        public IDocument MockDocument3(string name, string title, string typeTag, DateTime dateTime)
        {
            var moqDocument = new Mock<IDocument>();
            moqDocument.Setup(d => d.Name).Returns(name);
            moqDocument.Setup(d => d.ID).Returns(3);
            moqDocument.Setup(d => d.Title).Returns(title);
            moqDocument.Setup(d => d.TypeTag).Returns(typeTag);
            moqDocument.Setup(d => d.DateTime).Returns(dateTime);

            moqDocument.Setup(d => d.Tokenize(It.IsIn(title))).Returns(Tools.Tokenize(title, WordLocation.Title));
            moqDocument.Setup(d => d.Tokenize(It.IsNotIn(title))).Returns(PlainTextDocumentWords3);

            return moqDocument.Object;
        }

        /// <summary>
        /// Mocks a document with a fixed content.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="title">The title.</param>
        /// <param name="typeTag">The type tag.</param>
        /// <param name="dateTime">The date/time.</param>
        /// <returns>The mocked document.</returns>
        public IDocument MockDocument4(string name, string title, string typeTag, DateTime dateTime)
        {
            var moqDocument = new Mock<IDocument>();
            moqDocument.Setup(d => d.Name).Returns(name);
            moqDocument.Setup(d => d.ID).Returns(4);
            moqDocument.Setup(d => d.Title).Returns(title);
            moqDocument.Setup(d => d.TypeTag).Returns(typeTag);
            moqDocument.Setup(d => d.DateTime).Returns(dateTime);

            moqDocument.Setup(d => d.Tokenize(It.IsIn(title))).Returns(Tools.Tokenize(title, WordLocation.Title));
            moqDocument.Setup(d => d.Tokenize(It.IsNotIn(title))).Returns(PlainTextDocumentWords4);

            return moqDocument.Object;
        }

        public uint FreeDocumentId = 1;
        public uint FreeWordId = 1;

        public void AutoHandlerForDocumentStorage(object sender, IndexChangedEventArgs e)
        {
            List<WordId> ids = new List<WordId>();
            if (e.ChangeData != null && e.ChangeData.Words != null)
            {
                foreach (DumpedWord w in e.ChangeData.Words)
                {
                    ids.Add(new WordId(w.Text, FreeWordId));
                    FreeWordId++;
                }
            }

            e.Result = new IndexStorerResult(FreeDocumentId, ids);
            FreeDocumentId++;
        }
    }
}
