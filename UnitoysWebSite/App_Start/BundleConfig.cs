using System.Web;
using System.Web.Optimization;

namespace UnitoysWebSite
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Content/js/get").Include(
                      "~/Content/js/jquery.min.js",
                      "~/Content/js/wow.min.js",
                      "~/Content/js/jquery.mousewheel.js",
                      "~/Content/js/jquery.jscrollpane.min.js",
                      "~/Content/js/owl.carousel.min.js",
                      "~/Content/js/jquery.parallax-1.1.3.js",
                      "~/Content/js/jquery.localscroll-1.2.7-min.js",
                      "~/Content/js/skrollr.js",
                      "~/Content/js/common.js"));

            bundles.Add(new ScriptBundle("~/Content/js/common").Include(
                     "~/Content/js/jquery.min.js",
                     "~/Content/js/common.js"));

            bundles.Add(new StyleBundle("~/Content/css/get").Include(
                      "~/Content/css/jquery.jscrollpane.css",
                      "~/Content/css/animate.css",
                      "~/Content/css/owl.carousel.css",
                      "~/Content/css/style.css"));
            bundles.Add(new StyleBundle("~/Content/en/css/get").Include(
                      "~/Content/en/css/jquery.jscrollpane.css",
                      "~/Content/en/css/animate.css",
                      "~/Content/en/css/owl.carousel.css",
                      "~/Content/en/css/style.css"));
        }
    }
}
