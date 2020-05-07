using Enteprise_programming_evita.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Enteprise_programming_evita.Controllers
{
    public class QualityController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Quality
        public ActionResult Index()
        {
            return View(db.Qualities.ToList());
        }

     

    }
}