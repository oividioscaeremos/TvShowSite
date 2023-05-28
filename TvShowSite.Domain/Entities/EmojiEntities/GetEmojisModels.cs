using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvShowSite.Domain.Common;

namespace TvShowSite.Domain.Entities.EmojiEntities
{
    public class GetEmojisResponseEntity
    {
        public int Id { get; set; }
        public string? EmojiClass { get; set; }
        public string? EmojiName { get; set; }
    }

    public class GetEmojisResponse : BaseResponse<List<GetEmojisResponseEntity>>
    {

    }
}
