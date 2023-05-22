using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvShowSite.Domain.Common;

namespace TvShowSite.Domain.Entities.ShowEntities
{
    public class FavoriteCharactersResponsEntity
    {
        public string? CharacterName { get; set; }
        public int? VoteCount { get; set; }
        public string? PosterURL { get; set; }
    }

    public class FavoriteCharactersResponse : BaseResponse<List<FavoriteCharactersResponsEntity>>
    {

    }
}
