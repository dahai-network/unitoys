
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using UnitoysWebSite.Core;

namespace UnitoysWebSite.Areas.cn.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            var language = WebHelper.GetCookie("language");
            if (language == "")
            {
                CultureInfo info = Thread.CurrentThread.CurrentCulture;
                if (!info.Name.Equals("zh-CN"))
                {
                    WebHelper.SetCookie("language", "en");
                    return Redirect("/en");
                }
                WebHelper.SetCookie("language", "cn");
            }
            return View();
            
        }
       
    }
   
}