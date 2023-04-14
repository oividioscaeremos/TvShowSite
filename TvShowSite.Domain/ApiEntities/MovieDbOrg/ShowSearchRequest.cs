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
        [JsonPropertyName("include_adult")]
        public bool IncludeAdult { get; set; }
        public int Page { get; set; } = 1;
    }

    public class MovieDbShowResponseEntity
    {
        [JsonPropertyName("poster_path")]
        public string? PosterPath { get; set; }
        public int? Popularity { get; set; }
        public int? Id { get; set; }
        [JsonPropertyName("backdrop_path")]
        public string? BackdropPath { get; set; }
        [JsonPropertyName("vote_average")]
        public int? VoteAverage { get; set; }
        public string? Overview { get; set; }
        [JsonPropertyName("vote_count")]
        public int? VoteCount { get; set; }
        public string? Name { get; set; }
        [JsonPropertyName("original_name")]
        public string? OriginalName { get; set; }
    }

    public class MovieDbShowSearchResponse
    {
        public int? Page { get; set; }
        public List<MovieDbShowResponseEntity> Results { get; set; }
        [JsonPropertyName("total_results")]
        public int? TotalResults { get; set; }
        [JsonPropertyName("total_pages")]
        public int? TotalPages { get; set; }

        public MovieDbShowSearchResponse()
        {
            this.Results = new List<MovieDbShowResponseEntity>();
        }
    }
}
