namespace TurnScrew.Wiki
{

    /// <summary>
    /// Class used for exchanging data between the <b>Core</b> library and the Wiki engine.
    /// </summary>
    public static class Exchanger
    {

        /// <summary>
        /// Gets or sets the singleton instance of the Resource Exchanger object.
        /// </summary>
        public static IResourceExchanger ResourceExchanger { get; set; }

    }

}
