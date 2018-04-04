using System.Web;
using System.Web.Mvc;

namespace Anchor.FA.Mobile
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}