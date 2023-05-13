using TvShowSite.Domain.Common;

namespace TvShowSite.Domain.Entities.ShowEntities
{
    public class ShowSearchRequest
    {
        public string? Name { get; set; }
        public int? Page { get; set; }
        public int? PageSize { get; set; }
    }

    public class ShowSearchResponseEntity
    {
        public int? Id { get; set; }
        public string? ShowName { get; set; }
        public string? PosterURL { get; set; }
        public int? EpisodeCount { get; set; }
        public int? SeasonCount { get; set; }
    }

    public class ShowSearchResponse : BaseResponse<List<ShowSearchResponseEntity>>
    {
        public int? TotalResults { get; set; }
        public int? TotalPages { get; set; }

        public ShowSearchResponse()
        {
            Value = new List<ShowSearchResponseEntity>();
        }
    }
}
