using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yizhou.Api.Organization;
using Yizhou.Website.Api;

namespace Yizhou.Website.Controllers
{
    public class UserController : BaseController
    {
        //
        // GET: /User/

        public ActionResult ComplexSelectSource()
        {
            IList<UserInfo> userInfoList = WebHelper.UserService.GetAllUser();
            List<UserComplexSelectSourceModel> models = userInfoList.Select(u => new UserComplexSelectSourceModel { account = u.Account, name = u.Name }).ToList();
            return Json(models, JsonRequestBehavior.AllowGet); 
        }

    }
}
