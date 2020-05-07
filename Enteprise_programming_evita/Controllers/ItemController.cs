using Enteprise_programming_evita.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Enteprise_programming_evita.Controllers
{
    public class ItemController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Properties
        [Authorize()]
        public ActionResult Index()
        {
            if (User.IsInRole("Admin")) {
                var ActiveUserId = User.Identity.GetUserName();
                var itemslist = db.Items.Include(I => I.ItemType).Include(q => q.Quality);
                ViewBag.username = ActiveUserId;
                return View(itemslist.ToList());
            }
            else if (User.IsInRole("RegisteredUser")) {
                var ActiveUserId = User.Identity.GetUserName();
                var itemslist = db.Items.Include(p => p.ItemType).Include(q => q.Quality).ToList();
                return View(itemslist.ToList());
            }
            else { }

            return View(db.Items.ToList());
        }

        // GET: Properties/Details/5
        [Authorize()]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }



        // GET: Properties/Create
        [Authorize()]
        public ActionResult Create()
        {

            ViewBag.ItemTypeId = new SelectList(db.ItemTypes, "Id", "Name");
            ViewBag.QualityId = new SelectList(db.Qualities, "QualityId", "QualityName");
            return View();
        }

        // POST: Properties/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize()]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ItemTypeId,QualityId,Quantity,Price,Owner")] Item item)
        {
            ViewBag.QualityId = new SelectList(db.Qualities, "QualityId", "QualityName", item.QualityId);
            ViewBag.ItemTypeId = new SelectList(db.ItemTypes, "Id", "Name", item.ItemTypeId);

            if (ModelState.IsValid)

            {
                if (db.Items.Any(ac => ac.ItemTypeId.Equals(item.ItemTypeId)))
                {
                    
                     

                        ViewBag.wrong = ("Itemtype is the same, change the price or qunatity");
          
                }
            }

            else
            {
                item.Owner = User.Identity.GetUserId();
                db.Items.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }



            return View(item);
        }

    


        // GET: Properties/Edit/5
        [Authorize()]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", item.ItemType);
            ViewBag.ItemTypeId = new SelectList(db.ItemTypes, "Id", "Name");
            ViewBag.QualityId = new SelectList(db.Qualities, "QualityId", "QualityName");
            return View(item);
        }

        // POST: Properties/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize()]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CategoryId,Price,Name,Image")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ItemTypeId = new SelectList(db.ItemTypes, "Id", "Name", item.ItemType);
            return View(item);
        }

        // GET: Properties/Delete/5
        [Authorize()]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Properties/Delete/5
        [Authorize()]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Item item = db.Items.Find(id);
            db.Items.Remove(item);
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