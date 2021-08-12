using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FrenchTownStudiosV2.DATA.EF;
using Microsoft.AspNet.Identity;

namespace FrenchTownStudiosV2.UI.MVC.Controllers
{
    [Authorize]
    public class ReservationsController : Controller
    {
        private FSDPRegistrationSystemEntities db = new FSDPRegistrationSystemEntities();

        // GET: Reservations
        public ActionResult Index()
        {
            if (Request.IsAuthenticated && User.IsInRole("Admin") || User.IsInRole("Employee"))
            {
                return View(db.Reservations.ToList());
            }
            else
            {
                string currentUserID = User.Identity.GetUserId();
                var reservations = db.Reservations.Where(d => d.ClientAsset.ClientId == currentUserID);
                return View(reservations.ToList());
            }
        }

        // GET: Reservations/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Reservation reservation = db.Reservations.Find(id);
        //    if (reservation == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(reservation);
        //}

        // GET: Reservations/Create
        public ActionResult Create()
        {
            ViewBag.ClientAssetsId = new SelectList(db.ClientAssets, "ClientAssetsId", "AssetName");
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "LocationName");
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReservationId,ClientAssetsId,LocationId,ReservationDate")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.Reservations.Add(reservation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClientAssetsId = new SelectList(db.ClientAssets, "ClientAssetsId", "AssetName", reservation.ClientAssetsId);
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "LocationName", reservation.LocationId);
            return View(reservation);
        }

        // GET: Reservations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientAssetsId = new SelectList(db.ClientAssets, "ClientAssetsId", "AssetName", reservation.ClientAssetsId);
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "LocationName", reservation.LocationId);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReservationId,ClientAssetsId,LocationId,ReservationDate")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClientAssetsId = new SelectList(db.ClientAssets, "ClientAssetsId", "AssetName", reservation.ClientAssetsId);
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "LocationName", reservation.LocationId);
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reservation reservation = db.Reservations.Find(id);
            db.Reservations.Remove(reservation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
