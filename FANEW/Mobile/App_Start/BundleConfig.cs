using System.Web;
using System.Web.Optimization;
using Anchor.FA.Utility;

namespace Anchor.FA.Mobile
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/easyui1.5.2/themes/gray/easyui.css",
                        "~/Content/easyui1.5.2/themes/icon.css",
                        "~/Content/easyui1.5.2/themes/mobile.css"));

       
            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                            "~/Content/easyui1.5.2/jquery.min.js",
                            "~/Content/easyui1.5.2/jquery.easyui.min.js",
                            "~/Content/easyui1.5.2/jquery.easyui.mobile.js",
                            "~/Content/easyui1.5.2/locale/easyui-lang-zh_CN.js",
                            "~/Content/Script/Tab.js"));

        }
    }
}