﻿using Microsoft.AspNetCore.Http;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TvShowSite.Core.Helpers;
using TvShowSite.Data.Repositories;
using TvShowSite.Domain.Entities.AccountEntities;
using TvShowSite.Domain.TableEntities;
using TvShowSite.Service.ValidationServices;

namespace TvShowSite.Service.Services
{
    public class AccountService
    {
        private readonly SecurityHelper _securityHelper;
        private readonly AuthorizationRepository _authorizationRepository;
        private readonly UserTableRepository _userTableRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountService(
            SecurityHelper securityHelper,
            AuthorizationRepository authorizationRepository,
            UserTableRepository userTableRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _securityHelper = securityHelper;
            _authorizationRepository = authorizationRepository;
            _userTableRepository = userTableRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<RegisterResponse> RegisterAsync(RegisterRequest request)
        {
            var response = new RegisterResponse
            {
                ErrorList = AccountValidationService.ValidateRegisterRequest(request)
            };

            if(response.Status)
            {
                var userWithMailAddress = await _userTableRepository.GetUserByMailAddressAsync(request.EmailAddress!);
                var userWithUsername = await _userTableRepository.GetUserByUsernameAsync(request.Username!);

                if (userWithMailAddress is null && userWithUsername is null)
                {
                    var newUser = new UserTable
                    {
                        Username = request.Username,
                        Password = SecurityHelper.GetMD5Hash(request.Password!),
                        EmailAddress = request.EmailAddress
                    };

                    await _userTableRepository.InsertAsync(newUser, 0);
                }
                else if(userWithMailAddress is not null)
                {
                    response.ErrorList.Add("Bu mail adresiyle sisteme daha önce kayıt olunmuştur.");
                }
                else
                {
                    response.ErrorList.Add("Bu kullanıcı adıyla sisteme daha önce kayıt olunmuştur.");
                }
            }

            return response;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var response = new LoginResponse
            {
                ErrorList = AccountValidationService.ValidateLoginRequest(request)
            };

            if(response.Status)
            {
                var encryptedPassword = SecurityHelper.GetMD5Hash(request.Password!);

                var userDetails = await _userTableRepository.GetUserByUsernameAndEncryptedPasswordAsync(request.Username!, encryptedPassword);

                if(userDetails is not null)
                {
                    var expiresAt = DateTime.Now.AddMinutes(SettingsHelper.Settings?.AppSettings?.AccessTokenLifetimeInMinutes ?? 10080);

                    var (accessToken, refreshToken) = CreateAccessTokenAndRefreshToken(userDetails.Id, userDetails.Username!, expiresAt);

                    await _authorizationRepository.InsertAsync(new Authorization
                    {
                        Username = request.Username,
                        AccessToken = accessToken,
                        RefreshToken = refreshToken,
                        DidSucceed = true,
                        AccessTokenExpiresAt = expiresAt,
                        RefreshTokenExpiresAt = DateTime.Now.AddMinutes(SettingsHelper.Settings?.AppSettings?.RefreshTokenLifetimeInMinutes ?? 10080),
                        IPAddress = _securityHelper.GetClientIPAddress()
                    }, userDetails.Id);

                    response.Value = new LoginResponseEntity()
                    {
                        AccessToken = accessToken,
                        RefreshToken = refreshToken
                    };
                }
                else
                {
                    await _authorizationRepository.InsertAsync(new Authorization
                    {
                        Username = request.Username,
                        DidSucceed = true,
                        IPAddress = _securityHelper.GetClientIPAddress()
                    }, 0);

                    response.ErrorList.Add("Kullanıcı adı veya şifre yanlış.");
                }
            }

            return response;
        }

        public async Task<RefreshTokenResponse> GenerateTokenFromRefreshTokenAsync(RefreshTokenRequest request, string? username, int? userId)
        {
            var response = new RefreshTokenResponse
            {
                ErrorList = AccountValidationService.ValidateGenerateTokenFromRefreshTokenRequest(request)
            };

            if(string.IsNullOrEmpty(username) || !userId.HasValue)
            {
                response.ErrorList.Add("AccessToken doğrulanamadı.");
            }

            if(response.Status)
            {
                var activeAccessTokenEntry = await _authorizationRepository.GetByRefreshTokenAndUsernameAsync(request.RefreshToken!, username);

                if (activeAccessTokenEntry is not null)
                {
                    var expiresAt = DateTime.Now.AddMinutes(SettingsHelper.Settings?.AppSettings?.AccessTokenLifetimeInMinutes ?? 10080);

                    var (accessToken, refreshToken) = CreateAccessTokenAndRefreshToken(userId!.Value, username!, expiresAt);

                    await _authorizationRepository.MarkAsDeletedAsync(activeAccessTokenEntry, userId!.Value);

                    await _authorizationRepository.InsertAsync(new Authorization
                    {
                        Username = username,
                        AccessToken = accessToken,
                        RefreshToken = refreshToken,
                        DidSucceed = true,
                        AccessTokenExpiresAt = expiresAt,
                        RefreshTokenExpiresAt = DateTime.Now.AddMinutes(SettingsHelper.Settings?.AppSettings?.RefreshTokenLifetimeInMinutes ?? 10080),
                        IPAddress = _securityHelper.GetClientIPAddress()
                    }, userId.Value);

                    response.Value = new LoginResponseEntity()
                    {
                        AccessToken = accessToken,
                        RefreshToken = refreshToken
                    };
                }
                else
                {
                    await _authorizationRepository.InsertAsync(new Authorization
                    {
                        Username = username,
                        DidSucceed = false,
                        IPAddress = _securityHelper.GetClientIPAddress()
                    }, userId!.Value);

                    response.ErrorList.Add("Token yenileme işlemi yapılamıyor.");
                }
            }

            return response;
        }

        public async Task<string> LogoutAsync(int userId)
        {
            await _userTableRepository.MarkAsDeletedByUserIdAsync(userId);

            return "Success";
        }

        public async Task<UploadFileResponse> UploadFileAsync(IFormFile file)
        {
            var response = new UploadFileResponse();

            if (file is not null && file.Length > 0)
            {
                var fileNameWithExtension = Path.GetFileName(file.FileName);

                var fileExtension = fileNameWithExtension.Substring(fileNameWithExtension.LastIndexOf('.') + 1);

                if (new List<string> { "png", "jpg", "jpeg" }.Contains(fileExtension.ToLower()))
                {
                    var fullFileName = Guid.NewGuid().ToString() + "." + fileExtension;

                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\pictures", fullFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }

                    response.Value =  (SettingsHelper.Settings?.AppSettings?.ApiUrl ?? "") + "/pictures/" + fullFileName;
                }
                else
                {
                    response.ErrorList.Add("Poster türü PNG, JPG ve JPEG uzantılı olmalıdır.");
                }
            }
            else
            {
                response.ErrorList.Add("Could not detect any file to upload.");
            }

            return response;
        }

        public async Task<ChangeUserPictureResponse> ChangeUserPictureAsync(ChangeUserPictureRequest request, int userId)
        {
            var response = new ChangeUserPictureResponse()
            {
                ErrorList = AccountValidationService.ValidateChangeUserPictureRequest(request)
            };

            if (response.Status)
            {
                if (request.IsProfilePicture == true)
                {
                    await _userTableRepository.UpdateUserProfilePictureAsync(request.PictureUrl!, userId);
                }
                else
                {
                    await _userTableRepository.UpdateUserCoverPictureAsync(request.PictureUrl!, userId);
                }
            }

            return response;
        }

        public async Task<ChangeMailAddressResponse> ChangeMailAddressAsync(ChangeMailAddressRequest request, int userId)
        {
            var response = new ChangeMailAddressResponse()
            {
                ErrorList = AccountValidationService.ValidateChangeMailAddressRequest(request)
            };

            if (response.Status)
            {
                await _userTableRepository.UpdateUserMailAddressAsync(request.EmailAddress!, userId);
            }

            return response;
        }

        public async Task<ChangePasswordResponse> ChangePasswordAsync(ChangePasswordRequest request, int userId)
        {
            var response = new ChangePasswordResponse()
            {
                ErrorList = AccountValidationService.ValidateChangePasswordRequest(request)
            };

            if (response.Status)
            {
                var encodedPassword = SecurityHelper.GetMD5Hash(request.Password!);

                await _userTableRepository.UpdateUserPasswordAsync(encodedPassword, userId);
            }

            return response;
        }

        public async Task<GetUserProfileDetailResponse> GetUserProfileDetailAsync(GetUserProfileDetailRequest request, int userId)
        {
            var response = new GetUserProfileDetailResponse()
            {
                Value = new GetUserProfileDetailResponseEntity()
            };

            response.Value.IsUsersOwnProfile = request.UserId.HasValue ? request.UserId == userId : true;

            int requestUserId = request.UserId ?? userId;

            var user = await _userTableRepository.GetByIdAsync(requestUserId);

            if (user is not null)
            {
                response.Value.ProfilePictureUrl = user.ProfilePictureURL;
                response.Value.CoverPictureUrl = user.CoverPictureURL;
                response.Value.MailAddress = response.Value.IsUsersOwnProfile ? user.EmailAddress : string.Empty;
                response.Value.Username = user.Username;
            }
            else
            {
                response.ErrorList.Add("User could not be found.");
            }

            return response;
        }

        private static (string? accessToken, string? refreshToken) CreateAccessTokenAndRefreshToken(int userId, string username, DateTime expiresAt)
        {
            var claims = new Dictionary<string, object>
            {
                { "UserId", userId },
                { "Username", username }
            };

            string accessToken = JwtAuthenticationHelper.CreateToken(claims, expiresAt);
            string refreshToken = JwtAuthenticationHelper.CreateRefreshToken();

            return (accessToken, refreshToken);
        }
    }
}
