using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TvShowSite.Core.Helpers;
using TvShowSite.Data.Repositories;

namespace TvShowSite.API.Controllers
{
    [Route("home")]
    [Authorize]
    public class HomeController : BaseController
    {
        private readonly UserTableRepository _userTableRepository;
        public HomeController(
            LogHelper logHelper, 
            IHttpContextAccessor httpContextAccessor,
            UserTableRepository userTableRepository) : base(logHelper, httpContextAccessor)
        {
            _userTableRepository = userTableRepository;
        }

        [HttpGet]
        [Route("get_user_shows")]
        public async Task<ActionResult> GetUserShowsAsync()
        {
            return await ExecuteAsync(async () =>
            {
                return await _userTableRepository.GetAllAsync();
            });
        }
    }
}
