using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PatientManagementApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult PatientList()
        {
            //Wyciągnięcie listy pacjentów dla zalogowanego rehablitanta
            return View();
        }

        public ActionResult PatientExersicesList()
        {
            //Wyciąganięcie listy ćwiczeń dla danego pacjenta

        }
    }
}