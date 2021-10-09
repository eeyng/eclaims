using System.Web;
using System.Web.Optimization;

namespace eClaim.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));



            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                 "~/Scripts/jquery-3.4.1.js",
                                   "~/Scripts/angular.js",
                         "~/Scripts/jquery.validate*",
                           "~/Scripts/bootstrap.min.js",
                              "~/Scripts/angular-spinner.js",
                       "~/Scripts/spin.js",
                            "~/Scripts/app/appmodule.js",
                         "~/Scripts/app/controllers/login.js",
                           "~/Scripts/app/services/loginprocess.js",
                               "~/Scripts/app/controllers/layout.js",
                                           "~/Scripts/app/controllers/claim.js",
            "~/Scripts/app/services/claimprocess.js",
            "~/Scripts/app/controllers/claimlisting.js",
            "~/Scripts/app/services/claimlistingProcess.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/login.css",
                      "~/Content/site.css"));
        }
    }
}
