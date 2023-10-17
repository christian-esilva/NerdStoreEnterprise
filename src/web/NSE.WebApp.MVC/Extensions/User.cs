using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace NSE.WebApp.MVC.Extensions
{
    public class User : IUser
    {
        private readonly IHttpContextAccessor _acessor;

        public User(IHttpContextAccessor acessor)
        {
            _acessor = acessor;
        }

        public string Name => _acessor.HttpContext.User.Identity.Name;

        public string GetByEmail() 
            => IsAuthenticated() ? _acessor.HttpContext.User.GetUserEmail() : "";

        public Guid GetById() 
            => IsAuthenticated() ? Guid.Parse(_acessor.HttpContext.User.GetUserId()) : Guid.Empty;

        public IEnumerable<Claim> GetClaims()
            => _acessor.HttpContext.User.Claims;

        public HttpContext GetHttpContext()
            => _acessor.HttpContext;

        public string GetToken() 
            => IsAuthenticated() ? _acessor.HttpContext.User.GetUserToken() : "";

        public bool HasRole(string role)
            => _acessor.HttpContext.User.IsInRole(role);

        public bool IsAuthenticated()
            => _acessor.HttpContext.User.Identity.IsAuthenticated;
    }
}
