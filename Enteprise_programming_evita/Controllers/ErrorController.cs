﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Enteprise_programming_evita.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PageNotFound()
        {
            return View();
        }

        public ActionResult DefaultErrorPage()
        {
            return View();
        }
    }
}