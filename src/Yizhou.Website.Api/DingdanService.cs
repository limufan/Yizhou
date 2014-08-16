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

        public DingdanService(YizhouCoreManager coreManager, YizhouDataManager dataManager)
        {
            this._coreManager = coreManager;
            this._dataManager = dataManager;
        }

        public void Create(DingdanDetailsModel createModel)
        {
            Dingdan dingdan = new Dingdan();
            this.SetDingdanInfo(dingdan, createModel);
            dingdan.Id = Guid.NewGuid().ToString();

            this._dataManager.DingdanDataProvider.Insert(dingdan);
            this._coreManager.DingdanManager.Add(dingdan);
        }

        public void Change(DingdanDetailsModel changeModel)
        {
            Dingdan dingdan = this._coreManager.DingdanManager.GetDingdanById(changeModel.id);
            Dingdan dingdanClone = dingdan.Clone();
            this.SetDingdanInfo(dingdanClone, changeModel);
            this._dataManager.DingdanDataProvider.Update(dingdanClone);
            this.SetDingdanInfo(dingdan, changeModel);
            dingdan.Changed();
        }

        private void SetDingdanInfo(Dingdan dingdan, DingdanDetailsModel detailsModel)
        {
            ClassPropertyHelper.ChangeProperty(dingdan, detailsModel);
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
    }
}
