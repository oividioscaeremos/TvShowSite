using TvShowSite.Domain.Attributes;
using TvShowSite.Domain.Common;

namespace TvShowSite.Domain.TableEntities.BagEntities
{
    [TableName("CharacterEpisode")]
    public sealed class CharacterEpisode : CommonEntity
    {
        public int CharacterId { get; set; }
        public int SeasonId { get; set; }
        public int EpisodeId { get; set; }
        public int VoteCount { get; set; }
    }
}
