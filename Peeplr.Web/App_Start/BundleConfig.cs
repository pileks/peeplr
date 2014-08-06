using System.Web;
using System.Web.Optimization;

namespace Peeplr.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new StyleBundle("~/Assets/Styles").Include(
            //          "~/Assets/Styles/bootstrap.min.css",
            //          "~/Assets/Styles/bootstrap-theme.min.css",
            //          "~/Assets/Styles/site.css",
            //          "~/Assets/Fonts/glyphicons-halflings-regular.woff",
            //          "~/Assets/Fonts/glyphicons-halflings-regular.eot",
            //          "~/Assets/Fonts/glyphicons-halflings-regular.svg",
            //          "~/Assets/Fonts/glyphicons-halflings-regular.ttf"));

            BundleTable.EnableOptimizations = true;
        }
    }
}
