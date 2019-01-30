using System;
using Newtonsoft.Json;

namespace WordPressly.Models
{
    public class EventVenue
    {
        public int Id { get; set; }

        [JsonProperty("author")]
        public int AuthorId { get; set; }

        public string Status { get; set; }

        public DateTime Date { get; set; }

        [JsonProperty("date_utc")]
        public DateTime DateUtc { get; set; }

        public DateTime Modified { get; set; }

        [JsonProperty("modified_utc")]
        public DateTime ModifiedUtc { get; set; }

        public string Url { get; set; }

        public string Venue { get; set; }

        public string Slug { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string Province { get; set; }

        [JsonProperty("stateprovince")]
        public string StateProvince { get; set; }

        [JsonProperty("get_lat")]
        public double Latitude { get; set; }

        [JsonProperty("geo_lng")]
        public double Longitude { get; set; }

        [JsonProperty("show_map")]
        public bool ShowMap { get; set; }

        [JsonProperty("show_map_link")]
        public bool ShowMapLink { get; set; }

        [JsonProperty("global_id")]
        public string GlobalId { get; set; }
    }
}
