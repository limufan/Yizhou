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
    public class ChanpinController : BaseController
    {
        //
        // GET: /Chanpin/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetChanpinList()
        {
            ControllerResultModel resultModel = new ControllerResultModel();
            try
            {
                ChanpinFilterModel model = JsonConvert.DeserializeObject<ChanpinFilterModel>(Request["argsJson"]);
                int totalCount;
                List<ChanpinGridModel> models = WebHelper.ChanpinService.GetChanpin(model, out totalCount);
                resultModel.Add("chanpinList", models);
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
                ChanpinDetailsModel model = JsonConvert.DeserializeObject<ChanpinDetailsModel>(Request["argsJson"]);
                WebHelper.ChanpinService.Create(model);
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
            this.ViewBag.chanpinModelJson = JsonConvert.SerializeObject(WebHelper.ChanpinService.GetChanpin(id));
            return View();
        }

        public ActionResult EditPost()
        {
            ControllerResultModel resultModel = new ControllerResultModel();
            try
            {
                ChanpinDetailsModel model = JsonConvert.DeserializeObject<ChanpinDetailsModel>(Request["argsJson"]);
                WebHelper.ChanpinService.Change(model);
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
                string[] chanpinIdArray = Request["chanpinId"].Split(',');
                foreach (string chanpinId in chanpinIdArray)
                {
                    WebHelper.ChanpinService.Delete(chanpinId);
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
