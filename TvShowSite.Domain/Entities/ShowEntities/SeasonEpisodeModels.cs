using TvShowSite.Domain.Common;

namespace TvShowSite.Domain.Entities.ShowEntities
{
    public class SeasonEpisodeResponseEntity
    {
        public int ShowId { get; set; }
        public int SeasonId { get; set; }
        public int EpisodeId { get; set; }
        public string? SeasonName { get; set; }
        public string? EpisodeNumber { get; set; }
        public string? EpisodeName { get; set; }
        public bool IsWatched { get; set; }
    }

    public class SeasonEpisodeResponse : BaseResponse<List<SeasonEpisodeResponseEntity>>
    {
        public SeasonEpisodeResponse()
        {
            this.Value = new List<SeasonEpisodeResponseEntity>();
        }
    }
}
