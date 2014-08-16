using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Yizhou.Website.Api;
using Yizhou.Website.Models;

namespace Yizhou.Website.Controllers
{
    public class KehuController : BaseController
    {
        //
        // GET: /Kehu/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetKehuList()
        {
            ControllerResultModel resultModel = new ControllerResultModel();
            try
            {
                KehuFilterModel model = JsonConvert.DeserializeObject<KehuFilterModel>(Request["argsJson"]);
                int totalCount;
                List<KehuGridModel> models = WebHelper.KehuService.GetKehu(model, out totalCount);
                resultModel.Add("kehuList", models);
                resultModel.Add("totalCount", totalCount);
            }
            catch (Exception ex)
            {
                resultModel.result = false;
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            return Json(resultModel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult CreatePost()
        {
            ControllerResultModel resultModel = new ControllerResultModel();
            try
            {
                KehuDetailsModel model = JsonConvert.DeserializeObject<KehuDetailsModel>(Request["argsJson"]);
                WebHelper.KehuService.Create(model);
            }
            catch (Exception ex)
            {
                resultModel.result = false;
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            return Json(resultModel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(string id)
        {
            this.ViewBag.kehuModelJson = JsonConvert.SerializeObject(WebHelper.KehuService.GetKehu(id));
            return View();
        }

        public ActionResult EditPost()
        {
            ControllerResultModel resultModel = new ControllerResultModel();
            try
            {
                KehuDetailsModel model = JsonConvert.DeserializeObject<KehuDetailsModel>(Request["argsJson"]);
                WebHelper.KehuService.Change(model);
            }
            catch (Exception ex)
            {
                resultModel.result = false;
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            return Json(resultModel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete()
        {
            ControllerResultModel resultModel = new ControllerResultModel();
            try
            {
                string[] kehuIdArray = Request["kehuId"].Split(',');
                foreach (string kehuId in kehuIdArray)
                {
                    WebHelper.KehuService.Delete(kehuId);
                }
            }
            catch (Exception ex)
            {
                resultModel.result = false;
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            return Json(resultModel, JsonRequestBehavior.AllowGet);
        }
    }
}
