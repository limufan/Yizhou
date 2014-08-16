using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yizhou.Website.Models;
using Yizhou.Api;
using Yizhou.Website.Models;

namespace Yizhou.Website.Controllers
{
    public class LoginController : BaseController
    {
        //
        // GET: /Login/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SignIn(string account, string password, bool remember, string returnUrl)
        {
            ControllerResultModel resultModel = new ControllerResultModel();
            try
            {
                string token = WebHelper.AuthenticationService.SignIn(account, password, Request.UserHostAddress);
                resultModel.Add("data", this.Url.Action("Redirect", new { url = returnUrl, token = token, remember = remember }));
            }
            catch (Exception ex)
            {
                resultModel.result = false;
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            return Json(resultModel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SignOut()
        {
            WebHelper.AuthenticationService.SignOut(WebHelper.CurrentUserToken);
            return this.RedirectToAction("Index");
        }

        public ActionResult Redirect(string url, string token, bool remember)
        {
            if (string.IsNullOrEmpty(url))
            {
                url = this.Url.Action("Index", "Dingdan");
            }

            WebHelper.SetCurrentUserToken(token, remember);
            return this.Redirect(url);
        }
    }
}
