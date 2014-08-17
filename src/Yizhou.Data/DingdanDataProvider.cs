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
            model.ShoukuanListJson = JsonConvert.SerializeObject(dingdan.ShoukuanList.Select(s => new ShoukuanDataModel(s)).ToList());
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
                    DingdanCreateInfo createInfo = new DingdanCreateInfo();
                    ClassPropertyHelper.ChangeProperty(createInfo, m);
                    createInfo.Yewuyuan = this._coreManager.OrgManager.UserManager.GetUserByAccount(m.Yewuyuan);
                    createInfo.Kehu = this._coreManager.KehuManager.GetKehuById(m.KehuId);
                    Dingdan dingdan = new Dingdan(createInfo);
                    DingdanChangeInfo changeInfo = new DingdanChangeInfo(dingdan);
                    changeInfo.MingxiList = this.CreateDingdanMingxiList(dingdan, JsonConvertHelper.TryDeserializeObject<List<DingdanMingxiDataModel>>(m.MingxiListJson));
                    changeInfo.ShoukuanList = this.CreateShoukuanList(dingdan, JsonConvertHelper.TryDeserializeObject<List<ShoukuanDataModel>>(m.ShoukuanListJson));
                    dingdan.Change(changeInfo);
                    this._coreManager.DingdanManager.Add(dingdan);
                });
            }
        }

        private List<DingdanMingxi> CreateDingdanMingxiList(Dingdan dingdan, List<DingdanMingxiDataModel> models)
        {
            List<DingdanMingxi> mingxiList = new List<DingdanMingxi>();
            if (models != null)
            {
                foreach (DingdanMingxiDataModel model in models)
                {
                    DingdanMingxiCreateInfo createInfo = new DingdanMingxiCreateInfo();
                    createInfo.Dingdan = dingdan;
                    ClassPropertyHelper.ChangeProperty(createInfo, model);
                    DingdanMingxi mingxi = new DingdanMingxi(createInfo);
                    mingxiList.Add(mingxi);
                }
            }
            return mingxiList;
        }

        private List<Shoukuan> CreateShoukuanList(Dingdan dingdan, List<ShoukuanDataModel> models)
        {
            List<Shoukuan> mingxiList = new List<Shoukuan>();
            if (models != null)
            {
                foreach (ShoukuanDataModel model in models)
                {
                    Shoukuan shoukuan = new Shoukuan();
                    shoukuan.Dingdan = dingdan;
                    ClassPropertyHelper.ChangeProperty(shoukuan, model);
                    mingxiList.Add(shoukuan);
                }
            }
            return mingxiList;
        }
    }
}
