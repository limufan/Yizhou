using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Yizhou.Website.Controllers
{
    public class ScriptController : Controller
    {
        private static string[] caiwuUserArray = new string[] { "heyuchi", "admin" };

        //
        // GET: /Script/

        public ActionResult Index()
        {
            bool hasCaiwuQuanxian = caiwuUserArray.Contains(WebHelper.CurrentUserAccount);
            string hasCaiwuQuanxianScript = string.Format("$.hasCaiwuQuanxian = {0};", hasCaiwuQuanxian.ToString().ToLower());

            return this.JavaScript(hasCaiwuQuanxianScript);
        }
    }
}
