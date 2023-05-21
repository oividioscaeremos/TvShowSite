using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TvShowSite.Core.Helpers;
using TvShowSite.Domain.Entities.ShowEntities;
using TvShowSite.Service.Services;

namespace TvShowSite.API.Controllers
{
    [Authorize]
    [Route("episode")]
    public class EpisodeController : BaseController
    {
        private readonly EpisodeService _episodeService;

        public EpisodeController(
            EpisodeService episodeService,
            LogHelper logHelper, 
            IHttpContextAccessor httpContextAccessor) : base(logHelper, httpContextAccessor)
        {
            _episodeService = episodeService;
        }

        [HttpPost]
        [Route("mark_as_watched")]
        public async Task<IActionResult> MarkAsWatchedAsync([FromBody] MarkAsWatchedRequest request)
        {
            return await ExecuteAsync(async () =>
            {
                return await _episodeService.MarkAsWatchedAsync(request, UserId);
            });
        }
    }
}
