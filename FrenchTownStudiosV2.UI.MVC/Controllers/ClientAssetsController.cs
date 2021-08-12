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
    public class ClientAssetsController : Controller
    {
        private FSDPRegistrationSystemEntities db = new FSDPRegistrationSystemEntities();

        // GET: ClientAssets
        public ActionResult Index()
        {
            if (Request.IsAuthenticated && User.IsInRole("Admin") || User.IsInRole("Employee"))
            {
                return View(db.ClientAssets.ToList());
            }
            else
            {
                string currentUserID = User.Identity.GetUserId();
                var clientAssets = db.ClientAssets.Where(ca => ca.ClientId == currentUserID);
                return View(clientAssets.ToList());
            }
        }

        // GET: ClientAssets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientAsset clientAsset = db.ClientAssets.Find(id);
            if (clientAsset == null)
            {
                return HttpNotFound();
            }
            return View(clientAsset);
        }

        // GET: ClientAssets/Create
        public ActionResult Create()
        {
            ViewBag.ClientId = new SelectList(db.ClientDetails, "ClientId", "FirstName");
            return View();
        }

        // POST: ClientAssets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClientAssetsId,AssetName,ClientId,AssetPhoto,SpecialNotes,IsActive,DateAdded")] ClientAsset clientAsset)
        {
            if (ModelState.IsValid)
            {
                db.ClientAssets.Add(clientAsset);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClientId = new SelectList(db.ClientDetails, "ClientId", "FirstName", clientAsset.ClientId);
            return View(clientAsset);
        }

        // GET: ClientAssets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientAsset clientAsset = db.ClientAssets.Find(id);
            if (clientAsset == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientId = new SelectList(db.ClientDetails, "ClientId", "FirstName", clientAsset.ClientId);
            return View(clientAsset);
        }

        // POST: ClientAssets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClientAssetsId,AssetName,ClientId,AssetPhoto,SpecialNotes,IsActive,DateAdded")] ClientAsset clientAsset)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clientAsset).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClientId = new SelectList(db.ClientDetails, "ClientId", "FirstName", clientAsset.ClientId);
            return View(clientAsset);
        }

        // GET: ClientAssets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientAsset clientAsset = db.ClientAssets.Find(id);
            if (clientAsset == null)
            {
                return HttpNotFound();
            }
            return View(clientAsset);
        }

        // POST: ClientAssets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClientAsset clientAsset = db.ClientAssets.Find(id);
            db.ClientAssets.Remove(clientAsset);
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
