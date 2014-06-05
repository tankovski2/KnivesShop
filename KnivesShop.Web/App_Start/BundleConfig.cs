using System.Web;
using System.Web.Optimization;

namespace KnivesShop.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/js/lib/pace")
                .Include("~/js/lib/pace.min.js"));

             bundles.Add(new ScriptBundle("~/js/lib/jquery")
                .Include("~/js/jquery-1.10.2.min.js",
                        "~/Scripts/jquery.unobtrusive-ajax.min.js"));

            bundles.Add(new ScriptBundle("~/js/lib/BootstrapCore")
                .Include(
                            "~/js/lib/nivo-lightbox.min.js",
                            "~/js/bootstrap.min.js",
                            "~/js/lib/jquery.superslides.min.js",
                            "~/js/lib/smoothscroll.js",
                            "~/js/lib/jquery.sudoslider.min.js",
                            "~/js/lib/jquery.mixitup.min.js",
                            "~/js/lib/jquery.backtotop.js",
                            "~/js/lib/jquery.backstretch.min.js",
                            "~/js/lib/jquery.carouFredSel-6.2.1-packed.js",
                            "~/js/lib/jquery.mobileresponsive.js",
                            "~/js/lib/gmap3.min.js",
                            "~/js/main.js"));


            bundles.Add(new StyleBundle("~/css/lib/pace")
                .Include("~/css/lib/pace.css"));

            bundles.Add(new StyleBundle("~/Content/Site")
                .Include("~/Content/Site.css"));

            bundles.Add(new StyleBundle("~/css/lib/bootstrapCore")
                .Include("~/css/bootstrap.min.css",
                           "~/css/lib/font-awesome.css",
                           "~/css/lib/zocial.css",
                           "~/css/lib/nivo-lightbox.css",
                           "~/css/lib/nivo-themes/default/default.css",
                           "~/css/style.css",
                           "~/css/scheme/red.css"));
        }
    }
}
