using System;
using System.Collections.Generic;
using System.Text;

using WordPressly.Repository;

namespace WordPressly
{
    public class WordPress
    {
        public static Posts Posts { get; internal set; }

        public static Categories Categories { get; internal set; }

        public static Media Media { get; internal set; }
    }
}
