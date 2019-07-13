using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Vickuraa.Models;

namespace Vickuraa.Controllers
{
    public class vesselsController : Controller
    {
        private vicukraaModel db = new vicukraaModel();

        // GET: vessels
        public ActionResult VesselList()
        {
            var vessels = db.vessels.Include(v => v.user);
            return View(vessels.ToList());
        }

        // GET: vessels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vessel vessel = db.vessels.Find(id);
            if (vessel == null)
            {
                return HttpNotFound();
            }
            return View(vessel);
        }

        // GET: vessels/Create
        public ActionResult AddVessel()
        {
            ViewBag.ownerID = new SelectList(db.users, "userID", "username");
            return View();
        }

        // POST: vessels/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddVessel([Bind(Include = "vesselID,VesselRegNo,ownerID,availWeight,availVolume,vesselName,enteredDate,enteredUser")] vessel vessel)
        {
            if (ModelState.IsValid)
            {
                vessel.enteredUser = 1;
                vessel.enteredDate = DateTime.Now;
                db.vessels.Add(vessel);
                db.SaveChanges();
                return RedirectToAction("VesselList");
            }

            ViewBag.ownerID = new SelectList(db.users, "userID", "username", vessel.ownerID);
            return View(vessel);
        }

        // GET: vessels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vessel vessel = db.vessels.Find(id);
            if (vessel == null)
            {
                return HttpNotFound();
            }
            ViewBag.ownerID = new SelectList(db.users, "userID", "username", vessel.ownerID);
            return View(vessel);
        }

        // POST: vessels/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "vesselID,VesselRegNo,ownerID,availWeight,availVolume,vesselName,enteredDate,enteredUser")] vessel vessel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vessel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("VesselList");
            }
            ViewBag.ownerID = new SelectList(db.users, "userID", "username", vessel.ownerID);
            return View(vessel);
        }

        // GET: vessels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vessel vessel = db.vessels.Find(id);
            if (vessel == null)
            {
                return HttpNotFound();
            }
            return View(vessel);
        }

        // POST: vessels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            vessel vessel = db.vessels.Find(id);
            db.vessels.Remove(vessel);
            db.SaveChanges();
            return RedirectToAction("VesselList");
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
