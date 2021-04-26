using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hiriart_Corales_MVCWebApp_AgendaPersonal.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Eventos()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //Aniadir tabs para las 5 tablas de la DB
    }
}