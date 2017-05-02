using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UnitoysWebSite.Models;

namespace UnitoysWebSite.Areas.cn.Controllers
{
    public class AboutController : Controller
    {
        //
        // GET: /cn/About/
        public ActionResult Index()
        {
            return ReturnListIndexView("Index");
        }
        /// <summary>
        /// 企业文化
        /// </summary>
        /// <returns></returns>
        public ActionResult Culture()
        {
            return ReturnListIndexView("Culture");
        }

        public ActionResult Join()
        {
            return ReturnListIndexView("Join");
        }
        public ActionResult Business()
        {
            return ReturnListIndexView("Business");
        }

        private ActionResult ReturnListIndexView(string type)
        {
            ViewBag.Type = type;
            List<UT_GlobalContent> list = new List<UT_GlobalContent>(){
                GlobalContentService.GetHtmlDecodeContentFirstOrDefault(GlobalContentType.CompanyInfo),
                GlobalContentService.GetHtmlDecodeContentFirstOrDefault(GlobalContentType.CompanyCulture),
                GlobalContentService.GetHtmlDecodeContentFirstOrDefault(GlobalContentType.Recruitment),
                GlobalContentService.GetHtmlDecodeContentFirstOrDefault(GlobalContentType.BusinessCooperation)
            };
            return View("Index", list);
        }
    }
}