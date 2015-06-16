using CWI.ContraCheque.Importador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CWI.ContraCheque.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ImportaContraCheque importa = new ImportaContraCheque();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
    }
}