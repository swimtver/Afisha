using System.Web.Optimization;

namespace Afisha.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));
            bundles.Add(new StyleBundle("~/Content/bootstrap")
                            .Include("~/Content/bootstrap.css",
                                     "~/Content/bootstrap-responsive.css"));
        }
    }
}