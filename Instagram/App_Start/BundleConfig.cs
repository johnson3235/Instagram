using System.Web;
using System.Web.Optimization;

namespace Instagram
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/font.js",
                "~/Scripts/jquery.js",
                "~/Scripts/jquery-migrate.js",
                "~/Scripts/popper.js",
                "~/Scripts/bootstrap/bootstrap.js",
                "~/Scripts/o2system-ui.js",
                      "~/Scripts/owl-carousel.js",
                      "~/Scripts/cloudzoom.js",
                      "~/Scripts/thumbelina.js",
                      "~/Scripts//bootstrap-touchspin.js",
                      "~/Scripts/theme.js",
                      "~/Scripts/all.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/all.css",
                      "~/Content/bootstrap-icons.css",
                      "~/Content/fontawesome.css",
                      "~/Content/bbot.css",
                      "~/Content/o2system-ui.css",
                      "~/Content/owl-carousel.css",
                      "~/Content/cloudzoom.css",
                      "~/Content/thumbelina.css",
                      "~/Content/bootstrap-touchspin.css",
                      "~/Content/theme.css",
                      "~/Content/Profile.css"));
        }
    }
}
