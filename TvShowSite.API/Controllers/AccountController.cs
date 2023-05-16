using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TvShowSite.Core.Helpers;
using TvShowSite.Domain.Entities.AccountEntities;
using TvShowSite.Service.Services;

namespace TvShowSite.API.Controllers
{
    [Route("account")]
    [Authorize]
    public class AccountController : BaseController
    {
        private readonly AccountService _accountService;

        public AccountController(
            AccountService accountService,
            LogHelper logHelper, 
            IHttpContextAccessor httpContextAccessor) : base(logHelper, httpContextAccessor)
        {
            _accountService = accountService;
        }

        [AllowAnonymous]
        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest request)
        {
            return await ExecuteAsync(async () =>
            {
                return await _accountService.RegisterAsync(request);
            });
        }

        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request)
        {
            return await ExecuteAsync(async () =>
            {
                return await _accountService.LoginAsync(request);
            });
        }
        
        [Authorize(AuthenticationSchemes = "Timeless")]
        [Route("refresh_token")]
        [HttpPost]
        public async Task<IActionResult> GenerateRefreshTokenAsync([FromBody] RefreshTokenRequest request)
        {
            return await ExecuteAsync(async () =>
            {
                return await _accountService.GenerateTokenFromRefreshTokenAsync(request, Username, UserId);
            });
        }
    }
}
