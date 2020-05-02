using System.Web;
using System.Web.Optimization;

namespace BillingManagementSystem
{
    public class BundleConfig
    {

        public static void RegisterBundles(BundleCollection bundles)
        {

            // CSS style (bootstrap/inspinia)
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/nifty.min.css",
                      "~/Content/themes/type-e/theme-navy.min.css",
                      "~/Content/plugins/animate-css/animate.min.css"));
            // Line Icons
            bundles.Add(new StyleBundle("~/premium-line-icons/css").Include(
                      "~/fonts/line-icons/premium-line-icons.css"));
            // Solid Icons
            bundles.Add(new StyleBundle("~/premium-solid-icons/css").Include(
                      "~/fonts/solid-icons/premium-solid-icons.css"));
            // Font Awesome icons
            bundles.Add(new StyleBundle("~/font-awesome/css").Include(
                      "~/fonts/font-awesome/font-awesome.css", new CssRewriteUrlTransform()));

            // jQuery
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery.min.js",
                        "~/Scripts/jquery.cookie.js",
                        "~/Scripts/moment.min.js",
                        "~/Scripts/plugins/fast-click/fastclick.min.js"));
            
            // Bootstrap
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js"));

            // Select2
            bundles.Add(new ScriptBundle("~/bundles/select2").Include(
                      "~/Scripts/plugins/select2/js/select2.min.js"));
            // Select2
            bundles.Add(new StyleBundle("~/plugins/select2").Include(
                      "~/Content/plugins/select2/css/select2.min.css", new CssRewriteUrlTransform()));

            // DatePicker
            bundles.Add(new ScriptBundle("~/bundles/datepicker").Include(
                      "~/Scripts/plugins/datepicker/bootstrap-datepicker.min.js"));
            // DatePicker
            bundles.Add(new StyleBundle("~/plugins/datepicker").Include(
                      "~/Content/plugins/datepicker/bootstrap-datepicker.min.css", new CssRewriteUrlTransform()));

            // DropZone
            bundles.Add(new ScriptBundle("~/bundles/dropzone").Include(
                      "~/Scripts/plugins/dropzone/dropzone.min.js"));
            // DropZone
            bundles.Add(new StyleBundle("~/plugins/dropzone").Include(
                      "~/Content/plugins/dropzone/dropzone.min.css", new CssRewriteUrlTransform()));

            // Datatable
            bundles.Add(new ScriptBundle("~/bundles/datatable").Include(
                      "~/Scripts/plugins/datatable/datatables.min.js"));
            // Datatable
            bundles.Add(new StyleBundle("~/plugins/datatable").Include(
                      "~/Content/plugins/datatable/datatables.min.css"));

            // Nifty script
            bundles.Add(new ScriptBundle("~/bundles/nifty").Include(
                      "~/Scripts/nifty.min.js",
                        "~/Scripts/plugins/match_height/jquery-match-height.min.js"));

            // SWAL script
            bundles.Add(new ScriptBundle("~/bundles/sweetAlert").Include(
                      "~/Scripts/plugins/sweetalert/sweetalert.min.js"));

            // SWAL style
            bundles.Add(new StyleBundle("~/plugins/sweetAlert").Include(
                      "~/Content/plugins/sweetalert/sweetalert.css"));
            
            // CSS Loaders style
            bundles.Add(new StyleBundle("~/plugins/cssloaders").Include(
                      "~/Content/plugins/css-loaders/css/css-loaders.css"));
            // Sparkline script
            bundles.Add(new ScriptBundle("~/bundles/sparkline").Include(
                      "~/Scripts/plugins/sparkline/jquery.sparkline.min.js"));
            // Easy Pie Charts script
            bundles.Add(new ScriptBundle("~/bundles/easychart").Include(
                      "~/Scripts/plugins/easy-pie-chart/jquery.easypiechart.min.js"));
            // Validator style
            bundles.Add(new StyleBundle("~/plugins/validator").Include(
                      "~/Content/plugins/bootstrap-validator/bootstrapValidator.min.css"));
            // Validator script
            bundles.Add(new ScriptBundle("~/bundles/validator").Include(
                      "~/Scripts/plugins/bootstrap-validator/bootstrapValidator.min.js"));
            // JSTree style
            bundles.Add(new StyleBundle("~/plugins/jstree").Include(
                      "~/Content/plugins/jstree/themes/default/style.min.css"));
            // JSTree script
            bundles.Add(new ScriptBundle("~/bundles/jstree").Include(
                      "~/Scripts/plugins/jstree/jstree.min.js"));
            // Date Range Picker style
            bundles.Add(new StyleBundle("~/plugins/daterangepicker").Include(
                      "~/Content/plugins/daterangepicker/daterangepicker.css"));
            // Date Range Picker script
            bundles.Add(new ScriptBundle("~/bundles/daterangepicker").Include(
                      "~/Scripts/plugins/daterangepicker/moment.min.js",
                      "~/Scripts/plugins/daterangepicker/daterangepicker.js"));

            // Flot script
            bundles.Add(new ScriptBundle("~/bundles/flot").Include(
                      "~/Scripts/plugins/flot-charts/jquery.flot.min.js",
                      "~/Scripts/plugins/flot-charts/jquery.flot.resize.min.js",
                      "~/Scripts/plugins/flot-charts/jquery.flot.tooltip.min.js"));

            // Gallery style
            bundles.Add(new StyleBundle("~/plugins/gallery").Include(
                      "~/Content/plugins/unitegallery/css/unitegallery.min.css"));
            // Gallery script
            bundles.Add(new ScriptBundle("~/bundles/gallery").Include(
                      "~/Scripts/plugins/unitegallery/js/unitegallery.min.js", "~/Scripts/plugins/unitegallery/themes/grid/ug-theme-grid.js"));
            // Slider script
            bundles.Add(new ScriptBundle("~/bundles/slider").Include(
                      "~/Scripts/plugins/unitegallery/js/unitegallery.min.js", "~/Scripts/plugins/unitegallery/themes/slider/ug-theme-slider.js"));
            // Star style
            bundles.Add(new StyleBundle("~/plugins/star").Include(
                      "~/Content/plugins/star/star.css"));
            // Star script
            bundles.Add(new ScriptBundle("~/bundles/star").Include(
                      "~/Scripts/plugins/star/star.js"));


        }
    }
}
