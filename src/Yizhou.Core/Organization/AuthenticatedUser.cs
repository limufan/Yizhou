﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yizhou.Api.Organization;

namespace Yizhou.Core.Organization
{
    public class AuthenticatedUser
    {
        public AuthenticatedUser(string account, string ipAddress, string token, DateTime loginTime)
        {
            this.Account = account;
            this.IpAddress = ipAddress;
            this.Token = token;
            this.LoginTime = loginTime;
        }

        public string Account {private set; get;}

        public string IpAddress { private set; get; }

        public string Token { private set; get; }

        public DateTime LoginTime { private set; get; }
    }
}
