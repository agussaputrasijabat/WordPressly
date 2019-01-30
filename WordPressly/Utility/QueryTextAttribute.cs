using System;
using System.Collections.Generic;
using System.Text;

namespace WordPressly.Utility
{
    /// <summary>
    /// Attribute for set Text in querybuilder
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class QueryTextAttribute : Attribute
    {
        /// <summary>
        /// Text property uses in HTTP query string
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="text">text uses in HTTP query string</param>
        public QueryTextAttribute(string text)
        {
            Text = text;
        }
    }
}
