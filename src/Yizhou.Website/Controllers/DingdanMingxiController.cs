using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Yizhou.Website.Api;
using Yizhou.Website.Models;

namespace Yizhou.Website.Controllers
{
    public class DingdanMingxiController : BaseController
    {
        //
        // GET: /DingdanMingxi/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetDingdanMingxiList()
        {
            ControllerResultModel resultModel = new ControllerResultModel();
            try
            {
                DingdanMingxiFilterModel model = JsonConvert.DeserializeObject<DingdanMingxiFilterModel>(Request["argsJson"]);
                DingdanMingxiListModel listModel = WebHelper.DingdanService.GetDingdanMingxi(model);
                resultModel.Add("dingdanMingxiList", listModel.dingdanMingxiList);
                resultModel.Add("totalCount", listModel.totalCount);
                resultModel.Add("zongjineSum", listModel.zongjineSum);
                resultModel.Add("yewufeiSum", listModel.yewufeiSum);
                resultModel.Add("tichengSum", listModel.tichengSum);
            }
            catch (Exception ex)
            {
                resultModel.result = false;
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            return Json(resultModel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ExportToExcel()
        {
            ControllerResultModel resultModel = new ControllerResultModel();
            try
            {
                resultModel.Add("fileName", "simple.docx");
            }
            catch (Exception ex)
            {
                resultModel.result = false;
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            return Json(resultModel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Download(string fileName)
        {
            string tempPath = Server.MapPath("~/Temp");
            return this.File(Path.Combine(tempPath, fileName), "application/msword", fileName);
        }
    }
}
