using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrenchTownStudiosV2.UI.MVC.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShootHappens()
        {
            return View();
        }

        public ActionResult ShootHappensLimit()
        {
            return View();
        }
    }
}