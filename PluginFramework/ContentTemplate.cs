using System;
using System.Collections.Generic;

namespace TurnScrew.Wiki.PluginFramework
{
    /// <summary>
    /// Contains a template for page content.
    /// </summary>
    public class ContentTemplate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:ContentTemplate" /> class.
        /// </summary>
        /// <param name="name">The name of the template.</param>
        /// <param name="content">The content of the template.</param>
        /// <param name="provider">The provider handling the template.</param>
        public ContentTemplate(string name, string content, IPagesStorageProviderV30 provider)
        {
            Name = name;
            Content = content;
            Provider = provider;
        }

        /// <summary>
        /// Gets the name of the template.
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Gets the content of the template.
        /// </summary>
        public string Content { get; protected set; }

        /// <summary>
        /// Gets the provider handling the template.
        /// </summary>
        public IPagesStorageProviderV30 Provider { get; protected set; }

    }

    /// <summary>
    /// Compares two <see cref="T:ContentTemplate" /> objects.
    /// </summary>
    public class ContentTemplateNameComparer : IComparer<ContentTemplate>
    {

        /// <summary>
        /// Compares the name of two <see cref="T:ContentTemplate" /> objects.
        /// </summary>
        /// <param name="x">The first <see cref="T:ContentTemplate" />.</param>
        /// <param name="y">The second <see cref="T:ContentTemplate" />.</param>
        /// <returns>The result of the comparison (1, 0 or -1).</returns>
        public int Compare(ContentTemplate x, ContentTemplate y)
        {
            return StringComparer.OrdinalIgnoreCase.Compare(x.Name, y.Name);
        }

    }
}
