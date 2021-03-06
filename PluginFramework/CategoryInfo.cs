using System;
using System.Collections.Generic;

namespace TurnScrew.Wiki.PluginFramework
{
    /// <summary>
    /// Represents a Page Category. A page can be binded with one or more categories (within the same Provider); this class manages this binding.
    /// </summary>
    public class CategoryInfo
    {
        /// <summary>
        /// The namespace of the Category.
        /// </summary>
        protected string _nSpace;
        /// <summary>
        /// The Name of the Category.
        /// </summary>
        protected string _name;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:CategoryInfo" /> class.
        /// </summary>
        /// <param name="fullName">The Full Name of the Category.</param>
        /// <param name="provider">The Storage that manages the category.</param>
        public CategoryInfo(string fullName, IPagesStorageProviderV30 provider)
        {
            NameTools.ExpandFullName(fullName, out _nSpace, out _name);
            Provider = provider;
        }

        /// <summary>
        /// Gets or sets the full name of the Category, such as 'Namespace.Category' or 'Category'.
        /// </summary>
        public string FullName
        {
            get { return NameTools.GetFullName(_nSpace, _name); }
            set { NameTools.ExpandFullName(value, out _nSpace, out _name); }
        }

        /// <summary>
        /// Gets or sets the Provider that manages the Category.
        /// </summary>
        public IPagesStorageProviderV30 Provider { get; set; }

        /// <summary>
        /// Gets or sets the Page array, containing their names.
        /// </summary>
        public string[] Pages { get; set; } = new string[0];

        /// <summary>
        /// Gets a string representation of the current object.
        /// </summary>
        /// <returns>The string representation.</returns>
        public override string ToString()
        {
            return NameTools.GetFullName(_nSpace, _name);
        }

    }

    /// <summary>
    /// Compares two <b>CategoryInfo</b> objects, using the FullName as parameter.
    /// </summary>
    /// <remarks>The comparison is <b>case insensitive</b>.</remarks>
    public class CategoryNameComparer : IComparer<CategoryInfo>
    {

        /// <summary>
        /// Compares two <see cref="CategoryInfo"/> objects, using the FullName as parameter.
        /// </summary>
        /// <param name="x">The first object.</param>
        /// <param name="y">The second object.</param>
        /// <returns>The comparison result (-1, 0 or 1).</returns>
        public int Compare(CategoryInfo x, CategoryInfo y)
        {
            return StringComparer.OrdinalIgnoreCase.Compare(x.FullName, y.FullName);
        }

    }

}
