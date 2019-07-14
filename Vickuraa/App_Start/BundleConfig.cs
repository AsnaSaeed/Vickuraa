using System.Web;
using System.Web.Optimization;

namespace Vickuraa
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-3.3.1.js",
                         "~/Scripts/pixeladmin.min.js",
                         "~/Scripts/pace.min.js",
                         "~/Scripts/demo.js"
                        ));

       

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));


            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js",
                       "~/Scripts/app.js",
                        "~/Scripts/pace.min.js",
                         "~/Scripts/pixeladmin.min.js"

                      ));

            bundles.Add(new ScriptBundle("~/bundles/font").Include(
                    "~/Content/fonts/ionicons.eot",
                     "~/Content/fonts/ionicons.svg",
                      "~/Content/fonts/ionicons.ttf"
                    ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/themes/adminflare.min.css",
                     "~/Content/pixeladmin.min.css",
                     "~/Content/widgets.min.min.css",
                       "~/Content/widgets.min.min.css",
                        "~/Content/themes/clean.min.min.css"
                  
                      ));
        }
    }
}
