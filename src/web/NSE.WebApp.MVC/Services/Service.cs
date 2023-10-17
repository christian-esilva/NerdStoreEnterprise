﻿using NSE.WebApp.MVC.Extensions;
using System;
using System.Net.Http;

namespace NSE.WebApp.MVC.Services
{
    public abstract class Service
    {
        protected bool HandleErrorResponse(HttpResponseMessage response)
        {
            switch ((int)response.StatusCode)
            {
                case 401:
                case 403:
                case 404:
                case 500:
                    throw new CustomHttpResponseException(response.StatusCode);
                case 400:
                    return false;
            }

            response.EnsureSuccessStatusCode();
            return true;
        }
    }
}
