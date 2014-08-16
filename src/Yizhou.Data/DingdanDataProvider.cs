using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Yizhou.Core;
using Yizhou.Data.DataModels;

namespace Yizhou.Data
{
    public class DingdanDataProvider
    {
        YizhouCoreManager _coreManager;
        public DingdanDataProvider(YizhouCoreManager coreManager)
        {
            this._coreManager = coreManager;
        }

        public void Insert(Dingdan dingdan)
        {
            DingdanDataModel model = new DingdanDataModel();
            this.SetModel(model, dingdan);

            NHibernateHelper.CurrentSession.Save(model);
            NHibernateHelper.CurrentSession.Flush();
        }

        public void Update(Dingdan dingdan)
        {
            DingdanDataModel model = NHibernateHelper.CurrentSession.Get<DingdanDataModel>(dingdan.Id);
            this.SetModel(model, dingdan);

            NHibernateHelper.CurrentSession.Update(model);
            NHibernateHelper.CurrentSession.Flush();
        }

        public void Delete(Dingdan dingdan)
        {
            DingdanDataModel model = NHibernateHelper.CurrentSession.Get<DingdanDataModel>(dingdan.Id);
            NHibernateHelper.CurrentSession.Delete(model);
            NHibernateHelper.CurrentSession.Flush();
        }

        private void SetModel(DingdanDataModel model, Dingdan dingdan)
        {
            ClassPropertyHelper.ChangeProperty(model, dingdan);
            model.Yewuyuan = dingdan.Yewuyuan.Account;
            model.KehuId = dingdan.Kehu.Id;
            model.MingxiListJson = JsonConvert.SerializeObject(dingdan.MingxiList.Select(m => this.GetMingxiModel(m)).ToList());
            model.ShoukuanListJson = JsonConvert.SerializeObject(dingdan.ShoukuanList);
        }

        private DingdanMingxiDataModel GetMingxiModel(DingdanMingxi mingxi)
        {
            DingdanMingxiDataModel model = new DingdanMingxiDataModel();
            ClassPropertyHelper.ChangeProperty(model, mingxi);
            return model;
        }

        public void Load()
        {
            List<DingdanDataModel> models = NHibernateHelper.CurrentSession.QueryOver<DingdanDataModel>().List().ToList();
            if (models != null)
            {
                models.ForEach(m =>
                {
                    Dingdan dingdan = new Dingdan();
                    ClassPropertyHelper.ChangeProperty(dingdan, m);
                    dingdan.Yewuyuan = this._coreManager.OrgManager.UserManager.GetUserByAccount(m.Yewuyuan);
                    dingdan.MingxiList = JsonConvertHelper.TryDeserializeObject<List<DingdanMingxi>>(m.MingxiListJson);
                    dingdan.ShoukuanList = JsonConvertHelper.TryDeserializeObject<List<Shoukuan>>(m.ShoukuanListJson);
                    dingdan.Kehu = this._coreManager.KehuManager.GetKehuById(m.KehuId);
                    dingdan.Changed();
                    this._coreManager.DingdanManager.Add(dingdan);
                });
            }
        }
    }
}
