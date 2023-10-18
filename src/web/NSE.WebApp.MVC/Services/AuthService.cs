using Microsoft.Extensions.Options;
using NSE.WebApp.MVC.Extensions;
using NSE.WebApp.MVC.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace NSE.WebApp.MVC.Services
{
    public class AuthService : Service, IAuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.AuthenticationUrl);

            _httpClient = httpClient;
        }

        public async Task<UserLoginResponse> Login(UserLoginViewModel userLogin)
        {
            var loginContent = GetContent(userLogin);

            var response = await _httpClient.PostAsync("/api/identity/authenticate", loginContent);

            if (!HandleErrorResponse(response))
            {
                return new UserLoginResponse
                {
                    ResponseResult = await DeserializeObjectResponse<ResponseResult>(response)
                };
            };

            return await DeserializeObjectResponse<UserLoginResponse>(response);
        }

        public async Task<UserLoginResponse> Register(UserRegisterViewModel userRegister)
        {
            var registerContent = GetContent(userRegister);

            var response = await _httpClient.PostAsync("/api/identity/register", registerContent);

            if (!HandleErrorResponse(response))
            {
                return new UserLoginResponse
                {
                    ResponseResult = await DeserializeObjectResponse<ResponseResult>(response)
                };
            }

            return await DeserializeObjectResponse<UserLoginResponse>(response);
        }
    }
}
