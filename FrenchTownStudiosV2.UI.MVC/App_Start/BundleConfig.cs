using System.Web.Optimization;

namespace FrenchTownStudiosV2.UI.MVC
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-2.8.3.min.js*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/respond.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Scripts/lib/bootstrap/css/bootstrap.min.css",
                      "~/Content/site.css",
                      "~/Content/img/favicon.png",
                      "~/Content/img/apple-touch-icon.png",
                      "~/Scripts/lib/bootstrap/css/bootstrap.min.css",
                      "~/Scripts/lib/font-awesome/css/font-awesome.min.css",
                      "~/Scripts/lib/ionicons/css/ionicons.min.css",
                      "~/Scripts/lib/photostack/photostack.min.css",
                      "~/Scripts/lib/fullpage-menu/fullpage-menu.min.css",
                      "~/Scripts/lib/cubeportfolio/cubeportfolio.min.css",
                      "~/Scripts/lib/superslides/superslides.min.css",
                      "~/Content/DataTables/css/jquery.dataTables.min.css",
                      "~/Content/css/style.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/template").Include(
                "~/scripts/lib/jquery/jquery.min.js",
                "~/scripts/lib/modernizr/modernizr.min.js",
                "~/scripts/lib/bootstrap/js/bootstrap.min.js",
                "~/scripts/lib/php-mail-form/validate.min.js",
                "~/scripts/lib/easing/easing.min.js",
                "~/scripts/lib/cubeportfolio/cubeportfolio.min.js",
                "~/scripts/lib/classie/classie.min.js",
                "~/scripts/lib/fullpage-menu/fullpage-menu.min.js",
                "~/scripts/lib/superslides/superslides.min.js",
                "~/Scripts/DataTables/jquery.dataTables.min.js",
                "~/scripts/js/main.min.js",
                "~/scripts/lib/photostack/photostack.min.js"));
        }
    }
}
