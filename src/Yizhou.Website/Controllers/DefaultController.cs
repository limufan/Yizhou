﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Yizhou.Website.Controllers
{
    public class DefaultController : Controller
    {
        //
        // GET: /Default/

        public ActionResult Index()
        {
            if (WebHelper.HasDingdanGuanliQuanxian)
            {
                return this.RedirectToAction("Index", "Dingdan");
            }
            else
            {
                return this.RedirectToAction("Index", "DingdanMingxi");
            }
        }

    }
}
