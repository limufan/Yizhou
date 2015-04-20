using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Yizhou.Website.Api;
using Yizhou.Website.Models;
using System.IO;
using NPOI.HSSF.UserModel;

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

        public ActionResult ComplexSelectSource(string keyword)
        {
            KehuFilterModel model = new KehuFilterModel { keyword = keyword, start = 0, size = 20 };
            int totalCount;
            List<KehuGridModel> kehuModels = WebHelper.KehuService.GetKehu(model, out totalCount).OrderBy(k => k.name).ToList();
            return Json(kehuModels, JsonRequestBehavior.AllowGet);
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            this.ExecuteQuanxianFilter(filterContext);
            base.OnActionExecuting(filterContext);
        }

        public ActionResult ExportToExcel()
        {
            ControllerResultModel resultModel = new ControllerResultModel();
            try
            {
                KehuFilterModel model = JsonConvert.DeserializeObject<KehuFilterModel>(Request["argsJson"]);
                int totalCount;
                List<KehuGridModel> models = WebHelper.KehuService.GetKehu(model, out totalCount);
                string fileName = this.ShengchengExcel(models);
                resultModel.Add("fileName", fileName);
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
            return this.File(Path.Combine(tempPath, fileName), "application/x-xls", "客户资料.xls");
        }

        private string ShengchengExcel(List<KehuGridModel> kehuList)
        {
            string path = Server.MapPath("~/客户资料模板.xls");
            FileStream stream = System.IO.File.OpenRead(path);
            HSSFWorkbook workbook = new HSSFWorkbook(stream);
            HSSFSheet sheet = workbook.GetSheetAt(0);

            int dataRowIndex = 1;
            foreach (KehuGridModel model in kehuList)
            {
                HSSFRow dataRow = sheet.CreateRow(dataRowIndex);
                int cellIndex = -1;
                var cell = dataRow.CreateCell(++cellIndex);
                cell.SetCellValue(model.name);
                cell = dataRow.CreateCell(++cellIndex);
                cell.SetCellValue(model.yewuyuan);
                cell = dataRow.CreateCell(++cellIndex);
                cell.SetCellValue(model.yewulv);
                cell = dataRow.CreateCell(++cellIndex);
                cell.SetCellValue(model.yewulvFangshi);
                cell = dataRow.CreateCell(++cellIndex);
                cell.SetCellValue(model.jiekuanFangshi);
                cell = dataRow.CreateCell(++cellIndex);
                cell.SetCellValue(model.gongsiDizhi);
                cell = dataRow.CreateCell(++cellIndex);
                cell.SetCellValue(model.shouhuoDizhi);
                cell = dataRow.CreateCell(++cellIndex);
                cell.SetCellValue(model.shouhuoren);
                cell = dataRow.CreateCell(++cellIndex);
                cell.SetCellValue(model.shouhuorenDianhua);
                cell = dataRow.CreateCell(++cellIndex);
                cell.SetCellValue(model.shoukuanren);
                cell = dataRow.CreateCell(++cellIndex);
                cell.SetCellValue(model.shoukuanrenDianhua);
                cell = dataRow.CreateCell(++cellIndex);
                cell.SetCellValue(model.chuanzhen);
                cell = dataRow.CreateCell(++cellIndex);
                cell.SetCellValue(model.email);
                dataRowIndex++;
            }

            string tempPath = Server.MapPath(string.Format("~/Temp/{0}.xls", Guid.NewGuid().ToString()));
            if (!Directory.Exists(Path.GetDirectoryName(tempPath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(tempPath));
            }
            Stream newStream = System.IO.File.Open(tempPath, FileMode.Create);
            workbook.Write(newStream);
            newStream.Close();

            stream.Close();
            workbook = null;
            sheet = null;

            return Path.GetFileName(tempPath);
        }
    }
}
