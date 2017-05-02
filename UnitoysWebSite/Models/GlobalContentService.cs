using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace UnitoysWebSite.Models
{
    public class GlobalContentService
    {
        public static UT_GlobalContent GetHtmlDecodeContentFirstOrDefault(GlobalContentType globalContentType)
        {
            using (UnitoysEntities db = new UnitoysEntities())
            {
                var model = db.UT_GlobalContent.FirstOrDefault(x => x.GlobalContentType == (int)globalContentType);
                if (model != null)
                {
                    model.Content = WebUtility.HtmlDecode(model.Content);
                }
                else if (model == null)
                {
                    model = new UT_GlobalContent()
                    {
                        GlobalContentType=(int)globalContentType,
                        Content = "内容更新中..."
                    };
                }
                return model;
            }
        }
    }
}