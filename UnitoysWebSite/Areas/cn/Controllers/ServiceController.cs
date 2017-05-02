using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UnitoysWebSite.Models;

namespace UnitoysWebSite.Areas.cn.Controllers
{
    public class ServiceController : Controller
    {
        //
        // GET: /cn/Service/
        public ActionResult Index()
        {
            return ReturnListIndexView("Index");
        }
        public ActionResult Channel()
        {
            return ReturnListIndexView("Channel");
        }
        public ActionResult Question()
        {
            return ReturnListIndexView("Question");
        }
        private ActionResult ReturnListIndexView(string type)
        {
            ViewBag.Type = type;
            List<UT_GlobalContent> list = new List<UT_GlobalContent>(){
                GlobalContentService.GetHtmlDecodeContentFirstOrDefault(GlobalContentType.AfterSalesPolicy),
                GlobalContentService.GetHtmlDecodeContentFirstOrDefault(GlobalContentType.Aftermarket),
                GlobalContentService.GetHtmlDecodeContentFirstOrDefault(GlobalContentType.CommonProblem)
            };
            return View("Index",list);
        }


    }
}