using TvShowSite.Domain.Common;

namespace TvShowSite.Domain.Entities.ShowEntities
{
    public class ShowDescriptionSearchRequest
    {
        public int? Id { get; set; }
    }

    public class ShowDescriptionSearchResponse : BaseResponse<string>
    {

    }
}
