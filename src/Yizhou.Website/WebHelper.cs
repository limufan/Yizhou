using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
using Yizhou.Api.Organization;
using Yizhou.Website.Api;

namespace Yizhou.Website
{
    public class WebHelper
    {
        static WebHelper()
        {
            log4net.Config.XmlConfigurator.Configure();
            Logger = log4net.LogManager.GetLogger("logger");

            Spring.Context.IApplicationContext ctx = Spring.Context.Support.ContextRegistry.GetContext();
            UserService = (IUserService)ctx["UserService"];
            PositionService = (IPositionService)ctx["PositionService"];
            AuthenticationService = (IAuthenticationService)ctx["AuthenticationService"];
            KehuService = (IKehuService)ctx["KehuService"];
            ChanpinService = (IChanpinService)ctx["ChanpinService"];
            DingdanService = (IDingdanService)ctx["DingdanService"];
            Version = "1.0.1";
        }

        public static ILog Logger { private set; get; }

        public static IUserService UserService { private set; get; }

        public static IPositionService PositionService { private set; get; }

        public static IAuthenticationService AuthenticationService { private set; get; }

        public static IKehuService KehuService { private set; get; }

        public static IChanpinService ChanpinService { private set; get; }

        public static IDingdanService DingdanService { private set; get; }

        public static UserInfo CurrentUserInfo
        {
            get
            {
                UserInfo userInfo = AuthenticationService.GetAuthenticatedUser(CurrentUserToken);
                if (userInfo == null)
                {
                    HttpContext.Current.Response.Redirect("~/Login?returnUrl=" + HttpContext.Current.Server.UrlEncode(HttpContext.Current.Request.Url.ToString()));
                }
                return userInfo;
            }
        }

        public static string CurrentUserAccount
        {
            get
            {
                return CurrentUserInfo.Account;
            }
        }

        public static string CurrentUserToken
        {
            get
            {
                string token = HttpContext.Current.Request["token"];

                return token;
            }
        }

        public static void SetCurrentUserToken(string token, bool remember)
        {
            HttpCookie tokenCookie = null;
            if (HttpContext.Current.Response.Cookies["token"] != null)
            {
                tokenCookie = HttpContext.Current.Response.Cookies["token"];
                tokenCookie.Value = token;
            }
            else
            {
                tokenCookie = new HttpCookie("token", token);
            }
            if (remember)
            {
                tokenCookie.Expires = DateTime.Now.AddYears(1);
            }
            HttpContext.Current.Response.Cookies.Add(tokenCookie);
        }

        public static string Version { set; get; }
    }
}