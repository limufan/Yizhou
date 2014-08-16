using System;
using System.Collections.Generic;
using System.Web;
using Newtonsoft.Json.Linq;

namespace Yizhou.Website.Models
{
    public class ControllerResultModel : Dictionary<string, object>
    {
        public ControllerResultModel()
        {
            this.result = true;
        }

        public bool result
        {
            set
            {
                this["result"] = value;
            }
        }

        public string message
        {
            set
            {
                this["message"] = value;
            }
        }
    }
}