using System.Web;
using System.Web.Optimization;

namespace CWI.ContraCheque.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            // DATEPICKER - SCRIPTS 
            bundles.Add(new ScriptBundle("~/bundles/DatePicker").Include("~/Scripts/DatePicker/bootstrap-datepicker.js"));

            // DATEPICKER - BOOTSTRAP
            bundles.Add(new StyleBundle("~/Content/DatePicker").Include(
                      "~/Content/DatePicker/bootstrap-datepicker.css",
                      "~/Content/DatePicker/bootstrap-datepicker.standalone.css",
                      "~/Content/DatePicker/bootstrap-datepicker3.css",
                      "~/Content/DatePicker/bootstrap-datepicker3.standalone.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/Login.css",
                      "~/Content/Colaborador.css"));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}