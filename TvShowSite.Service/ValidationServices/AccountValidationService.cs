using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvShowSite.Domain.ApiEntities.MovieDbOrg;
using TvShowSite.Domain.Entities.AccountEntities;

namespace TvShowSite.Service.ValidationServices
{
    internal static class AccountValidationService
    {
        public static List<string> ValidateRegisterRequest(RegisterRequest request)
        {
            var response = new List<string>();

            if(request is not null)
            {
                if (string.IsNullOrEmpty(request.Username))
                {
                    response.Add("Username cannot be empty.");
                }
                if (string.IsNullOrEmpty(request.Password))
                {
                    response.Add("Password cannot be empty.");
                }
                if (string.IsNullOrEmpty(request.EmailAddress))
                {
                    response.Add("Mail address cannot be empty.");
                }
            }
            else
            {
                response.Add("Request cannot be empty.");
            }    

            return response;
        }

        public static List<string> ValidateLoginRequest(LoginRequest request)
        {
            var response = new List<string>();

            if (request is not null)
            {
                if (string.IsNullOrEmpty(request.Username))
                {
                    response.Add("Username cannot be empty.");
                }
                if (string.IsNullOrEmpty(request.Password))
                {
                    response.Add("Password cannot be empty.");
                }
            }
            else
            {
                response.Add("Request cannot be empty.");
            }

            return response;
        }

        public static List<string> ValidateGenerateTokenFromRefreshTokenRequest(RefreshTokenRequest request)
        {
            var response = new List<string>();

            if (request is not null)
            {
                if (string.IsNullOrEmpty(request.RefreshToken))
                {
                    response.Add("RefreshToken cannot be empty.");
                }
            }
            else
            {
                response.Add("Request cannot be empty.");
            }

            return response;
        }
    }
}
