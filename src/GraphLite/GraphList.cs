using System;
using System.Collections.Generic;
using System.Text;

namespace GraphLite
{
    /// <summary>
    /// This is a collection response from the GraphApiClient.
    /// </summary>
    /// <typeparam name="T">The type of the items in the collection.</typeparam>
    public class GraphList<T>
    {
        public GraphList(IEnumerable<T> items, string skipToken = null)
        {
            Items = new List<T>(items);
            SkipToken = skipToken;
        }

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        public List<T> Items { get; set; }
        
        /// <summary>
        /// Gets or sets the skip token (it has a value when there is a next page of results).
        /// </summary>
        public string SkipToken { get; set; }

        /// <summary>
        /// Gets a value indicating whether this result has a next page of results (use <see cref="SkipToken"/> in the query).
        /// </summary>
        public bool HasMoreResults => !string.IsNullOrEmpty(SkipToken);
    }
}