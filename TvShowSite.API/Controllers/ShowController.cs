using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TvShowSite.Core.Helpers;
using TvShowSite.Domain.Entities.ShowEntities;
using TvShowSite.Service.Services;

namespace TvShowSite.API.Controllers
{
    [Route("show")]
    [Authorize]
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

        [HttpPost]
        [Route("search")]
        public async Task<IActionResult> SearchAsync([FromBody] ShowSearchRequest request)
        {
            return await ExecuteAsync(async () =>
            {
                return await _showService.SearchAsync(request, UserId);
            });
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddShowAsync([FromBody] AddShowRequest request)
        {
            return await ExecuteAsync(async () =>
            {
                return await _showService.AddShowAsync(request, UserId);
            });
        }
        
        [HttpPost]
        [Route("remove")]
        public async Task<IActionResult> RemoveShowAsync([FromBody] RemoveShowRequest request)
        {
            return await ExecuteAsync(async () =>
            {
                return await _showService.RemoveShowAsync(request, UserId);
            });
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
