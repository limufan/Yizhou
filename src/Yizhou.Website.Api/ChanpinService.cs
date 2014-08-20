using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yizhou.Core;
using Yizhou.Data;

namespace Yizhou.Website.Api
{
    public class ChanpinService : IChanpinService
    {
        YizhouCoreManager _coreManager;
        YizhouDataManager _dataManager;

        public ChanpinService(YizhouCoreManager coreManager, YizhouDataManager dataManager)
        {
            this._coreManager = coreManager;
            this._dataManager = dataManager;
        }

        public ChanpinDetailsModel Create(ChanpinDetailsModel createModel)
        {
            Chanpin chanpin = new Chanpin();
            this.SetChanpinInfo(chanpin, createModel);
            chanpin.Id = Guid.NewGuid().ToString();

            this._dataManager.ChanpinDataProvider.Insert(chanpin);
            this._coreManager.ChanpinManager.Add(chanpin);
            return new ChanpinDetailsModel(chanpin);
        }

        public void Change(ChanpinDetailsModel changeModel)
        {
            Chanpin chanpin = this._coreManager.ChanpinManager.GetChanpinById(changeModel.id);
            Chanpin chanpinClone = chanpin.Clone();
            this.SetChanpinInfo(chanpinClone, changeModel);
            this._dataManager.ChanpinDataProvider.Update(chanpinClone);
            this.SetChanpinInfo(chanpin, changeModel);
            chanpin.Changed();
        }

        private void SetChanpinInfo(Chanpin chanpin, ChanpinDetailsModel detailsModel)
        {
            ClassPropertyHelper.ChangeProperty(chanpin, detailsModel);
        }

        public void Delete(string chanpinId)
        {
            Chanpin chanpin = this._coreManager.ChanpinManager.GetChanpinById(chanpinId);
            this._dataManager.ChanpinDataProvider.Delete(chanpin);
            this._coreManager.ChanpinManager.Remove(chanpin);
        }

        public ChanpinDetailsModel GetChanpin(string chanpinId)
        {
            Chanpin chanpin = this._coreManager.ChanpinManager.GetChanpinById(chanpinId);
            return new ChanpinDetailsModel(chanpin);
        }

        public List<ChanpinGridModel> GetChanpin(ChanpinFilterModel model, out int totalCount)
        {
            ChanpinFilter filter = new ChanpinFilter();
            filter.KeywordRegex = RegexHelper.GetRegexList(model.keyword);
            int skipCount = model.start;
            List<Chanpin> chanpinList = this._coreManager.ChanpinManager.GetChanpin(filter);
            totalCount = chanpinList.Count;
            return chanpinList.Skip(skipCount).Take(model.size).Select(k => new ChanpinGridModel(k)).ToList();
        }
    }
}
