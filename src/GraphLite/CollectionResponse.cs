using System;
using System.Collections.Generic;
using System.Text;

namespace GraphLite
{
    public class GraphList<T>
    {
        public GraphList(IEnumerable<T> items, string skipToken = null)
        {
            Items = new List<T>(items);
            SkipToken = skipToken;
        }

        public List<T> Items { get; set; }
        public string SkipToken { get; set; }
    }
}