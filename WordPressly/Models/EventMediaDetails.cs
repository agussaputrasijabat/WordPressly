using System.Collections.Generic;
using Newtonsoft.Json;

namespace WordPressly.Models
{
    public class EventMediaDetails
    {
        /// <summary>
        /// Media id
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// Media width
        /// </summary>
        [JsonProperty("width")]
        public int Width { get; set; }
        /// <summary>
        /// Media height
        /// </summary>
        [JsonProperty("height")]
        public int Height { get; set; }
        /// <summary>
        /// Url
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }
        /// <summary>
        /// Extension
        /// </summary>
        [JsonProperty("extension")]
        public string Extension { get; set; }
        /// <summary>
        /// Sizes
        /// </summary>
        [JsonProperty("sizes")]
        public IDictionary<string, EventMediaSize> Sizes { get; set; }
    }
}
