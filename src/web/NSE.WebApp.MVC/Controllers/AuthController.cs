﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Models;
using NSE.WebApp.MVC.Services.Contracts;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NSE.WebApp.MVC.Controllers
{
    public class AuthController : MainController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        [Route("new-account")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("new-account")]
        public async Task<IActionResult> Register(UserRegisterViewModel userRegister)
        {
            if (!ModelState.IsValid) return View(userRegister);

            var response = await _authService.Register(userRegister);

            if (HasErrorsResponse(response.ResponseResult)) return View(userRegister);

            await SignIn(response);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("login")]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login(UserLoginViewModel userLogin, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (!ModelState.IsValid) return View(userLogin);

            // API - Login
            var response = await _authService.Login(userLogin);

            if (HasErrorsResponse(response.ResponseResult)) return View(userLogin);

            // Realizar login
            await SignIn(response);

            if (string.IsNullOrEmpty(returnUrl)) return RedirectToAction("Index", "Home");

            return LocalRedirect(returnUrl);
        }

        [HttpGet]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        private async Task SignIn(UserLoginResponse userLoginResponse)
        {
            var token = GetFormattedToken(userLoginResponse.AccessToken);

            var claims = new List<Claim>
            {
                new Claim("JWT", userLoginResponse.AccessToken)
            };
            claims.AddRange(token.Claims);

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60),
                IsPersistent = true
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), authProperties);
        }

        private static JwtSecurityToken GetFormattedToken(string jwtToken)
        {
            return new JwtSecurityTokenHandler().ReadJwtToken(jwtToken) as JwtSecurityToken;
        }
    }
}
