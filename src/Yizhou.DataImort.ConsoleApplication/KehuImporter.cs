using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Yizhou.Website.Api;

namespace Yizhou.DataImort.ConsoleApplication
{
    public class KehuImporter
    {
        IKehuService _kehuService;
        public KehuImporter()
        {
            this._kehuService = (IKehuService)Activator.GetObject(typeof(IKehuService),
                this.GenerateUrl("127.0.0.1:6301", "KehuService"));
        }

        protected string GenerateUrl(string server, string serviceUri)
        {
            return string.Format("tcp://{0}/{1}", server, serviceUri);
        }

        public void Import()
        {
            MetadataExporter exporter = new MetadataExporter();
            List<JObject> kehuJsonList = exporter.Export("358a9b3b-6351-440f-aae3-1baf12e01d70");
            foreach (JObject jObject in kehuJsonList)
            {
                jObject["yewuyuan"] = (jObject["yewuyuan"] as JArray)[0];
                string json = JsonConvert.SerializeObject(jObject);
                KehuDetailsModel model = JsonConvert.DeserializeObject<KehuDetailsModel>(json);
                this._kehuService.Create(model);
            }
        }
    }
}
