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
    public class ClientDetailsController : Controller
    {
        private FSDPRegistrationSystemEntities db = new FSDPRegistrationSystemEntities();

        // GET: ClientDetails
        public ActionResult Index()
        {
            if (Request.IsAuthenticated && User.IsInRole("Admin") || User.IsInRole("Employee"))
            {
                return View(db.ClientDetails.ToList());
            }
            else
            {
                string currentUserID = User.Identity.GetUserId();
                return View(db.ClientDetails.Where(cd => cd.ClientId == currentUserID).ToList());
            }
        }



        // GET: ClientDetails/Details/5
        //public ActionResult Details(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ClientDetail clientDetail = db.ClientDetails.Find(id);
        //    if (clientDetail == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(clientDetail);
        //}

        // GET: ClientDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClientDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClientId,FirstName,LastName")] ClientDetail clientDetail)
        {
            if (ModelState.IsValid)
            {
                db.ClientDetails.Add(clientDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(clientDetail);
        }

        // GET: ClientDetails/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientDetail clientDetail = db.ClientDetails.Find(id);
            if (clientDetail == null)
            {
                return HttpNotFound();
            }
            return View(clientDetail);
        }

        // POST: ClientDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClientId,FirstName,LastName")] ClientDetail clientDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clientDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(clientDetail);
        }

        // GET: ClientDetails/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientDetail clientDetail = db.ClientDetails.Find(id);
            if (clientDetail == null)
            {
                return HttpNotFound();
            }
            return View(clientDetail);
        }

        // POST: ClientDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ClientDetail clientDetail = db.ClientDetails.Find(id);
            db.ClientDetails.Remove(clientDetail);
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
