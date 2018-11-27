using System.Web;
using System.Web.Optimization;
using Anchor.FA.Utility;

namespace Anchor.FA.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/easyui1.5.2/themes/default/easyui.css",
                        "~/Content/easyui1.5.2/themes/icon.css",
                        "~/Content/css/default.css"));

            if (AppConfig.NetworkSegment)
            {
                bundles.Add(new ScriptBundle("~/bundles/js").Include(
                            "~/Content/Script/jquery-1.8.0.min.js",
                            "~/Content/easyui1.5.2/jquery.easyui.min.js",
                            "~/Content/easyui1.5.2/locale/easyui-lang-zh_CN.js",
                            "~/Content/Script/jquery.tips.js",
                            "~/Content/Script/datagrid-detailview.js",
                            "~/Content/Script/jquery.maskedinput-1.2.2.js",
                            "~/Content/Script/Tab.js",
                            "~/Content/Script/NetworkSegment.js"));
            }
            else
            {
                bundles.Add(new ScriptBundle("~/bundles/js").Include(
                            "~/Content/Script/jquery-1.8.0.min.js",
                            "~/Content/easyui1.5.2/jquery.easyui.min.js",
                            "~/Content/easyui1.5.2/locale/easyui-lang-zh_CN.js",
                            "~/Content/Script/jquery.tips.js",
                            "~/Content/Script/datagrid-detailview.js",
                            "~/Content/Script/jquery.maskedinput-1.2.2.js",
                            "~/Content/Script/Tab.js"));
            }

            bundles.Add(new ScriptBundle("~/flows/js").Include("~/Areas/Content/Script/public.js"));

            bundles.Add(new ScriptBundle("~/ckeditor/js").Include("~/Content/ckeditor4.5.6/ckeditor.js"));

        }
    }
}