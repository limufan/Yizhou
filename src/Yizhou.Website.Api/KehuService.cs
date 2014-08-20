using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yizhou.Core;
using Yizhou.Data;

namespace Yizhou.Website.Api
{
    public class KehuService : IKehuService
    {
        YizhouCoreManager _coreManager;
        YizhouDataManager _dataManager;

        public KehuService(YizhouCoreManager coreManager, YizhouDataManager dataManager)
        {
            this._coreManager = coreManager;
            this._dataManager = dataManager;
        }

        public KehuDetailsModel Create(KehuDetailsModel createModel)
        {
            Kehu kehu = new Kehu();
            kehu.Id = Guid.NewGuid().ToString();
            this.SetKehuInfo(kehu, createModel);

            this._dataManager.KehuDataProvider.Insert(kehu);
            this._coreManager.KehuManager.Add(kehu);
            return new KehuDetailsModel(kehu);
        }

        public void Change(KehuDetailsModel changeModel)
        {
            Kehu kehu = this._coreManager.KehuManager.GetKehuById(changeModel.id);
            Kehu kehuClone = kehu.Clone();
            this.SetKehuInfo(kehuClone, changeModel);
            this._dataManager.KehuDataProvider.Update(kehuClone);
            this.SetKehuInfo(kehu, changeModel);
            kehu.Changed();
        }

        private void SetKehuInfo(Kehu kehu, KehuDetailsModel detailsModel)
        {
            kehu.Beizhu = detailsModel.beizhu;
            kehu.Chuanzhen = detailsModel.chuanzhen;
            kehu.CreateTime = DateTime.Now;
            kehu.Email = detailsModel.email;
            kehu.GongsiDizhi = detailsModel.gongsiDizhi;
            kehu.JiekuanFangshi = detailsModel.jiekuanFangshi;
            kehu.Lianxidianhua = detailsModel.lianxidianhua;
            kehu.Name = detailsModel.name;
            kehu.ShouhuoDizhi = detailsModel.shouhuoDizhi;
            kehu.Shouhuoren = detailsModel.shouhuoren;
            kehu.ShouhuorenDianhua = detailsModel.shouhuorenDianhua;
            kehu.Yewulv = detailsModel.yewulv;
            kehu.YewulvFangshi = detailsModel.yewulvFangshi;
            kehu.Yewuyuan = this._coreManager.OrgManager.UserManager.GetUserByAccount(detailsModel.yewuyuan.account);
        }

        public void Delete(string kehuId)
        {
            Kehu kehu = this._coreManager.KehuManager.GetKehuById(kehuId);
            this._dataManager.KehuDataProvider.Delete(kehu);
            this._coreManager.KehuManager.Remove(kehu);
        }

        public KehuDetailsModel GetKehu(string kehuId)
        {
            Kehu kehu = this._coreManager.KehuManager.GetKehuById(kehuId);
            return new KehuDetailsModel(kehu);
        }

        public List<KehuGridModel> GetKehu(KehuFilterModel model, out int totalCount)
        {
            KehuFilter filter = new KehuFilter();
            filter.KeywordRegex = RegexHelper.GetRegexList(model.keyword);
            int skipCount = model.start;
            List<Kehu> kehuList = this._coreManager.KehuManager.GetKehu(filter);
            totalCount = kehuList.Count;
            return kehuList.Skip(skipCount).Take(model.size).Select(k => new KehuGridModel(k)).ToList();
        }
    }
}
