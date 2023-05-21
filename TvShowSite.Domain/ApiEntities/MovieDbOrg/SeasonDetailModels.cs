using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TvShowSite.Domain.ApiEntities.MovieDbOrg
{
    public class SeasonDetail
    {
        [JsonProperty("air_date")]
        public DateTime? AirDate { get; set; }
        [JsonProperty("episodes")]
        public List<SeasonDetailEpisode>? Episodes { get; set;}
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Id { get; set; }
        [JsonProperty("poster_path")]
        public string? PosterURL { get; set; }
        [JsonProperty("season_number")]
        public int SeasonNumber { get; set; }
    }

    public class SeasonDetailEpisode
    {
        public int Id { get; set; }
        [JsonProperty("episode_number")]
        public int EpisodeNumber { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        [JsonProperty("season_number")]
        public int SeasonNumber { get; set; }
        [JsonProperty("show_id")]
        public int ShowId { get; set; }
        [JsonProperty("still_path")]
        public string? PosterURL { get; set; }
    }
}
