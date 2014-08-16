using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Yizhou.Core
{
    public class JsonConvertHelper
    {
        public static T TryDeserializeObject<T>(string value)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(value);
            }
            catch
            {

            }
            return default(T);
        }
    }
}
