using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yizhou.Core;
using Yizhou.Data;

namespace Yizhou.Website.Api
{
    public class DingdanService : IDingdanService
    {
        YizhouCoreManager _coreManager;
        YizhouDataManager _dataManager;
        private object _lock = new object();

        public DingdanService(YizhouCoreManager coreManager, YizhouDataManager dataManager)
        {
            this._coreManager = coreManager;
            this._dataManager = dataManager;
        }

        public void Create(DingdanDetailsModel createModel)
        {
            lock (this._lock)
            {
                Dingdan dingdan = this.CreateDingdan(createModel);

                this._dataManager.DingdanDataProvider.Insert(dingdan);
                this._coreManager.DingdanManager.Add(dingdan);
            }
        }

        private Dingdan CreateDingdan(DingdanDetailsModel createModel)
        {
            DingdanCreateInfo createInfo = new DingdanCreateInfo();
            ClassPropertyHelper.ChangeProperty(createInfo, createModel);
            createInfo.Id = Guid.NewGuid().ToString();
            createInfo.Kehu = this._coreManager.KehuManager.GetKehuById(createModel.kehu.id);
            createInfo.Yewuyuan = this._coreManager.OrgManager.UserManager.GetUserByAccount(createModel.yewuyuan.account);
            createInfo.Danhao = this._coreManager.DingdanManager.ShengchengDingdanhao();
            createInfo.CreateTime = DateTime.Now;
            Dingdan dingdan = new Dingdan(createInfo);
            DingdanChangeInfo changeInfo = new DingdanChangeInfo(dingdan);
            if (createModel.mingxiList != null)
            {
                changeInfo.MingxiList = createModel.mingxiList.Select(m => this.CreateDingdanMingxi(dingdan, m)).ToList();
            }
            if (createModel.shoukuanList != null)
            {
                changeInfo.ShoukuanList = createModel.shoukuanList.Select(m => this.CreateShoukuan(dingdan, m)).ToList();
            }
            dingdan.Change(changeInfo);
            return dingdan;
        }

        public void Change(DingdanDetailsModel changeModel)
        {
            Dingdan dingdan = this._coreManager.DingdanManager.GetDingdanById(changeModel.id);
            DingdanChangeInfo changeInfo = new DingdanChangeInfo(dingdan);
            ClassPropertyHelper.ChangeProperty(changeInfo, changeModel);
            changeInfo.Yewuyuan = this._coreManager.OrgManager.UserManager.GetUserByAccount(changeModel.yewuyuan.account);
            changeInfo.Kehu = this._coreManager.KehuManager.GetKehuById(changeModel.kehu.id);
            changeInfo.MingxiList = changeModel.mingxiList.Select(m => this.CreateDingdanMingxi(dingdan, m)).ToList();
            changeInfo.ShoukuanList = changeModel.shoukuanList.Select(m => this.CreateShoukuan(dingdan, m)).ToList();
            //update to clone 
            Dingdan dingdanClone = dingdan.Clone();
            dingdanClone.Change(changeInfo);
            this._dataManager.DingdanDataProvider.Update(dingdanClone);
            //update
            dingdan.Change(changeInfo);
        }

        private DingdanMingxi CreateDingdanMingxi(Dingdan dingdan, DingdanMingxiDetailsModel detailsModel)
        {
            DingdanMingxiCreateInfo mingxiInfo = new DingdanMingxiCreateInfo(); 
            ClassPropertyHelper.ChangeProperty(mingxiInfo, detailsModel);
            mingxiInfo.Dingdan = dingdan;
            mingxiInfo.Chanpin = this._coreManager.ChanpinManager.GetChanpinById(detailsModel.chanpin.id);
            return new DingdanMingxi(mingxiInfo);
        }

        private Shoukuan CreateShoukuan(Dingdan dingdan, ShoukuanDetailsModel detailsModel)
        {
            ShoukuanCreateInfo shoukuanCreateInfo = new ShoukuanCreateInfo();
            shoukuanCreateInfo.Dingdan = dingdan;
            ClassPropertyHelper.ChangeProperty(shoukuanCreateInfo, detailsModel);
            Shoukuan shoukuan = new Shoukuan(shoukuanCreateInfo);
            return shoukuan;
        }

