using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;
using Yizhou.Api.Organization.Exceptions;
using Yizhou.Api.Organization;

namespace Yizhou.Core.Organization
{
    public class AuthenticationService : IAuthenticationService
    {
        public AuthenticationService(YizhouCoreManager yizhouManager)
        {
            this.OrganizationManager = yizhouManager.OrgManager;
        }

        public OrganizationManagement OrganizationManager { set; get; }

        public void AuthenticateToken(string token)
        {
            this.OrganizationManager.AuthenticationManager.AuthenticateToken(token);
        }

        public bool TryAuthenticateToken(string token)
        {
            return this.OrganizationManager.AuthenticationManager.TryAuthenticateToken(token);
        }

        public UserInfo GetAuthenticatedUser(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return null;
            }
            AuthenticatedUser authenticatedUser = this.OrganizationManager.AuthenticationManager.GetAuthenticatedUser(token);
            if (authenticatedUser != null)
            {
                User user = this.OrganizationManager.UserManager.GetUserByAccount(authenticatedUser.Account);
                if (user != null)
                {
                    return user.MapUserInfo();
                }
            }
            return null;
        }

        public void SignOut(string token)
        {
            this.OrganizationManager.AuthenticationManager.SignOut(token);
        }

        public SignInResult TrySignIn(string account, string password, string ip, out string token)
        {
            return this.OrganizationManager.AuthenticationManager.TrySignIn(account, password, ip, out token);
        }

        public string SignIn(string account, string password, string ip)
        {
            return this.OrganizationManager.AuthenticationManager.SignIn(account, password, ip);
        }
    }
}