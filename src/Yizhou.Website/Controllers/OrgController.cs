using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Yizhou.Api.Organization;
using Yizhou.Website.Api;
using Yizhou.Website.Controllers;
using Yizhou.Website.Models;

namespace Yizhou.Website
{
    public class OrgController : BaseController
    {
        //
        // GET: /Org/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MoidfyCurrentUserInfo(string name, string email, string oldPassword, string newPassword)
        {
            ControllerResultModel resultModel = new ControllerResultModel();
            try
            {
                UserChangeInfo changeInfo = new UserChangeInfo(this.CurrentUser);
                changeInfo.Name = name;
                changeInfo.Email = email;
                WebHelper.UserService.ChangeInfo(this.CurrentUser.ID, changeInfo);

                if (!string.IsNullOrEmpty(newPassword))
                {
                    WebHelper.UserService.ChangePassword(this.CurrentUser.Account, oldPassword, newPassword);
                }
            }
            catch (Exception ex)
            {
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            return Json(resultModel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCurrentUserInfo()
        {
            ControllerResultModel resultModel = new ControllerResultModel();
            try
            {
                UserDetailsModel model = new UserDetailsModel();
                model.account = this.CurrentUser.Account;
                model.email = this.CurrentUser.Email;
                model.id = this.CurrentUser.ID;
                model.name = this.CurrentUser.Name;
                resultModel.Add("data", model);
            }
            catch (Exception ex)
            {
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            return Json(resultModel, JsonRequestBehavior.AllowGet);
        }
    }
}
