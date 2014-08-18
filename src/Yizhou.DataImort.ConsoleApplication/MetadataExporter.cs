using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Coldew.Website.Api;
using Coldew.Website.Api.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Yizhou.Website.Api;

namespace Yizhou.DataImort.ConsoleApplication
{
    public class MetadataExporter
    {
        IMetadataService _metadataService;
        IFormService _formService;
        public MetadataExporter()
        {
            this._metadataService = (IMetadataService)Activator.GetObject(typeof(IMetadataService),
                this.GenerateUrl("127.0.0.1:6302", "WebsiteMetadataService"));
            this._formService = (IFormService)Activator.GetObject(typeof(IFormService),
                this.GenerateUrl("127.0.0.1:6302", "WebsiteFormService"));
        }

        protected string GenerateUrl(string server, string serviceUri)
        {
            return string.Format("tcp://{0}/{1}", server, serviceUri);
        }

        public List<JObject> Export(string objectId)
        {
            List<JObject> objectList = new List<JObject>();
            FormWebModel formModel = this._formService.GetForm("admin", objectId, "editForm");
            int totalCount;
            string json = this._metadataService.GetGridJson(objectId, "admin", "", 0, 1000, "", out totalCount);
            List<JObject> list = JsonConvert.DeserializeObject<List<JObject>>(json);
            foreach (JObject jobject in list)
            {
                string formJson = this._metadataService.GetFormJson("admin", objectId, jobject["id"].ToString(), formModel.id);
                objectList.Add(JsonConvert.DeserializeObject<JObject>(formJson));
            }
            return objectList;
        }
    }
}
