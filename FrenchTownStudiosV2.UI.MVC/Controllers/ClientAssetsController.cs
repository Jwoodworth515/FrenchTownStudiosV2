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
using System.Drawing;
using FrenchTownStudiosV2.UI.MVC.Utilities;


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
        public ActionResult Create([Bind(Include = "ClientAssetsId,AssetName,ClientId,AssetPhoto,SpecialNotes,IsActive,DateAdded")] ClientAsset clientAsset, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                string file = "noimage.png";

                if (image != null)
                {
                    file = image.FileName;
                    string ext = file.Substring(file.LastIndexOf("."));
                    string[] goodExts = { ".jpeg", ".jpg", ".png", ".gif" };

                    //Check that the uploaded file is in our list of acceptable exts and that the file size <= 4mb max from ASP.NET
                    if (goodExts.Contains(ext.ToLower()))
                    {
                        //create a new file name using a GUID
                        file = Guid.NewGuid() + ext;

                        //save the image

                        string savePath = Server.MapPath("~/Content/img/");

                        Image convertedImage = Image.FromStream(image.InputStream);

                        int maxImageSize = 500;
                        int maxThumbSize = 100;

                        ImageUtility.ResizeImage(savePath, file, convertedImage, maxImageSize, maxThumbSize);

                    }
                    //no matter what, update the PhotoUrl with the value of the file variable
                }
                clientAsset.AssetPhoto = file;

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
        public ActionResult Edit([Bind(Include = "ClientAssetsId,AssetName,ClientId,AssetPhoto,SpecialNotes,IsActive,DateAdded")] ClientAsset clientAsset, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {

                //check to see if a new file has been uploaded. If not, the HiddenFor() in the View will maintain the original value (image)
                string file = "noimage.png";

                if (image != null)
                {
                    //get the name
                    file = image.FileName;

                    //capture the extension
                    string ext = file.Substring(file.LastIndexOf("."));

                    //Create a "Whitelisted" array of acceptable exts
                    string[] goodExts = { ".jpg", ".jpeg", ".png", ".gif" };

                    //Check the extension and file size
                    if (goodExts.Contains(ext.ToLower()))
                    {
                        //create a new file name
                        file = Guid.NewGuid() + ext;

                        //Resize the image
                        string savePath = Server.MapPath("~/Content/img/");

                        Image convertedImage = Image.FromStream(image.InputStream);

                        int maxImageSize = 500;
                        int maxThumbSize = 100;

                        ImageUtility.ResizeImage(savePath, file, convertedImage, maxImageSize, maxThumbSize);

                        if (clientAsset.AssetPhoto != null && clientAsset.AssetPhoto != "noimage.png")
                        {
                            string path = Server.MapPath("~/Content/img/");
                            ImageUtility.Delete(path, clientAsset.AssetPhoto);
                        }
                    }
                    //update the property of the book object
                }
                clientAsset.AssetPhoto = file;

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
            string path = Server.MapPath("~/Content/img/");
            ImageUtility.Delete(path, clientAsset.AssetPhoto);


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
