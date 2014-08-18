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
    public class DingdanController : BaseController
    {
        //
        // GET: /Dingdan/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetDingdanList()
        {
            ControllerResultModel resultModel = new ControllerResultModel();
            try
            {
                DingdanFilterModel model = JsonConvert.DeserializeObject<DingdanFilterModel>(Request["argsJson"]);
                int totalCount;
                List<DingdanGridModel> models = WebHelper.DingdanService.GetDingdan(model, out totalCount);
                resultModel.Add("dingdanList", models);
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
                DingdanDetailsModel model = JsonConvert.DeserializeObject<DingdanDetailsModel>(Request["argsJson"]);
                WebHelper.DingdanService.Create(model);
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
            this.ViewBag.dingdanModelJson = JsonConvert.SerializeObject(WebHelper.DingdanService.GetDingdan(id));
            return View();
        }

        public ActionResult EditPost()
        {
            ControllerResultModel resultModel = new ControllerResultModel();
            try
            {
                DingdanDetailsModel model = JsonConvert.DeserializeObject<DingdanDetailsModel>(Request["argsJson"]);
                WebHelper.DingdanService.Change(model);
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
                string[] dingdanIdArray = Request["dingdanId"].Split(',');
                foreach (string dingdanId in dingdanIdArray)
                {
                    WebHelper.DingdanService.Delete(dingdanId);
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

        public ActionResult JisuanDingdanMingxi()
        {
            ControllerResultModel resultModel = new ControllerResultModel();
            try
            {
                DingdanMingxiDetailsModel model = JsonConvert.DeserializeObject<DingdanMingxiDetailsModel>(Request["argsJson"]);
                model = WebHelper.DingdanService.JisuanMingxi(model);
                resultModel.Add("dingdanMingxi", model);
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
