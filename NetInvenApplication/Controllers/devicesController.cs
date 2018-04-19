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
    public class devicesController : Controller
    {
        private NetInventoryEntities db = new NetInventoryEntities();

        // GET: devices
        public ActionResult Index()
        {
            var devices = db.devices.Include(d => d.computer).Include(d => d.device_types).Include(d => d.network_devices).Include(d => d.wired_devices);
            return View(devices.ToList());
        }

        // GET: devices/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            device device = db.devices.Find(id);
            if (device == null)
            {
                return HttpNotFound();
            }
            return View(device);
        }

        // GET: devices/Create
        public ActionResult Create()
        {
            ViewBag.asset_id = new SelectList(db.computers, "asset_id", "os_version");
            //ViewBag.manufacturer = new SelectList(db.device_models, "manufacturer", "manufacturer");
            ViewBag.device_type = new SelectList(db.device_types, "type", "type");
            ViewBag.asset_id = new SelectList(db.network_devices, "asset_id", "host_name");
            ViewBag.asset_id = new SelectList(db.wired_devices, "asset_id", "switch_port");
            return View();
        }

        // POST: devices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "asset_id,description,device_type,device_subtype,manufacturer,model,serial_number,building,room,status")] device device)
        {
            if (ModelState.IsValid)
            {
                db.devices.Add(device);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.asset_id = new SelectList(db.computers, "asset_id", "os_version", device.asset_id);
            //ViewBag.manufacturer = new SelectList(db.device_models, "manufacturer", "manufacturer", device.manufacturer);
            ViewBag.device_type = new SelectList(db.device_types, "type", "type", device.device_type);
            ViewBag.asset_id = new SelectList(db.network_devices, "asset_id", "host_name", device.asset_id);
            ViewBag.asset_id = new SelectList(db.wired_devices, "asset_id", "switch_port", device.asset_id);
            return View(device);
        }

        // GET: devices/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            device device = db.devices.Find(id);
            if (device == null)
            {
                return HttpNotFound();
            }
            ViewBag.asset_id = new SelectList(db.computers, "asset_id", "os_version", device.asset_id);
            //ViewBag.manufacturer = new SelectList(db.device_models, "manufacturer", "manufacturer", device.manufacturer);
            ViewBag.device_type = new SelectList(db.device_types, "type", "type", device.device_type);
            ViewBag.asset_id = new SelectList(db.network_devices, "asset_id", "host_name", device.asset_id);
            ViewBag.asset_id = new SelectList(db.wired_devices, "asset_id", "switch_port", device.asset_id);
            return View(device);
        }

        // POST: devices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "asset_id,description,device_type,device_subtype,manufacturer,model,serial_number,building,room,status")] device device)
        {
            if (ModelState.IsValid)
            {
                db.Entry(device).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.asset_id = new SelectList(db.computers, "asset_id", "os_version", device.asset_id);
            //ViewBag.manufacturer = new SelectList(db.device_models, "manufacturer", "manufacturer", device.manufacturer);
            ViewBag.device_type = new SelectList(db.device_types, "type", "type", device.device_type);
            ViewBag.asset_id = new SelectList(db.network_devices, "asset_id", "host_name", device.asset_id);
            ViewBag.asset_id = new SelectList(db.wired_devices, "asset_id", "switch_port", device.asset_id);
            return View(device);
        }

        // GET: devices/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            device device = db.devices.Find(id);
            if (device == null)
            {
                return HttpNotFound();
            }
            return View(device);
        }

        // POST: devices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            device device = db.devices.Find(id);
            db.devices.Remove(device);
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
