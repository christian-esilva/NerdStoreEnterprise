﻿using System.Collections.Generic;

namespace NSE.WebApp.MVC.Models
{
    public class UserToken
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public IEnumerable<UserClaim> Claims { get; set; }
    }
}
