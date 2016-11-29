﻿using System.Web.Optimization;

namespace POS.Portal
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-{version}.js", "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include("~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include("~/Scripts/angular.js", "~/Scripts/angular-cookies.js", "~/Scripts/angular-animate.js",
                                         "~/Scripts/angular-route.js", "~/Scripts/angular-touch.js"));

            bundles.Add(new ScriptBundle("~/bundles/require").Include("~/Scripts/require.js", "~/App/Main.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Scripts/bootstrap.js", "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/ui-bootstrap").Include("~/Scripts/angular-ui/ui-bootstrap-tpls.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/bootstrap.css", "~/Content/site.css", "~/Content/multi-select.css"));

            bundles.Add(new StyleBundle("~/Content/bootstrap-rtl").Include("~/Content/bootstrap-rtl.css", "~/Content/site-rtl.css"));
        }
    }
}
