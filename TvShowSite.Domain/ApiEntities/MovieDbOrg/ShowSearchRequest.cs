using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TvShowSite.Domain.ApiEntities
{
    public class MovieDbShowSearchRequest
    {
        public string Language { get; set; } = "en-US";
        public string? Query { get; set; }
        [JsonProperty("include_adult")]
        public bool IncludeAdult { get; set; }
        public int Page { get; set; } = 1;
    }

    public class MovieDbShowResponseEntity
    {
        [JsonProperty("poster_path")]
        public string? PosterPath { get; set; }
        public double? Popularity { get; set; }
        public int? Id { get; set; }
        [JsonProperty("backdrop_path")]
        public string? BackdropPath { get; set; }
        [JsonProperty("vote_average")]
        public double? VoteAverage { get; set; }
        public string? Overview { get; set; }
        [JsonProperty("vote_count")]
        public int? VoteCount { get; set; }
        public string? Name { get; set; }
        [JsonProperty("original_name")]
        public string? OriginalName { get; set; }
        [JsonProperty("origin_country")]
        public List<string>? OriginCountry { get; set; }
    }

    public class MovieDbShowSearchResponse
    {
        public int? Page { get; set; }
        public List<MovieDbShowResponseEntity> Results { get; set; }
        [JsonProperty("total_results")]
        public int? TotalResults { get; set; }
        [JsonProperty("total_pages")]
        public int? TotalPages { get; set; }

        public MovieDbShowSearchResponse()
        {
            this.Results = new List<MovieDbShowResponseEntity>();
        }
    }
}
