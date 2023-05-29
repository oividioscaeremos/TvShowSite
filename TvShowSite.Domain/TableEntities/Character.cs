using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvShowSite.Domain.Attributes;
using TvShowSite.Domain.Common;

namespace TvShowSite.Domain.TableEntities
{
    [TableName("Character")]
    public class Character : BaseEntity
    {
        public int? MovieDbId { get; set; }
        public int ShowId { get; set; }
        public string? CharacterName { get; set; }
        public string? Role { get; set; }
        public string? PosterURL { get; set; }
        public int? CharacterOrder { get; set; }
    }
}
