using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvShowSite.Domain.Attributes;
using TvShowSite.Domain.Common;

namespace TvShowSite.Domain.TableEntities.BagEntities
{
    [TableName("UserEpisodeNote")]
    public class UserEpisodeNote : BaseEntity
    {
        public int UserId { get; set; }
        public int EpisodeId { get; set; }
        public string? Content { get; set; }
    }
}
