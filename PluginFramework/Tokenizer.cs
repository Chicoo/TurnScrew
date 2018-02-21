using TurnScrew.Wiki.SearchEngine;

namespace TurnScrew.Wiki.PluginFramework
{

    /// <summary>
    /// Defines a delegate that tokenizes strings.
    /// </summary>
    /// <param name="content">The content to tokenize.</param>
    /// <returns>The tokenized words.</returns>
    public delegate WordInfo[] Tokenizer(string content);

}
