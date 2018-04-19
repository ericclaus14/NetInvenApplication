using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NetInvenApplication.Models;

namespace NetInvenApplication.Controllers
{
    public class device_typesController : Controller
    {
        private NetInventoryEntities db = new NetInventoryEntities();

        // GET: device_types
        public ActionResult Index()
        {
            return View(db.device_types.ToList());
        }

        // GET: device_types/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            device_types device_types = db.device_types.Find(id);
            if (device_types == null)
            {
                return HttpNotFound();
            }
            return View(device_types);
        }

        // GET: device_types/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: device_types/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "type,subtype")] device_types device_types)
        {
            if (ModelState.IsValid)
            {
                db.device_types.Add(device_types);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(device_types);
        }

        // GET: device_types/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            device_types device_types = db.device_types.Find(id);
            if (device_types == null)
            {
                return HttpNotFound();
            }
            return View(device_types);
        }

        // POST: device_types/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "type,subtype")] device_types device_types)
        {
            if (ModelState.IsValid)
            {
                db.Entry(device_types).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(device_types);
        }

        // GET: device_types/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            device_types device_types = db.device_types.Find(id);
            if (device_types == null)
            {
                return HttpNotFound();
            }
            return View(device_types);
        }

        // POST: device_types/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            device_types device_types = db.device_types.Find(id);
            db.device_types.Remove(device_types);
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
