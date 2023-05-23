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

        [HttpGet]
        [Route("get_description")]
        public async Task<ActionResult> GetShowDescriptionAsync(int? showId)
        {
            return await ExecuteAsync(async () =>
            {
                return await _showService.GetShowDescriptionAsync(showId);
            });
        }
        
        [HttpGet]
        [Route("get_name")]
        public async Task<ActionResult> GetShowNameAsync(int? showId)
        {
            return await ExecuteAsync(async () =>
            {
                return await _showService.GetShowNameAsync(showId);
            });
        }
        
        [HttpGet]
        [Route("get_poster")]
        public async Task<ActionResult> GetShowPosterURLAsync(int? showId)
        {
            return await ExecuteAsync(async () =>
            {
                return await _showService.GetShowPosterURLAsync(showId);
            });
        }

        [HttpGet]
        [Route("get_favorite_characters")]
        public async Task<ActionResult> GetShowFavoriteCharactersAsync(int? showId, int? episodeId)
        {
            return await ExecuteAsync(async () =>
            {
                return await _showService.GetShowFavoriteCharactersAsync(showId, episodeId);
            });
        }

        [HttpGet]
        [Route("get_seasons_episodes")]
        public async Task<ActionResult> GetShowSeasonsEpisodesAsync(int? showId)
        {
            return await ExecuteAsync(async () =>
            {
                return await _showService.GetShowSeasonsEpisodesAsync(showId);
            });
        }
        
        [HttpGet]
        [Route("get_user_show_status")]
        public async Task<ActionResult> GetUserShowStatusAsync(int? showId)
        {
            return await ExecuteAsync(async () =>
            {
                return await _showService.GetUserShowStatusAsync(showId, UserId);
            });
        }
    }
}
