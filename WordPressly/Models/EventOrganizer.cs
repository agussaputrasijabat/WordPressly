using System;
using Newtonsoft.Json;

namespace WordPressly.Models
{
    public class EventOrganizer
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

        public string Organizer { get; set; }

        public string Slug { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        [JsonProperty("global_id")]
        public string GlobalId { get; set; }
    }
}
