using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yizhou.Api.Organization;

namespace Yizhou.Website.Controllers
{
    public class BaseController : Controller
    {
        private UserInfo _currentUser;
        protected UserInfo CurrentUser
        {
            get
            {
                if (this._currentUser == null)
                {
                    this._currentUser = WebHelper.CurrentUserInfo;
                }
                return this._currentUser;
            }
        }

        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new NewtonJsonResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!WebHelper.HasDingdanGuanliQuanxian && !(this is DingdanMingxiController))
            {
                filterContext.Result = this.RedirectToAction("Index", "DingdanMingxi");
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
