using System.Web;
using System.Web.Mvc;

namespace Viva.CorporateSys.DanceMVC
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}