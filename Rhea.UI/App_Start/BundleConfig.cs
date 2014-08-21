﻿using System.Web;
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

            bundles.Add(new ScriptBundle("~/bundles/fileinput").Include(
                "~/plugins/bootstrap-fileinput/bootstrap-fileinput.js"));

            bundles.Add(new ScriptBundle("~/bundles/cookie").Include(
                "~/plugins/jquery.cokie.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/blockui").Include(
                "~/plugins/jquery.blockui.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap-hover-dropdown").Include(
                "~/plugins/bootstrap-hover-dropdown/bootstrap-hover-dropdown.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/slimscroll").Include(
                "~/plugins/jquery-slimscroll/jquery.slimscroll.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/uniform").Include(
                "~/plugins/uniform/jquery.uniform.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap-switch").Include(
                "~/plugins/bootstrap-switch/js/bootstrap-switch.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/fileupload").Include(
                "~/plugins/jquery-file-upload/js/vendor/load-image.min.js",
                "~/plugins/jquery-file-upload/js/vendor/canvas-to-blob.min.js",
                "~/plugins/jquery-file-upload/js/jquery.iframe-transport.js",
                "~/plugins/jquery-file-upload/js/jquery.fileupload.js",
                "~/plugins/jquery-file-upload/js/jquery.fileupload-process.js",
                "~/plugins/jquery-file-upload/js/jquery.fileupload-image.js",
                "~/plugins/jquery-file-upload/js/jquery.fileupload-validate.js"));

            bundles.Add(new ScriptBundle("~/bundles/select2").Include(
                "~/plugins/select2/select2.min.js"));

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

            bundles.Add(new StyleBundle("~/Plugin/bootstrap-switch").Include(
                "~/plugins/bootstrap-switch/css/bootstrap-switch.min.css"));

            bundles.Add(new StyleBundle("~/Plugin/fileinput").Include(
                "~/plugins/bootstrap-fileinput/bootstrap-fileinput.css"));

            bundles.Add(new StyleBundle("~/Plugin/fileupload").Include(
                "~/plugins/jquery-file-upload/blueimp-gallery/blueimp-gallery.min.css",
                "~/plugins/jquery-file-upload/css/jquery.fileupload.css",
                "~/plugins/jquery-file-upload/css/jquery.fileupload-ui.css"));

            bundles.Add(new StyleBundle("~/Plugin/select2").Include(
                "~/plugins/select2/select2.css"));

            // 将 EnableOptimizations 设为 false 以进行调试。有关详细信息，
            // 请访问 http://go.microsoft.com/fwlink/?LinkId=301862
#if DEBUG
            BundleTable.EnableOptimizations = false;
#else
            BundleTable.EnableOptimizations = false;
#endif
        }
    }
}
