using System.Web;
using System.Web.Optimization;

namespace drugstore_003
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                            "~/Content/vendor/jquery/jquery.min.js",
                            "~/Content/vendor/bootstrap/js/bootstrap.bundle.min.js",
                            "~/Content/vendor/jquery-easing/jquery.easing.min.js",
                            "~/Content/vendor/chart.js/Chart.min.js",
                            "~/Content/vendor/datatables/jquery.dataTables.js",
                            "~/Content/vendor/datatables/dataTables.bootstrap4.js",
                            "~/Content/js/sb-admin.min.js",
                            "~/Content/js/demo/datatables-demo.js",
                            "~/Content/js/demo/chart-area-demo.js"


                      ));


            bundles.Add(new StyleBundle("~/Content/css").Include(

                      "~/Content/vendor/fontawesome-free/css/all.min.css",
                      "~/Content/vendor/datatables/dataTables.bootstrap4.css",
                      "~/Content/css/sb-admin.css"));
        }
    }
}
