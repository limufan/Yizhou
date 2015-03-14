using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using NPOI.HSSF.UserModel;
using Yizhou.Website.Api;
using Yizhou.Website.Models;

namespace Yizhou.Website.Controllers
{
    public class ShoukuanController : BaseController
    {
        //
        // GET: /Shoukuan/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetShoukuanList()
        {
            ControllerResultModel resultModel = new ControllerResultModel();
            try
            {
                ShoukuanFilterModel model = JsonConvert.DeserializeObject<ShoukuanFilterModel>(Request["argsJson"]);
                ShoukuanListModel listModel = WebHelper.DingdanService.GetShoukuan(model);
                resultModel.Add("shoukuanList", listModel.shoukuanList);
                resultModel.Add("totalCount", listModel.totalCount);
                resultModel.Add("shoukuanJineSum", listModel.shoukuanJineSum);
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
                ShoukuanFilterModel model = JsonConvert.DeserializeObject<ShoukuanFilterModel>(Request["argsJson"]);
                model.start = 0;
                model.size = 5000;
                ShoukuanListModel listModel = WebHelper.DingdanService.GetShoukuan(model);
                string fileName = this.ShengchengExcel(listModel.shoukuanList);
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
            return this.File(Path.Combine(tempPath, fileName), "application/x-xls", "收款明细.xls");
        }

        private string ShengchengExcel(List<ShoukuanGridModel> models)
        {
            string path = Server.MapPath("~/ShoukuanTemplate.xls");
            FileStream stream = System.IO.File.OpenRead(path);
            HSSFWorkbook workbook = new HSSFWorkbook(stream);
            HSSFSheet sheet = workbook.GetSheetAt(0);

            int dataRowIndex = 1;
            foreach (ShoukuanGridModel model in models)
            {
                HSSFRow dataRow = sheet.CreateRow(dataRowIndex);
                var cell = dataRow.CreateCell(0);
                cell.SetCellValue(model.dingdanhao);
                cell = dataRow.CreateCell(1);
                cell.SetCellValue(model.yewuyuan);
                cell = dataRow.CreateCell(2);
                cell.SetCellValue(model.kehu);
                cell = dataRow.CreateCell(3);
                cell.SetCellValue(model.xiadanRiqi.ToString("yyyy-MM-dd"));
                cell = dataRow.CreateCell(4);
                cell.SetCellValue(model.jiekuanRiqi.ToString("yyyy-MM-dd"));
                cell = dataRow.CreateCell(5);
                cell.SetCellValue(Math.Round(model.shoukuanJine));
                cell = dataRow.CreateCell(6);
                cell.SetCellValue(Math.Round(model.ticheng, 2));
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

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            this.ExecuteQuanxianFilter(filterContext);
            base.OnActionExecuting(filterContext);
        }
    }
}
