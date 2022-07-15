﻿using System.Web;
using System.Web.Optimization;

namespace Laptop
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
            bundles.Add(new StyleBundle("~/Content/mycss").Include(
                      "~/Content/css/bootstrap.min.css",
                      "~/Content/css/font-awesome.min.css",
                      "~/Content/css/linear-icon.css",
                      "~/Content/css/plugins.css",
                      "~/Content/css/default.css",
                      "~/Content/css/style.css",
                      "~/Content/css/responsive.css"));
            bundles.Add(new ScriptBundle("~/bundles/myjs").Include(
                       "~/Scripts/js/vendor/jquery-1.12.4.min.js",
                       "~/Scripts/js/popper.min.js",
                       "~/Scripts/js/bootstrap.min.js",
                       "~/Scripts/js/plugins.js",
                       "~/Scripts/js/ajax-mail.js",
                       "~/Scripts/js/main.js"));
        }
    }
}
