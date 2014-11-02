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

            bundles.Add(new ScriptBundle("~/bundles/fileinput").Include(
                "~/plugins/bootstrap-fileinput/bootstrap-fileinput.js"));

            bundles.Add(new ScriptBundle("~/bundles/fileupload").Include(
                "~/plugins/jquery-file-upload/js/vendor/jquery.ui.widget.js",
                "~/plugins/jquery-file-upload/js/vendor/load-image.min.js",
                "~/plugins/jquery-file-upload/js/vendor/canvas-to-blob.min.js",
                "~/plugins/jquery-file-upload/js/jquery.iframe-transport.js",
                "~/plugins/jquery-file-upload/js/jquery.fileupload.js",
                "~/plugins/jquery-file-upload/js/jquery.fileupload-process.js",
                "~/plugins/jquery-file-upload/js/jquery.fileupload-image.js",
                "~/plugins/jquery-file-upload/js/jquery.fileupload-validate.js"));



            bundles.Add(new ScriptBundle("~/bundles/colorbox").Include(
                "~/plugins/colorbox/jquery.colorbox-min.js"));


            bundles.Add(new ScriptBundle("~/bundles/core").Include(
                "~/plugins/jquery-ui/jquery-ui-1.10.3.custom.min.js",
                "~/plugins/jquery-slimscroll/jquery.slimscroll.min.js",
                "~/Scripts/jquery.unobtrusive*",
                "~/Scripts/jquery.validate*",
                "~/Scripts/localization/messages_zh.js",
                "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/core-plugin").Include(
                "~/plugins/bootstrap-hover-dropdown/bootstrap-hover-dropdown.min.js",
                "~/plugins/jquery-slimscroll/jquery.slimscroll.min.js",
                "~/plugins/jquery.blockui.min.js",
                "~/plugins/jquery.cokie.min.js",
                "~/plugins/moment.min.js",
                "~/plugins/uniform/jquery.uniform.min.js",
                "~/plugins/bootstrap-switch/js/bootstrap-switch.min.js",
                "~/plugins/datatables/media/js/jquery.dataTables.min.js",
                "~/plugins/datatables/extensions/TableTools/js/dataTables.tableTools.min.js",
                "~/plugins/datatables/extensions/Scroller/js/dataTables.scroller.min.js",
                "~/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.js",
                "~/plugins/bootstrap-datepicker/js/bootstrap-datepicker.js",
                "~/plugins/bootstrap-datepicker/js/locales/bootstrap-datepicker.zh-CN.js",
                "~/plugins/noty/jquery.noty.js",
                "~/plugins/noty/layouts/top.js",
                "~/plugins/noty/themes/default.js"));

            bundles.Add(new ScriptBundle("~/bundles/rhea").Include(
                "~/scripts/metronic.js",
                "~/scripts/layout.js",
                "~/scripts/rhea.js"));

            bundles.Add(new ScriptBundle("~/bundles/apartment").Include(
                "~/plugins/select2/select2.min.js",
                "~/plugins/select2/select2_locale_zh-CN.js",
                "~/plugins/jstree/jstree.min.js",
                "~/plugins/colorbox/jquery.colorbox-min.js",
                "~/plugins/bootstrap-wizard/jquery.bootstrap.wizard.min.js",
                "~/plugins/jquery-form/jquery.form.js",
                "~/Scripts/apartment.js"));

            // For css
            bundles.Add(new StyleBundle("~/Content/core").Include(
                     "~/Content/font-awesome.css",
                     "~/Content/bootstrap.css"));

            bundles.Add(new StyleBundle("~/Content/core-style").Include(
                     "~/Content/components.css",
                     "~/Content/plugins.css",
                     "~/Content/layout.css",
                     "~/Content/themes/grey.css",
                     "~/Content/custom.css"));

            bundles.Add(new StyleBundle("~/Content/core-style-admin").Include(
                     "~/Content/components.css",
                     "~/Content/plugins.css",
                     "~/Content/layout.css",
                     "~/Content/themes/default.css",
                     "~/Content/custom.css"));

            bundles.Add(new StyleBundle("~/Plugin/datatables").Include(
                "~/plugins/datatables/extensions/Scroller/css/dataTables.scroller.min.css",
                "~/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.css"));

            bundles.Add(new StyleBundle("~/Plugin/jqueryui").Include(
                     "~/plugins/jquery-ui/jquery-ui.min.css"));

            bundles.Add(new StyleBundle("~/Plugin/datepicker").Include(
                    "~/plugins/bootstrap-datepicker/css/datepicker3.css"));

            bundles.Add(new StyleBundle("~/Plugin/fileinput").Include(
                "~/plugins/bootstrap-fileinput/bootstrap-fileinput.css"));

            bundles.Add(new StyleBundle("~/Plugin/fileupload").Include(
                "~/plugins/jquery-file-upload/blueimp-gallery/blueimp-gallery.min.css",
                "~/plugins/jquery-file-upload/css/jquery.fileupload.css",
                "~/plugins/jquery-file-upload/css/jquery.fileupload-ui.css"));

            bundles.Add(new StyleBundle("~/Plugin/select2").Include(
                "~/plugins/select2/select2.css"));

            bundles.Add(new StyleBundle("~/Plugin/jstree").Include(
                "~/plugins/jstree/themes/default/style.min.css"));

            bundles.Add(new StyleBundle("~/Plugin/colorbox").Include(
                "~/plugins/colorbox/colorbox.css"));

            // 将 EnableOptimizations 设为 false 以进行调试。有关详细信息，
            // 请访问 http://go.microsoft.com/fwlink/?LinkId=301862
#if DEBUG
            BundleTable.EnableOptimizations = false;
#else
            BundleTable.EnableOptimizations = true;
#endif
        }
    }
}
