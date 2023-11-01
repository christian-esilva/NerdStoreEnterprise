using Microsoft.AspNetCore.Http;
using NSE.WebApi.Core.User;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace NSE.WebApp.MVC.Extensions
{
    public class User : IAspNetUser
    {
        private readonly IHttpContextAccessor _acessor;

        public User(IHttpContextAccessor acessor)
        {
            _acessor = acessor;
        }

        public string Name => _acessor.HttpContext.User.Identity.Name;

        public string GetUserEmail() 
            => IsAuthenticated() ? _acessor.HttpContext.User.GetUserEmail() : "";

        public Guid GetUserId() 
            => IsAuthenticated() ? Guid.Parse(_acessor.HttpContext.User.GetUserId()) : Guid.Empty;

        public IEnumerable<Claim> GetClaims()
            => _acessor.HttpContext.User.Claims;

        public HttpContext GetHttpContext()
            => _acessor.HttpContext;

        public string GetUserToken() 
            => IsAuthenticated() ? _acessor.HttpContext.User.GetUserToken() : "";

        public bool HasRole(string role)
            => _acessor.HttpContext.User.IsInRole(role);

        public bool IsAuthenticated()
            => _acessor.HttpContext.User.Identity.IsAuthenticated;
    }
}
