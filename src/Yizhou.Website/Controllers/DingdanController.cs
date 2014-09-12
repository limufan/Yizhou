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
                DingdanListModel listModel = WebHelper.DingdanService.GetDingdan(model);
                resultModel.Add("dingdanList", listModel.dingdanList);
                resultModel.Add("totalCount", listModel.totalCount);
                resultModel.Add("tichengSum", listModel.tichengSum.ToString("0.00"));
                resultModel.Add("weishoukuanJineSum", listModel.weishoukuanJineSum.ToString("0.00"));
                resultModel.Add("yingshoukuanJineSum", listModel.yingshoukuanJineSum.ToString("0.00"));
                resultModel.Add("yishoukuanJineSum", listModel.yishoukuanJineSum.ToString("0.00"));
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

        public ActionResult JisuanShoukuanTicheng()
        {
            ControllerResultModel resultModel = new ControllerResultModel();
            try
            {
                DingdanDetailsModel dingdanModel = JsonConvert.DeserializeObject<DingdanDetailsModel>(Request["dingdanJson"]);
                ShoukuanDetailsModel shoukuanModel = JsonConvert.DeserializeObject<ShoukuanDetailsModel>(Request["shoukuanJson"]);
                double ticheng = WebHelper.DingdanService.JisuanTicheng(dingdanModel, shoukuanModel);
                resultModel.Add("ticheng", ticheng);
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
                DingdanFilterModel model = JsonConvert.DeserializeObject<DingdanFilterModel>(Request["argsJson"]);
                model.start = 0;
                model.size = 5000;
                DingdanListModel listModel = WebHelper.DingdanService.GetDingdan(model);
                string fileName = this.ShengchengExcel(listModel.dingdanList);
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
            return this.File(Path.Combine(tempPath, fileName), "application/x-xls", "订单.xls");
        }

        private string ShengchengExcel(List<DingdanGridModel> models)
        {
            string path = Server.MapPath("~/DingdanTemplate.xls");
            FileStream stream = System.IO.File.OpenRead(path);
            HSSFWorkbook workbook = new HSSFWorkbook(stream);
            HSSFSheet sheet = workbook.GetSheetAt(0);

            int dataRowIndex = 1;
            foreach (DingdanGridModel model in models)
            {
                HSSFRow dataRow = sheet.CreateRow(dataRowIndex);
                var cell = dataRow.CreateCell(0);
                cell.SetCellValue(model.danhao);
                cell = dataRow.CreateCell(1);
                cell.SetCellValue(model.yewuyuan);
                cell = dataRow.CreateCell(2);
                cell.SetCellValue(model.kehu);
                cell = dataRow.CreateCell(3);
                cell.SetCellValue(model.xiadanRiqi.ToString("yyyy-MM-dd"));
                cell = dataRow.CreateCell(4);
                cell.SetCellValue(model.fahuoRiqi.ToString("yyyy-MM-dd"));
                cell = dataRow.CreateCell(5);
                cell.SetCellValue(model.jiekuanRiqi.ToString("yyyy-MM-dd"));
                cell = dataRow.CreateCell(6);
                cell.SetCellValue(Math.Round(model.yingshoukuanJine, 2));
                cell = dataRow.CreateCell(7);
                cell.SetCellValue(Math.Round(model.yishoukuanJine, 2));
                cell = dataRow.CreateCell(8);
                cell.SetCellValue(Math.Round(model.weishoukuanJine, 2));
                cell = dataRow.CreateCell(9);
                cell.SetCellValue(Math.Round(model.ticheng, 2));
                cell = dataRow.CreateCell(10);
                cell.SetCellValue(model.shouhuoDizhi);
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

        public ActionResult ShengchengChuhuodan(string dingdanId)
        {
            string path = Server.MapPath("~/锐涂出货申请单.xls");
            FileStream stream = System.IO.File.OpenRead(path);
            HSSFWorkbook workbook = new HSSFWorkbook(stream);
            HSSFSheet sheet = workbook.GetSheetAt(0);

            DingdanDetailsModel dingdan = WebHelper.DingdanService.GetDingdan(dingdanId);
            HSSFRow dataRow = sheet.GetRow(1);
            var cell = dataRow.GetCell(6);
            cell.SetCellValue(DateTime.Today.ToString("yyyy 年"));
            cell = dataRow.GetCell(7);
            cell.SetCellValue(DateTime.Today.ToString("MM 月 dd 日"));

            dataRow = sheet.GetRow(2);
            cell = dataRow.GetCell(1);
            cell.SetCellValue(dingdan.kehu.name);
            cell = dataRow.GetCell(5);
            cell.SetCellValue(dingdan.shouhuoDizhi);

            dataRow = sheet.GetRow(3);
            cell = dataRow.GetCell(1);
            cell.SetCellValue(dingdan.shouhuoren);
            cell = dataRow.GetCell(3);
            cell.SetCellValue(dingdan.shouhuorenDianhua);
            cell = dataRow.GetCell(7);
            cell.SetCellValue(dingdan.fahuoRiqi.ToString("MM 月 dd 日"));
            int dataRowIndex = 6;
            foreach (DingdanMingxiDetailsModel mingxiModel in dingdan.mingxiList)
            {
                dataRow = sheet.GetRow(dataRowIndex);
                cell = dataRow.GetCell(0);
                cell.SetCellValue("");
                cell = dataRow.GetCell(1);
                cell.SetCellValue(mingxiModel.chanpin.name);
                cell = dataRow.GetCell(2);
                cell.SetCellValue(mingxiModel.guige);
                cell = dataRow.GetCell(3);
                cell.SetCellValue(mingxiModel.danwei);
                cell = dataRow.GetCell(4);
                cell.SetCellValue(mingxiModel.shuliang);
                cell = dataRow.GetCell(5);
                cell.SetCellValue(mingxiModel.beizhu);
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

            return this.File(tempPath, "application/x-xls", dingdan.danhao + "出货申请单.xls");
        } 
    }
}
