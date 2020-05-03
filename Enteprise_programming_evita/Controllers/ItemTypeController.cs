using Dropbox.Api;
using Dropbox.Api.Files;
using Enteprise_programming_evita.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Enteprise_programming_evita.Controllers
{
    public class ItemTypeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Properties
        public ActionResult Index()
        {
            var data = db.ItemTypes.Include(p => p.Category);
            return View(data.ToList());
        }

        // GET: Properties/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemType itemType = db.ItemTypes.Find(id);
            if (itemType == null)
            {
                return HttpNotFound();
            }
            return View(itemType);
        }

        // GET: Properties/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            return View();
        }

        // POST: Properties/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
  
        public ActionResult Create([Bind(Include = "Id,CategoryId,Name, Price")] ItemType itemType, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            { 
                if(Image == null )
                {
                    ModelState.AddModelError("Image", "Image can not be empty");
                    ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", itemType.CategoryId);
                    return View(itemType);
                }else
                    if(!db.ItemTypes.Any(i => i.Name == itemType.Name))
                {
                    string accessToken = "5clFbYDf69AAAAAAAAAAUpeTeTocwvjJUn-ymBjZSVxc9398emg-8ZWK6uIkX994";
                    using (DropboxClient client = new DropboxClient(accessToken, new DropboxClientConfig(ApplicationName)))
                    {

                        string[] spitInputFileName = Image.FileName.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries);
                        string fileNameAndExtension = spitInputFileName[spitInputFileName.Length - 1];

                        string[] fileNameAndExtensionSplit = fileNameAndExtension.Split('.');
                        string originalFileName = fileNameAndExtensionSplit[0];
                        string originalExtension = fileNameAndExtensionSplit[1];

                        string fileName = @"/Images/" + originalFileName + Guid.NewGuid().ToString().Replace("-", "") + "." + originalExtension;

                        var updated = client.Files.UploadAsync(
                            fileName,
                            mode: WriteMode.Overwrite.Overwrite.Instance,
                            body: Image.InputStream).Result;

                        var result = client.Sharing.CreateSharedLinkWithSettingsAsync(fileName).Result;
                        itemType.ImageUrl = result.Name;
                        itemType.Image = result.Url.Replace("?dl=0", "?raw=1");
                    }
        
                    db.ItemTypes.Add(itemType);
                    db.SaveChanges();
                    return RedirectToAction("Index");


                }
            }

                ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", itemType.CategoryId);
                return View(itemType);
            
        }

        // GET: Properties/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemType itemType = db.ItemTypes.Find(id);
            if (itemType == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", itemType.CategoryId);
            return View(itemType);
        }

        // POST: Properties/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CategoryId,Price,Name,Image")] ItemType itemType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(itemType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", itemType.CategoryId);
            return View(itemType);
        }

        // GET: Properties/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemType itemType = db.ItemTypes.Find(id);
            if (itemType == null)
            {
                return HttpNotFound();
            }
            return View(itemType);
        }

        // POST: Properties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ItemType itemType = db.ItemTypes.Find(id);
            db.ItemTypes.Remove(itemType);
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

        public ActionResult UploadImage()
        {
            return View();
        }
        static string ApplicationName = "Enterprise_programming_evita";

        [HttpPost]
        public ActionResult UploadImage(HttpPostedFileBase file)
        {
            string accessToken = "5clFbYDf69AAAAAAAAAAUpeTeTocwvjJUn-ymBjZSVxc9398emg-8ZWK6uIkX994";
            using (DropboxClient client = new DropboxClient(accessToken, new DropboxClientConfig(ApplicationName)))
            {
                string[] spitInputFileName = file.FileName.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries);
                string fileNameAndExtension = spitInputFileName[spitInputFileName.Length - 1];

                string[] fileNameAndExtensionSplit = fileNameAndExtension.Split('.');
                string originalFileName = fileNameAndExtensionSplit[0];
                string originalExtension = fileNameAndExtensionSplit[1];

                string fileName = @"/Images/" + originalFileName + Guid.NewGuid().ToString().Replace("-", "") + "." + originalExtension;

                var updated = client.Files.UploadAsync(
                    fileName,
                    mode: WriteMode.Overwrite.Overwrite.Instance,
                    body: file.InputStream).Result;

                var result = client.Sharing.CreateSharedLinkWithSettingsAsync(fileName).Result;
                return RedirectToAction("Create", "ItemType", new { ImageUrl = result.Url});
            }
        }
  

      
    }
}