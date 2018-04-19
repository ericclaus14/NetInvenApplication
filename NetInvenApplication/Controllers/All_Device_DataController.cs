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
    public class All_Device_DataController : Controller
    {
        private NetInventoryEntities db = new NetInventoryEntities();

        // GET: All_Device_Data
        public ActionResult Index()
        {
            return View(db.All_Device_Data.ToList());
        }

        // GET: All_Device_Data/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            All_Device_Data all_Device_Data = db.All_Device_Data.Find(id);
            if (all_Device_Data == null)
            {
                return HttpNotFound();
            }
            return View(all_Device_Data);
        }

        // GET: All_Device_Data/Create
        public ActionResult Create()
        {
            // Create lists of device types and subtypes to be used in drop-down lists
            NetInventoryEntities deviceTypeEntities = new NetInventoryEntities();
            var getDeviceTypeList = deviceTypeEntities.Device_Types_View.ToList();
            SelectList deviceTypeList = new SelectList(getDeviceTypeList, "type", "type");
            ViewBag.deviceTypeList = deviceTypeList;
            NetInventoryEntities deviceSubTypeEntities = new NetInventoryEntities();
            var getDeviceSubTypeList = deviceSubTypeEntities.device_types.ToList();
            SelectList deviceSubTypeList = new SelectList(getDeviceSubTypeList, "subtype", "subtype");
            ViewBag.deviceSubTypeList = deviceSubTypeList;
            
            return View();
        }

        // POST: All_Device_Data/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "asset_id,description,device_type,device_subtype,manufacturer,model,serial_number,building,room,status,host_name,mac_address,static_ip,switch,switch_port,vlan_id,os_version,cpu,ram,hdd")] All_Device_Data all_Device_Data)
        {
            if (ModelState.IsValid)
            {
                db.All_Device_Data.Add(all_Device_Data);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DeviceTypes = new SelectList(db.device_types, "Name", "Name");
            return View(all_Device_Data);
        }

        // GET: All_Device_Data/Edit/5
        public ActionResult Edit(short? id)
        {
            // Create lists of device types and subtypes to be used in drop-down lists
            NetInventoryEntities deviceTypeEntities = new NetInventoryEntities();
            var getDeviceTypeList = deviceTypeEntities.Device_Types_View.ToList();
            SelectList deviceTypeList = new SelectList(getDeviceTypeList, "type", "type");
            ViewBag.deviceTypeList = deviceTypeList;
            NetInventoryEntities deviceSubTypeEntities = new NetInventoryEntities();
            var getDeviceSubTypeList = deviceSubTypeEntities.device_types.ToList();
            SelectList deviceSubTypeList = new SelectList(getDeviceSubTypeList, "subtype", "subtype");
            ViewBag.deviceSubTypeList = deviceSubTypeList;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            All_Device_Data all_Device_Data = db.All_Device_Data.Find(id);
            if (all_Device_Data == null)
            {
                return HttpNotFound();
            }
            return View(all_Device_Data);
        }

        // POST: All_Device_Data/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "asset_id,description,device_type,device_subtype,manufacturer,model,serial_number,building,room,status,host_name,mac_address,static_ip,switch,switch_port,vlan_id,os_version,cpu,ram,hdd")] All_Device_Data all_Device_Data)
        {
            if (ModelState.IsValid)
            {
                db.Entry(all_Device_Data).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(all_Device_Data);
        }

        // GET: All_Device_Data/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            All_Device_Data all_Device_Data = db.All_Device_Data.Find(id);
            if (all_Device_Data == null)
            {
                return HttpNotFound();
            }
            return View(all_Device_Data);
        }

        // POST: All_Device_Data/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            All_Device_Data all_Device_Data = db.All_Device_Data.Find(id);
            db.All_Device_Data.Remove(all_Device_Data);
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
