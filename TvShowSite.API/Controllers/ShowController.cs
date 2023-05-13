using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TvShowSite.Core.Helpers;
using TvShowSite.Domain.Entities.ShowEntities;
using TvShowSite.Service.Services;

namespace TvShowSite.API.Controllers
{
    [Route("api/show")]
    public class ShowController : BaseController
    {
        private readonly ShowService _showService;

        public ShowController(
            ShowService showService,
            LogHelper logHelper, 
            IHttpContextAccessor httpContextAccessor) : base(logHelper, httpContextAccessor)
        {
            _showService = showService;
        }

        public async Task<ActionResult> GetShowDescriptionAsync([FromBody]ShowDescriptionSearchRequest request)
        {
            return await ExecuteAsync(async () =>
            {
                return await _showService.GetShowDescriptionAsync(request);
            });
        }
    }
}
