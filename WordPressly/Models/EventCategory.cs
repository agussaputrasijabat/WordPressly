using Newtonsoft.Json;

namespace WordPressly.Models
{
    public class EventCategory
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Slug { get; set; }

        [JsonProperty("term_group")]
        public int TermGroup { get; set; } 

        [JsonProperty("term_taxonomy_id")]
        public int TermTaxonomyId { get; set; }

        public string Taxonomy { get; set; }

        public string Description { get; set; }

        public int Parent { get; set; }

        public int Count { get; set; }

        public string Raw { get; set; }
    }
}
