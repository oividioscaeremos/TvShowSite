using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                    response.Add("Kullanıcı adı boş olamaz.");
                }
                if (string.IsNullOrEmpty(request.Password))
                {
                    response.Add("Şifre boş olamaz.");
                }
                if (string.IsNullOrEmpty(request.EmailAddress))
                {
                    response.Add("Mail adresi boş olamaz.");
                }
            }
            else
            {
                response.Add("Talep boş olamaz.");
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
                    response.Add("Kullanıcı adı boş olamaz.");
                }
                if (string.IsNullOrEmpty(request.Password))
                {
                    response.Add("Şifre boş olamaz.");
                }
            }
            else
            {
                response.Add("Talep boş olamaz.");
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
                    response.Add("RefreshToken boş olamaz.");
                }
            }
            else
            {
                response.Add("Talep boş olamaz.");
            }

            return response;
        }
    }
}
