using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Yizhou.Website.Api;

namespace Yizhou.DataImort.ConsoleApplication
{
    public class ChanpinImporter
    {
        IChanpinService _kehuService;
        public ChanpinImporter()
        {
            this._kehuService = (IChanpinService)Activator.GetObject(typeof(IChanpinService),
                this.GenerateUrl("127.0.0.1:6301", "ChanpinService"));
        }

        protected string GenerateUrl(string server, string serviceUri)
        {
            return string.Format("tcp://{0}/{1}", server, serviceUri);
        }

        public void Import()
        {
            MetadataExporter exporter = new MetadataExporter();
            List<JObject> kehuJsonList = exporter.Export("ae44dc68-44f6-4538-9a8a-ec3b450b54c7");
            foreach (JObject jObject in kehuJsonList)
            {
                string json = JsonConvert.SerializeObject(jObject);
                ChanpinDetailsModel model = JsonConvert.DeserializeObject<ChanpinDetailsModel>(json);
                this._kehuService.Create(model);
            }
        }
    }
}
