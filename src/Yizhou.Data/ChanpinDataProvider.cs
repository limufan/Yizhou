using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yizhou.Core;
using Yizhou.Data.DataModels;

namespace Yizhou.Data
{
    public class ChanpinDataProvider
    {
        YizhouCoreManager _coreManager;
        public ChanpinDataProvider(YizhouCoreManager coreManager)
        {
            this._coreManager = coreManager;
        }

        public void Insert(Chanpin chanpin)
        {
            ChanpinDataModel model = new ChanpinDataModel();
            this.SetModel(model, chanpin);

            NHibernateHelper.CurrentSession.Save(model);
            NHibernateHelper.CurrentSession.Flush();
        }

        public void Update(Chanpin chanpin)
        {
            ChanpinDataModel model = NHibernateHelper.CurrentSession.Get<ChanpinDataModel>(chanpin.Id);
            this.SetModel(model, chanpin);

            NHibernateHelper.CurrentSession.Update(model);
            NHibernateHelper.CurrentSession.Flush();
        }

        public void Delete(Chanpin chanpin)
        {
            List<Dingdan> dingdanList = this._coreManager.DingdanManager.GetDingdan(chanpin);
            if (dingdanList.Count > 0)
            {
                throw new Exception(string.Format("无法删除，{0}有{1}个订单", chanpin.Name, dingdanList.Count));
            }
            ChanpinDataModel model = NHibernateHelper.CurrentSession.Get<ChanpinDataModel>(chanpin.Id);
            NHibernateHelper.CurrentSession.Delete(model);
            NHibernateHelper.CurrentSession.Flush();
        }

        private void SetModel(ChanpinDataModel model, Chanpin chanpin)
        {
            ClassPropertyHelper.ChangeProperty(model, chanpin);
        }

        public void Load()
        {
            List<ChanpinDataModel> models = NHibernateHelper.CurrentSession.QueryOver<ChanpinDataModel>().List().ToList();
            if (models != null)
            {
                models.ForEach(m =>
                {
                    Chanpin chanpin = new Chanpin();
                    ClassPropertyHelper.ChangeProperty(chanpin, m);
                    this._coreManager.ChanpinManager.Add(chanpin);
                });
            }
        }
    }
}
