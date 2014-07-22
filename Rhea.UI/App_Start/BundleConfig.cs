using System.Web;
using System.Web.Optimization;

namespace Rhea.UI
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            // For javascript
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                      "~/plugins/jquery-ui/jquery-ui.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/datatables").Include(
                      "~/plugins/datatables/jquery.dataTables.js",
                      "~/plugins/datatables/dataTables.bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/noty").Include(
                      "~/plugins/noty/jquery.noty.js",
                      "~/plugins/noty/layouts/top.js",
                      "~/plugins/noty/themes/default.js"));

            bundles.Add(new ScriptBundle("~/bundles/datepicker").Include(
                      "~/plugins/bootstrap-datepicker/js/bootstrap-datepicker.js",
                      "~/plugins/bootstrap-datepicker/js/locales/bootstrap-datepicker.zh-CN.js"));


            // For css
            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
                      "~/Content/bootstrap.min.css"));

            bundles.Add(new StyleBundle("~/Content/fontawesome").Include(
                      "~/Content/font-awesome.min.css"));

            bundles.Add(new StyleBundle("~/Content/admin").Include(
                     "~/Content/admin.css",
                     "~/Content/admin.custom.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Plugin/jqueryui").Include(
                     "~/plugins/jquery-ui/jquery-ui.min.css"));

            bundles.Add(new StyleBundle("~/Plugin/datepicker").Include(
                    "~/plugins/bootstrap-datepicker/css/datepicker3.css"));

            // 将 EnableOptimizations 设为 false 以进行调试。有关详细信息，
            // 请访问 http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}
