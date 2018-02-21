namespace TurnScrew.Wiki.PluginFramework
{
    /// <summary>
    /// Contains information about a Provider.
    /// </summary>
    public class ComponentInformation
    {
        /// <summary>
        /// Initializes a new instance of the <b>ComponentInformation</b> class.
        /// </summary>
        /// <param name="name">The Name of the Component.</param>
        /// <param name="author">The Author of the Component.</param>
        /// <param name="version">The component version.</param>
        /// <param name="url">The info URL of the Component/Author.</param>
        /// <param name="updateUrl">The update URL of the component, or <c>null</c>.</param>
        public ComponentInformation(string name, string author, string version, string url, string updateUrl)
        {
            Name = name;
            Author = author;
            Version = version;
            Url = url;
            UpdateUrl = updateUrl;
        }

        /// <summary>
        /// Gets the Name of the Component.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the Author of the Component.
        /// </summary>
        public string Author { get; private set; }

        /// <summary>
        /// Gets the component version.
        /// </summary>
        public string Version { get; private set; }

        /// <summary>
        /// Gets the info URL of the Component/Author.
        /// </summary>
        public string Url { get; private set; }

        /// <summary>
        /// Gets the update URL of the component.
        /// </summary>
        public string UpdateUrl { get; private set; }

    }
}
