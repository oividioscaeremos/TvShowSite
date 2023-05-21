using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TvShowSite.Domain.ApiEntities.MovieDbOrg
{
    public class ShowDetail
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string? Name { get; set; }
        [JsonProperty("overview")]
        public string? Description { get; set; }
        [JsonProperty("origin_country")]
        public List<string>? OriginCountries { get; set; }
        [JsonProperty("original_name")]
        public string? OriginalName { get; set; }
        [JsonProperty("original_language")]
        public string? OriginalLanguage { get; set; }
        [JsonProperty("backdrop_path")]
        public string? PosterURL { get; set; }
        [JsonProperty("first_air_date")]
        public DateTime? FirstAirDate { get; set; }
        [JsonProperty("last_air_date")]
        public DateTime? LastAirDate { get; set; }
        [JsonProperty("in_production")]
        public bool? IsOnGoing { get; set; }
        public List<ShowDetailProductionCompany>? ProductionCompanies { get; set; }
        public List<int>? EpisodeRuntime { get; set; }
        public double? Popularity { get; set; }
        public List<ShowDetailSeason>? Seasons { get; set; }
    }

    public class ShowDetailProductionCompany
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
    }

    public class ShowDetailSeason
    {
        public int? Id { get; set; }
        [JsonProperty("air_date")]
        public DateTime AirDate { get; set; }
        [JsonProperty("episode_count")]
        public int EpisodeCount { get; set; }
        public string? Name { get; set; }
        [JsonProperty("Overview")]
        public string? Description { get; set; }
        [JsonProperty("poster_path")]
        public string? PosterURL { get; set; }
        [JsonProperty("season_number")]
        public int SeasonNumber { get; set; }
    }
}
