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

            bundles.Add(new ScriptBundle("~/bundles/jqueryv1").Include(
                "~/plugins/jquery-1.11.0.min.js",
                "~/plugins/jquery-migrate-1.2.1.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/plugins/bootstrap/js/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/localization/messages_zh.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));           

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                      "~/plugins/jquery-ui/jquery-ui-1.10.3.custom.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/datatables").Include(
                "~/plugins/datatables/media/js/jquery.dataTables.min.js",
                "~/plugins/datatables/extensions/TableTools/js/dataTables.tableTools.min.js",
                "~/plugins/datatables/extensions/Scroller/js/dataTables.scroller.min.js",
                "~/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/noty").Include(
                      "~/plugins/noty/jquery.noty.js",
                      "~/plugins/noty/layouts/top.js",
                      "~/plugins/noty/themes/default.js"));

            bundles.Add(new ScriptBundle("~/bundles/datepicker").Include(
                      "~/plugins/bootstrap-datepicker/js/bootstrap-datepicker.js",
                      "~/plugins/bootstrap-datepicker/js/locales/bootstrap-datepicker.zh-CN.js"));


            // For css
            bundles.Add(new StyleBundle("~/Content/fontawesome").Include(
                      "~/Content/font-awesome.min.css"));

            bundles.Add(new StyleBundle("~/Content/admin").Include(
                     "~/Content/admin.css",
                     "~/Content/admin.custom.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Plugin/bootstrap").Include(
                     "~/plugins/bootstrap/css/bootstrap.min.css"));

            bundles.Add(new StyleBundle("~/Plugin/uniform").Include(
                     "~/plugins/uniform/css/uniform.default.css"));

            bundles.Add(new StyleBundle("~/Plugin/datatables").Include(
                "~/plugins/datatables/extensions/Scroller/css/dataTables.scroller.min.css",
                "~/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.css"));

            bundles.Add(new StyleBundle("~/Plugin/jqueryui").Include(
                     "~/plugins/jquery-ui/jquery-ui.min.css"));

            bundles.Add(new StyleBundle("~/Plugin/datepicker").Include(
                    "~/plugins/bootstrap-datepicker/css/datepicker3.css"));

            // 将 EnableOptimizations 设为 false 以进行调试。有关详细信息，
            // 请访问 http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = false;
        }
    }
}
