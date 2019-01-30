using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace WordPressly.Models
{
    public class EventMediaSize
    {
        /// <summary>
        /// Media Width
        /// </summary>
        [JsonProperty("width")]
        public int? Width { get; set; }
        /// <summary>
        /// Media Height
        /// </summary>
        [JsonProperty("height")]
        public int? Height { get; set; }
        /// <summary>
        /// Mime Type
        /// </summary>
        [JsonProperty("mime-type")]
        public string MimeType { get; set; }
        /// <summary>
        /// Url of source media
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
