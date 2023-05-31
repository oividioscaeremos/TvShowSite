using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Controllers;
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
        [Route("logout")]
        [HttpDelete]
        public async Task<IActionResult> LogoutAsync()
        {
            return await ExecuteAsync(async () =>
            {
                return await _accountService.LogoutAsync(UserId);
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

        [Authorize]
        [Route("upload_file")]
        [HttpPost]
        public async Task<IActionResult> UploadFileAsync(IFormFile formFile)
        {
            return await ExecuteAsync(async () =>
            {
                return await _accountService.UploadFileAsync(formFile);
            });
        }
        
        [Authorize]
        [Route("change_user_picture")]
        [HttpPost]
        public async Task<IActionResult> ChangeUserPictureAsync([FromBody] ChangeUserPictureRequest request)
        {
            return await ExecuteAsync(async () =>
            {
                return await _accountService.ChangeUserPictureAsync(request, UserId);
            });
        }
        
        [Authorize]
        [Route("change_mail")]
        [HttpPost]
        public async Task<IActionResult> ChangeMailAddressAsync([FromBody] ChangeMailAddressRequest request)
        {
            return await ExecuteAsync(async () =>
            {
                return await _accountService.ChangeMailAddressAsync(request, UserId);
            });
        }

        [Authorize]
        [Route("change_password")]
        [HttpPost]
        public async Task<IActionResult> ChangePasswordAsync([FromBody] ChangePasswordRequest request)
        {
            return await ExecuteAsync(async () =>
            {
                return await _accountService.ChangePasswordAsync(request, UserId);
            });
        }
        
        [Authorize]
        [Route("get_user_profile_detail")]
        [HttpPost]
        public async Task<IActionResult> GetUserProfileDetailAsync([FromBody] GetUserProfileDetailRequest request)
        {
            return await ExecuteAsync(async () =>
            {
                return await _accountService.GetUserProfileDetailAsync(request, UserId);
            });
        }
    }
}