        public void Delete(string dingdanId)
        {
            Dingdan dingdan = this._coreManager.DingdanManager.GetDingdanById(dingdanId);
            this._dataManager.DingdanDataProvider.Delete(dingdan);
            this._coreManager.DingdanManager.Remove(dingdan);
        }

        public DingdanDetailsModel GetDingdan(string dingdanId)
        {
            Dingdan dingdan = this._coreManager.DingdanManager.GetDingdanById(dingdanId);
            return new DingdanDetailsModel(dingdan);
        }

        public List<DingdanGridModel> GetDingdan(DingdanFilterModel model, out int totalCount)
        {
            DingdanFilter filter = new DingdanFilter();
            filter.KeywordRegex = RegexHelper.GetRegex(model.keyword);
            int skipCount = model.start;
            List<Dingdan> dingdanList = this._coreManager.DingdanManager.GetDingdan(filter);
            totalCount = dingdanList.Count;
            return dingdanList.Skip(skipCount).Take(model.size).Select(k => new DingdanGridModel(k)).ToList();
        }

        public DingdanMingxiDetailsModel JisuanMingxi(DingdanMingxiDetailsModel model)
        {
            DingdanMingxi mingxi = this.CreateDingdanMingxi(null, model);
            mingxi.Jisuan();
            return new DingdanMingxiDetailsModel(mingxi);
        }

        public double JisuanTicheng(DingdanDetailsModel dingdanModel, ShoukuanDetailsModel model)
        {
            Dingdan dingdan = this.CreateDingdan(dingdanModel);
            Shoukuan shoukuan = this.CreateShoukuan(dingdan, model);
            shoukuan.Jisuan();
            return shoukuan.Ticheng;
        }

        public DingdanListModel GetDingdan(DingdanFilterModel model)
        {
            DingdanListModel listModel = new DingdanListModel();
            DingdanFilter filter = new DingdanFilter();
            filter.KeywordRegex = RegexHelper.GetRegex(model.keyword);
            int skipCount = model.start;
            List<Dingdan> dingdanList = this._coreManager.DingdanManager.GetDingdan(filter);
            listModel.tichengSum = dingdanList.Sum(d => d.Ticheng);
            listModel.totalCount = dingdanList.Count;
            listModel.weishoukuanJineSum = dingdanList.Sum(d => d.WeishoukuanJine);
            listModel.yingshoukuanJineSum = dingdanList.Sum(d => d.YingshoukuanJine);
            listModel.yishoukuanJineSum = dingdanList.Sum(d => d.YishoukuanJine);
            listModel.dingdanList = dingdanList.Skip(skipCount).Take(model.size).Select(k => new DingdanGridModel(k)).ToList();
            return listModel;
        }

        public DingdanMingxiListModel GetDingdanMingxi(DingdanMingxiFilterModel model)
        {
            DingdanMingxiListModel listModel = new DingdanMingxiListModel();
            DingdanMingxiFilter filter = new DingdanMingxiFilter();
            filter.KeywordRegex = RegexHelper.GetRegex(model.keyword);
            int skipCount = model.start;
            List<DingdanMingxi> dingdanMingxiList = this._coreManager.DingdanManager.GetDingdanMingxi(filter);
            listModel.totalCount = dingdanMingxiList.Count;
            listModel.zongjineSum = dingdanMingxiList.Sum(m => m.Zongjine);
            listModel.yewufeiSum = dingdanMingxiList.Sum(m => m.Yewufei);
            listModel.tichengSum = dingdanMingxiList.Sum(m => m.Ticheng);
            listModel.dingdanMingxiList = dingdanMingxiList.Skip(skipCount).Take(model.size).Select(m => new DingdanMingxiGridModel(m)).ToList();
            return listModel;
        }
    }
}
