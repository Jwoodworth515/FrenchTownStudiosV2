using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FrenchTownStudiosV2.DATA.EF;
using System.Data.Entity;
using System.Data;
using System.Net;

namespace FrenchTownStudiosV2.UI.MVC.Controllers
{
    public class FiltersController : Controller
    {
        private FSDPRegistrationSystemEntities db = new FSDPRegistrationSystemEntities();
        // GET: Filters
        public ActionResult ReservationTable()
        {
            DbSet<Reservation> reservations = db.Reservations;
            return View(reservations.ToList());
        }
    }
}