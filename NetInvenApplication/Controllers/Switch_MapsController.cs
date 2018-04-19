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
    public class Switch_MapsController : Controller
    {
        private NetInventoryEntities db = new NetInventoryEntities();

        // GET: Switch_Maps
        public ActionResult Index()
        {
            return View(db.Switch_Maps.ToList());
        }

        // GET: Switch_Maps/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Switch_Maps switch_Maps = db.Switch_Maps.Find(id);
            if (switch_Maps == null)
            {
                return HttpNotFound();
            }
            return View(switch_Maps);
        }

        // GET: Switch_Maps/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Switch_Maps/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "switch,switch_port,vlan_id,asset_id,description,mac_address,static_ip,building,room")] Switch_Maps switch_Maps)
        {
            if (ModelState.IsValid)
            {
                db.Switch_Maps.Add(switch_Maps);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(switch_Maps);
        }

        // GET: Switch_Maps/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Switch_Maps switch_Maps = db.Switch_Maps.Find(id);
            if (switch_Maps == null)
            {
                return HttpNotFound();
            }
            return View(switch_Maps);
        }

        // POST: Switch_Maps/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "switch,switch_port,vlan_id,asset_id,description,mac_address,static_ip,building,room")] Switch_Maps switch_Maps)
        {
            if (ModelState.IsValid)
            {
                db.Entry(switch_Maps).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(switch_Maps);
        }

        // GET: Switch_Maps/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Switch_Maps switch_Maps = db.Switch_Maps.Find(id);
            if (switch_Maps == null)
            {
                return HttpNotFound();
            }
            return View(switch_Maps);
        }

        // POST: Switch_Maps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            Switch_Maps switch_Maps = db.Switch_Maps.Find(id);
            db.Switch_Maps.Remove(switch_Maps);
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
