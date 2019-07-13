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
    public class bookingsController : Controller
    {
        private vicukraaModel db = new vicukraaModel();

        // GET: bookings
        public ActionResult Index()
        {
            var bookings = db.bookings.Include(b => b.package).Include(b => b.schedule).Include(b => b.user);
            return View(bookings.ToList());
        }

        // GET: bookings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            booking booking = db.bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // GET: bookings/Create
        public ActionResult Create()
        {
            ViewBag.packageID = new SelectList(db.packages, "packageID", "packageID");
            ViewBag.scheduleID = new SelectList(db.schedules, "scheduleID", "scheduleID");
            ViewBag.customerID = new SelectList(db.users, "userID", "username");
            return View();
        }

        // POST: bookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "bookingID,scheduleID,customerID,quantity,receiverName,receiverAddress,receiverContactNo,senderName,senderContactNo,packageID,date,invoiceNo,invoiceYear,RecievedPhysicalUser,RecievedPhyscialDate,paymentRecievedUser,paymentRecievedDate,receiptNo,receiptYear,dispatchedUser,dispatchedDate")] booking booking)
        {
            if (ModelState.IsValid)
            {
                db.bookings.Add(booking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.packageID = new SelectList(db.packages, "packageID", "packageID", booking.packageID);
            ViewBag.scheduleID = new SelectList(db.schedules, "scheduleID", "scheduleID", booking.scheduleID);
            ViewBag.customerID = new SelectList(db.users, "userID", "username", booking.customerID);
            return View(booking);
        }

        // GET: bookings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            booking booking = db.bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            ViewBag.packageID = new SelectList(db.packages, "packageID", "packageID", booking.packageID);
            ViewBag.scheduleID = new SelectList(db.schedules, "scheduleID", "scheduleID", booking.scheduleID);
            ViewBag.customerID = new SelectList(db.users, "userID", "username", booking.customerID);
            return View(booking);
        }

        // POST: bookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "bookingID,scheduleID,customerID,quantity,receiverName,receiverAddress,receiverContactNo,senderName,senderContactNo,packageID,date,invoiceNo,invoiceYear,RecievedPhysicalUser,RecievedPhyscialDate,paymentRecievedUser,paymentRecievedDate,receiptNo,receiptYear,dispatchedUser,dispatchedDate")] booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.packageID = new SelectList(db.packages, "packageID", "packageID", booking.packageID);
            ViewBag.scheduleID = new SelectList(db.schedules, "scheduleID", "scheduleID", booking.scheduleID);
            ViewBag.customerID = new SelectList(db.users, "userID", "username", booking.customerID);
            return View(booking);
        }

        // GET: bookings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            booking booking = db.bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            booking booking = db.bookings.Find(id);
            db.bookings.Remove(booking);
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
