﻿using System;
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
            return View();
        }

        public ActionResult Welcome()
        {
            return View();
        }
    }
}