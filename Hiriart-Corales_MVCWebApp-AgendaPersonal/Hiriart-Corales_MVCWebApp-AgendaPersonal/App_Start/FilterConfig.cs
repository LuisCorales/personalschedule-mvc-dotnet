using System.Web;
using System.Web.Mvc;

namespace Hiriart_Corales_MVCWebApp_AgendaPersonal
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
