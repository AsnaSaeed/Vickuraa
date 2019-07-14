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
    public class schedulesController : Controller
    {
        private vicukraaModel db = new vicukraaModel();

        // GET: schedules
        public ActionResult ScheduleList()
        {
            var schedules = db.schedules.Include(s => s.route).Include(s => s.user).Include(s => s.vessel);
            return View(schedules.ToList());
        }

        // GET: schedules/Details/5
        public ActionResult ScheduleDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            schedule schedule = db.schedules.Find(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }
            return View(schedule);
        }

        // GET: schedules/Create
        public ActionResult AddSchedule()
        {
            //var route = db.Routes
            //ViewBag.routeID = new SelectList(db.routes, "routeID", "de");
            var route = db.routes.Select(x => new
            {
                routeID = x.routeID,
                RouteName = x.destination.destinationName 
            });
            ViewBag.routeID = new SelectList(route, "routeID", "RouteName");

            ViewBag.enteredUser = new SelectList(db.users, "userID", "username");
            ViewBag.vesselID = new SelectList(db.vessels, "vesselID", "VesselName");
            return View();
        }

        // POST: schedules/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddSchedule([Bind(Include = "scheduleID,routeID,vesselID,estimateDeparturetime,estimateArrivaltime,arrivedDate,departedDate,enteredUser,entereddate")] schedule schedule)
        {
            if (ModelState.IsValid)
            {
                schedule.entereddate = DateTime.Now;
                schedule.enteredUser = 1;
                db.schedules.Add(schedule);
                db.SaveChanges();
                return RedirectToAction("ScheduleList");
            }

            ViewBag.routeID = new SelectList(db.routes, "routeID", "routeID", schedule.routeID);
            ViewBag.enteredUser = new SelectList(db.users, "userID", "username", schedule.enteredUser);
            ViewBag.vesselID = new SelectList(db.vessels, "vesselID", "VesselRegNo", schedule.vesselID);
            return View(schedule);
        }

        // GET: schedules/Edit/5
        public ActionResult EditSchedule(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            schedule schedule = db.schedules.Find(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }
            ViewBag.routeID = new SelectList(db.routes, "routeID", "routeID", schedule.routeID);
            ViewBag.enteredUser = new SelectList(db.users, "userID", "username", schedule.enteredUser);
            ViewBag.vesselID = new SelectList(db.vessels, "vesselID", "VesselRegNo", schedule.vesselID);
            return View(schedule);
        }

        // POST: schedules/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSchedule([Bind(Include = "scheduleID,routeID,vesselID,estimateDeparturetime,estimateArrivaltime,arrivedDate,departedDate,enteredUser,entereddate")] schedule schedule)
        {
            if (ModelState.IsValid)
            {
                //schedule.user = 4;
                db.Entry(schedule).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ScheduleList");
            }
            ViewBag.routeID = new SelectList(db.routes, "routeID", "routeID", schedule.routeID);
            ViewBag.enteredUser = new SelectList(db.users, "userID", "username", schedule.enteredUser);
            ViewBag.vesselID = new SelectList(db.vessels, "vesselID", "VesselRegNo", schedule.vesselID);
            return View(schedule);
        }


        public ActionResult UpdateArrivalDate(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            schedule schedule = db.schedules.Find(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }

            ViewBag.scheduleID = id;
            return View(schedule);
        }

        // POST: schedules/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateArrivalDate(int scheduleID, DateTime arrivedDate)
        {
            if (ModelState.IsValid)
            {
                var schedule = db.schedules.Where(a => a.scheduleID == scheduleID).FirstOrDefault();
                schedule.arrivedDate = arrivedDate;
                db.Entry(schedule).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ScheduleList");
            }
            return View();
        }


        public ActionResult UpdateDepartureDate(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            schedule schedule = db.schedules.Find(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }

            ViewBag.scheduleID = id;
            return View(schedule);
        }

        // POST: schedules/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateDepartureDate(int scheduleID, DateTime departedDate)
        {
            if (ModelState.IsValid)
            {
                var schedule = db.schedules.Where(a => a.scheduleID == scheduleID).FirstOrDefault();
                schedule.departedDate = departedDate;
                db.Entry(schedule).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ScheduleList");
            }
            return View();
        }
        // GET: schedules/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            schedule schedule = db.schedules.Find(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }
            return View(schedule);
        }

        // POST: schedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            schedule schedule = db.schedules.Find(id);
            db.schedules.Remove(schedule);
            db.SaveChanges();
            return RedirectToAction("ScheduleList");
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
