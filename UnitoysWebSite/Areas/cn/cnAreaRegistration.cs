using System.Web.Mvc;

namespace UnitoysWebSite.Areas.cn
{
    public class cnAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "cn";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "cn_default",
                "cn/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}