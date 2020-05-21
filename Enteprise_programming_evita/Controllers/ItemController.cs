using Enteprise_programming_evita.Models;
using Microsoft.AspNet.Identity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PagedList.Mvc;
using PagedList;

namespace Enteprise_programming_evita.Controllers
{
    public class ItemController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Properties
        [Authorize()]
        public ActionResult Index(String sortOrder,int ? m)
        {
            ViewBag.sort = sortOrder;
            var ActiveUserId = User.Identity.GetUserName();
                var itemslist = db.Items.Include(I => I.ItemType).Include(q => q.Quality).Include(u=>u.Owner);
                ViewBag.username = ActiveUserId;
            if (sortOrder != null)
            {
                string check = sortOrder.Substring(0, 3);
                switch (check)
                {
                    case "dis":
                        itemslist = itemslist.OrderByDescending(s => s.AddingDate);
                        break;
                    case "ais":
                        itemslist = itemslist.OrderBy(s => s.AddingDate);
                        break;
                    case "use":
                        string email = sortOrder.Substring(3);
                        itemslist = itemslist.Where(i => i.Owner.Email.Equals(email));
                        break;
                    default:

                        break;


                }
            }
            return View(itemslist.ToList().ToPagedList(m ?? 1,2));
            
           

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
        public ActionResult Create([Bind(Include = "Id,ItemTypeId,QualityId,Quantity,Price,Owner,AddingDate")] Item item)
        {
            ViewBag.QualityId = new SelectList(db.Qualities, "QualityId", "QualityName", item.QualityId);
            ViewBag.ItemTypeId = new SelectList(db.ItemTypes, "Id", "Name", item.ItemTypeId);

            if (ModelState.IsValid)

            {
                try
                {
                    using (var db = new ApplicationDbContext())
                    {
                        item.AddingDate = DateTime.Now;
                        item.OwnerId = User.Identity.GetUserId();
                        db.Items.Add(item);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                catch (DbUpdateException ex)
                {
                    ViewBag.wrong = "Itemtype with these paremeters already exists! change price, quality or quantity";
                }


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
            ViewBag.QualityId = new SelectList(db.Qualities, "QualityId", "QualityName", item.QualityId);
            ViewBag.ItemTypeId = new SelectList(db.ItemTypes, "Id", "Name", item.ItemType);
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", item.ItemType);

            return View(item);
        }

        // POST: Properties/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize()]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemId,CategoryId, Quantity,Quality, Owner, QualityId, OwnerId, ItemType, ItemTypeId, Price,Name,Image,AddingDate")] Item item)
        {
            if (ModelState.IsValid)

            {
                try
                {
                    using (var db = new ApplicationDbContext())
                    {

                        db.Entry(item).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                catch (DbUpdateException ex)
                {
                    ViewBag.wrong = "Itemtype with these paremeters already exists! change price, quality or quantity";
                }
                ViewBag.QualityId = new SelectList(db.Qualities, "QualityId", "QualityName", item.QualityId);
                ViewBag.ItemTypeId = new SelectList(db.ItemTypes, "Id", "Name", item.ItemType);
                return View(item);
            }
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