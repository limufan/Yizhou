using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yizhou.Core;
using Yizhou.Data.DataModels;

namespace Yizhou.Data
{
    public class KehuDataProvider
    {
        YizhouCoreManager _coreManager;
        public KehuDataProvider(YizhouCoreManager coreManager)
        {
            this._coreManager = coreManager;
        }

        public void Insert(Kehu kehu)
        {
            KehuDataModel model = new KehuDataModel();
            this.SetModel(model, kehu);

            NHibernateHelper.CurrentSession.Save(model);
            NHibernateHelper.CurrentSession.Flush();
        }

        public void Update(Kehu kehu)
        {
            KehuDataModel model = NHibernateHelper.CurrentSession.Get<KehuDataModel>(kehu.Id);
            this.SetModel(model, kehu);

            NHibernateHelper.CurrentSession.Update(model);
            NHibernateHelper.CurrentSession.Flush();
        }

        public void Delete(Kehu kehu)
        {
            KehuDataModel model = NHibernateHelper.CurrentSession.Get<KehuDataModel>(kehu.Id);
            NHibernateHelper.CurrentSession.Delete(model);
            NHibernateHelper.CurrentSession.Flush();
        }

        private void SetModel(KehuDataModel model, Kehu kehu)
        {
            ClassPropertyHelper.ChangeProperty(model, kehu);
            model.Yewuyuan = kehu.Yewuyuan.Account;
        }

        public void Load()
        {
            List<KehuDataModel> models = NHibernateHelper.CurrentSession.QueryOver<KehuDataModel>().List().ToList();
            if (models != null)
            {
                models.ForEach(m =>
                {
                    Kehu kehu = new Kehu();
                    ClassPropertyHelper.ChangeProperty(kehu, m);
                    kehu.Yewuyuan = this._coreManager.OrgManager.UserManager.GetUserByAccount(m.Yewuyuan);
                    this._coreManager.KehuManager.Add(kehu);
                });
            }
        }
    }
}
