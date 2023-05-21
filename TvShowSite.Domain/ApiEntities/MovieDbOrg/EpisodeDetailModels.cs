using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TvShowSite.Domain.ApiEntities.MovieDbOrg
{
    public class EpisodeDetail
    {
        public int Id { get; set; }
        [JsonProperty("air_date")]
        public DateTime? AirDate { get; set; }
        [JsonProperty("episode_number")]
        public int EpisodeNumber { get; set; }
        public string? Name { get; set; }
        [JsonProperty("overview")]
        public string? Description { get; set; }
        [JsonProperty("season_number")]
        public int SeasonNumber { get; set; }
        [JsonProperty("still_path")]
        public string? PosterURL { get; set; }
    }
}
