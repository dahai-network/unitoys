using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UnitoysWebSite.Controllers
{
    public class downController : Controller
    {
        //
        // GET: /Down/
        public ActionResult Index()
        {
            return Redirect("http://a.app.qq.com/o/simple.jsp?pkgname=cn.com.aixiaoqi");//2
        }
	}
}