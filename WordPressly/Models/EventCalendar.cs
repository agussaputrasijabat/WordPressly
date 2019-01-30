using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace WordPressly.Models
{
    public class EventCalendar
    {
        /// <summary>
        /// Unique identifier for the object.
        /// </summary>
        /// <remarks>
        /// Read only
        /// Context: view, edit, embed
        /// </remarks>
        [JsonProperty("id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int Id { get; set; }

        [JsonProperty("global_id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string GlobalId { get; set; }

        [JsonProperty("global_id_lineage", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string[] GlobalIdLineage { get; set; }

        [JsonProperty("author", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int Author { get; set; }

        [JsonProperty("status", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Status { get; set; }

        /// <summary>
        /// The date the object was published, in the site's timezone.
        /// </summary>
        /// <remarks>Context: view, edit, embed</remarks>
        [JsonProperty("date", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTime Date { get; set; }

        /// <summary>
        /// The date the object was published, as UTC.
        /// </summary>
        /// <remarks>Context: view, edit</remarks>
        [JsonProperty("date_utc", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTime DateUtc{ get; set; }

        /// <summary>
        /// The date the object was last modified, in the site's timezone.
        /// </summary>
        /// <remarks>
        /// Read only
        /// Context: view, edit
        /// </remarks>
        [JsonProperty("modified", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTime Modified { get; set; }

        /// <summary>
        /// The date the object was last modified, as UTC.
        /// </summary>
        /// <remarks>
        /// Read only
        /// Context: view, edit
        /// </remarks>
        [JsonProperty("modified_utc", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTime ModifiedUtc { get; set; }

        [JsonProperty("url", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Url { get; set; }

        [JsonProperty("rest_url", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string RestUrl { get; set; }

        [JsonProperty("", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty("description", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Description { get; set; }

        [JsonProperty("excerpt", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Excerpt { get; set;}

        [JsonProperty("slug", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Slug { get; set; }

        [JsonProperty("image", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public EventMediaDetails Image { get; set; }

        [JsonProperty("all_day", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool AllDay { get; set; }

        [JsonProperty("start_date", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTime StartDate { get; set; }

        [JsonProperty("end_date", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTime EndDate { get; set; }

        [JsonProperty("timezone", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string TimeZone { get; set; }

        [JsonProperty("cost", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Cost { get; set; }

        [JsonProperty("website", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Website { get; set; }

        [JsonProperty("show_map", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool ShowMap { get; set; }

        [JsonProperty("show_map_link", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool ShowMapLink { get; set; }

        [JsonProperty("hide_from_listings", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool HideFromListings { get; set; }

        [JsonProperty("sticky", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool Sticky { get; set; }

        [JsonProperty("featured", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool Featured { get; set; }

        public List<EventCategory> Categories { get; set; }

        public EventVenue Venue { get; set; }

        [JsonProperty("organizer")]
        public List<EventOrganizer> Organizers { get; set; }

        public bool Ticketed { get; set; }
    }
}
