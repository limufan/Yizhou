using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Yizhou.Website.Api;

namespace Yizhou.DataImort.ConsoleApplication
{
    public class DingdanImporter
    {
        IKehuService _kehuService;
        IChanpinService _chanpinService;
        IDingdanService _dingdanService;
        public DingdanImporter()
        {
            this._dingdanService = (IDingdanService)Activator.GetObject(typeof(IDingdanService),
                this.GenerateUrl("127.0.0.1:6301", "DingdanService"));
            this._kehuService = (IKehuService)Activator.GetObject(typeof(IKehuService),
                this.GenerateUrl("127.0.0.1:6301", "KehuService"));
            this._chanpinService = (IChanpinService)Activator.GetObject(typeof(IChanpinService),
                this.GenerateUrl("127.0.0.1:6301", "ChanpinService"));
        }

        protected string GenerateUrl(string server, string serviceUri)
        {
            return string.Format("tcp://{0}/{1}", server, serviceUri);
        }

        public void Import()
        {
            int totalCount;
            List<KehuGridModel> kehuGridModels = this._kehuService.GetKehu(new KehuFilterModel{ keyword = "", size = 100, start = 0 }, out totalCount);
            List<ChanpinGridModel> chanpinGridModels = this._chanpinService.GetChanpin(new ChanpinFilterModel{ keyword = "", size = 100, start = 0 }, out totalCount);

            MetadataExporter exporter = new MetadataExporter();
            List<JObject> kehuJsonList = exporter.Export("71d07482-0b90-408a-a6f0-cbb4847ba533");
            List<DingdanDetailsModel> models = new List<DingdanDetailsModel>();
            foreach (JObject jObject in kehuJsonList)
            {
                string kehuName = jObject["kehu"].ToString();
                KehuGridModel kehuGridModel = kehuGridModels.Find(k => k.name == kehuName);
                if (kehuGridModel == null)
                {
                    kehuGridModel = this.CreateKehu(kehuName);
                }
                JObject kehuJObject = new JObject();
                kehuJObject.Add("id", kehuGridModel.id);
                kehuJObject.Add("name", kehuGridModel.name);
                jObject["kehu"] = kehuJObject;
                jObject["xiadanRiqi"] = jObject["fahuoRiqi"];
                string json = JsonConvert.SerializeObject(jObject);
                DingdanDetailsModel model = JsonConvert.DeserializeObject<DingdanDetailsModel>(json);
                foreach (JObject chanpin in jObject["chanpinGrid"])
                {
                    JObject chanpinObject = new JObject();
                    ChanpinGridModel chanpinModel = chanpinGridModels.Find(c => c.name == chanpin["name"].ToString());
                    if (chanpinModel == null)
                    {
                        chanpinModel = this.CreateChanpin(chanpin["name"].ToString());
                    }
                    chanpinObject.Add("id", chanpinModel.id);
                    chanpinObject.Add("name", chanpinModel.name);
                    chanpin["chanpin"] = chanpinObject;
                    chanpin["shifouKaipiao"] = chanpin["shifouKaipiao"].ToString() == "是";
                }
                model.mingxiList = JsonConvert.DeserializeObject<List<DingdanMingxiDetailsModel>>(JsonConvert.SerializeObject(jObject["chanpinGrid"]));
                models.Add(model);
            }
            models = models.OrderBy(m => m.xiadanRiqi).ToList();
            foreach (DingdanDetailsModel model in models)
            {
                Thread.Sleep(1000);
                this._dingdanService.Create(model);
            }
        }

        private KehuGridModel CreateKehu(string kehuName)
        {
            KehuDetailsModel model = new KehuDetailsModel{ name = kehuName, jiekuanFangshi ="1个月月结", yewuyuan = new UserInputModel{ account = "admin" } };
            model = this._kehuService.Create(model);
            
            return new KehuGridModel { id = model.id, name = model.name };
        }

        private ChanpinGridModel CreateChanpin(string name)
        {
            ChanpinDetailsModel model = new ChanpinDetailsModel { name = name };
            model = this._chanpinService.Create(model);

            return new ChanpinGridModel { id = model.id, name = model.name };
        }
    }
}
