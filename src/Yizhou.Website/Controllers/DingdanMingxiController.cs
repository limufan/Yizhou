﻿using System;
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
                DingdanMingxiFilterModel model = JsonConvert.DeserializeObject<DingdanMingxiFilterModel>(Request["argsJson"]);
                model.start = 0;
                model.size = 5000;
                DingdanMingxiListModel listModel = WebHelper.DingdanService.GetDingdanMingxi(model);
                string fileName = this.ShengchengExcel(listModel.dingdanMingxiList);
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
            return this.File(Path.Combine(tempPath, fileName), "application/x-xls", "订单明细.xls");
        }

        private string ShengchengExcel(List<DingdanMingxiGridModel> models)
        {
            string path = Server.MapPath("~/DingdanMingxiTemplate.xls");
            FileStream stream = System.IO.File.OpenRead(path);
            HSSFWorkbook workbook = new HSSFWorkbook(stream);
            HSSFSheet sheet = workbook.GetSheetAt(0);

            int dataRowIndex = 1;
            foreach (DingdanMingxiGridModel model in models)
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
                cell.SetCellValue(model.fahuoRiqi.ToString("yyyy-MM-dd"));
                cell = dataRow.CreateCell(5);
                cell.SetCellValue(model.chanpin.name);
                cell = dataRow.CreateCell(6);
                cell.SetCellValue(model.shuliang);
                cell = dataRow.CreateCell(7);
                cell.SetCellValue(model.xiaoshouDanjia);
                cell = dataRow.CreateCell(8);
                cell.SetCellValue(Math.Round(model.shijiDanjia, 2));
                cell = dataRow.CreateCell(9);
                cell.SetCellValue(model.xiaoshouDijia);
                cell = dataRow.CreateCell(10);
                cell.SetCellValue(Math.Round(model.zongjine, 2));
                cell = dataRow.CreateCell(11);
                cell.SetCellValue(model.yewulv);
                cell = dataRow.CreateCell(12);
                cell.SetCellValue(Math.Round(model.yewufei, 2));
                cell = dataRow.CreateCell(13);
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
    }
}