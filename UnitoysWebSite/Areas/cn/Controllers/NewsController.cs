using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using UnitoysWebSite.Models;

namespace UnitoysWebSite.Areas.cn.Controllers
{
    public class NewsController : Controller
    {
        //
        // GET: /cn/News/
        public ActionResult Index(string type)
        {
            ViewBag.Type = type;
            using (UnitoysEntities db = new UnitoysEntities())
            {
                List<UT_News> list = new List<UT_News>();
                if (type == "industry")
                {
                    list = db.UT_News.Where(x => x.NewsType == 0).OrderByDescending(x => x.NewsDate).ToList();
                }
                else
                {
                    list = db.UT_News.Where(x => x.NewsType == 1).OrderByDescending(x => x.NewsDate).ToList();
                }
                list.ForEach(x => x.Image = Unitoys.Core.ImageUtil.GetCompleteUrl(x.Image));
                return View(list);
            }
        }

        public async Task<ActionResult> Detail(Guid? id)
        {
            if (!id.HasValue)
            {
                return Redirect("Index");
            }
            using (UnitoysEntities db = new UnitoysEntities())
            {
                var model = await db.UT_News.FindAsync(id);
                if (model == null)
                {
                    return Redirect("Index");
                }

                model.Content = Server.HtmlDecode(model.Content);

                //TODO 根据时间来获取上一篇下一篇，会存在选择相同时间时，顺序出现问题的情况
                //上一篇
                var prevModel = db.UT_News.Where(x => x.NewsType == model.NewsType && x.NewsDate <= model.NewsDate && x.ID != model.ID).OrderByDescending(x => x.NewsDate).FirstOrDefault();
                //下一篇
                Guid? prevId = prevModel == null ? (Guid?)null : prevModel.ID;
                var nextModel = db.UT_News.Where(x => x.NewsType == model.NewsType && x.NewsDate >= model.NewsDate && x.ID != model.ID && (x.ID != prevId)).OrderBy(x => x.NewsDate).FirstOrDefault(x => x.ID != model.ID);

                ViewBag.PrevModel = prevModel;
                ViewBag.NextModel = nextModel;
                return View(model);
            }
        }
    }
}