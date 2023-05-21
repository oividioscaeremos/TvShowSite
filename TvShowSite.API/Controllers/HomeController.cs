using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TvShowSite.Core.Helpers;
using TvShowSite.Data.Repositories;
using TvShowSite.Service.Services;

namespace TvShowSite.API.Controllers
{
    [Route("home")]
    [Authorize]
    public class HomeController : BaseController
    {
        private readonly HomeService _homeService;
        private readonly UserTableRepository _userTableRepository;
        public HomeController(
            LogHelper logHelper, 
            IHttpContextAccessor httpContextAccessor,
            HomeService homeService,
            UserTableRepository userTableRepository) : base(logHelper, httpContextAccessor)
        {
            _homeService = homeService;
            _userTableRepository = userTableRepository;
        }

        [HttpGet]
        [Route("get_user_next_to_watch")]
        public async Task<ActionResult> GetUserNextToWatchAsync()
        {
            return await ExecuteAsync(async () =>
            {
                return await _homeService.GetUserNextToWatchAsync(UserId);
            });
        }
    }
}
