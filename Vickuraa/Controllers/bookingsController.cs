using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Vickuraa.Dto;
using Vickuraa.Models;

namespace Vickuraa.Controllers
{
    public class bookingsController : Controller
    {
        private vicukraaModel db = new vicukraaModel();


        public ActionResult RecievePackage(int Id)
        {

            booking booking = db.bookings.Find(Id);
            ViewBag.PackageDesc = booking.package.packageDesc;
            ViewBag.receiverName = booking.receiverName;
            ViewBag.RecieverAddress = booking.receiverAddress;
            ViewBag.senderName = booking.senderName;
            return View(booking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RecievePackage(int BookingId, string senderName)
        {
            if (ModelState.IsValid)
            {
                var b = db.bookings.Where(a => a.bookingID == BookingId).FirstOrDefault();
                b.RecievedPhysicalUser = 4;
                b.RecievedPhyscialDate = DateTime.Now;
                b.paymentRecievedDate = DateTime.Now;
                b.paymentRecievedUser = 4;
                var ro = db.bookings.Where(a => a.receiptYear == DateTime.Now.Year).OrderByDescending(x => x.receiptNo).FirstOrDefault();
                if (ro.receiptNo == null)
                {
                    b.receiptNo = 0 + 1;
                }

                else
                {
                    b.receiptNo = ro.receiptNo + 1;
                }
               
                b.receiptYear = DateTime.Now.Year;
                db.Entry(b).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("PaidBookingList");
            }
            return View();
        }



        public ActionResult DispatchPackage(int Id)
        {

            booking booking = db.bookings.Find(Id);
            ViewBag.PackageDesc = booking.package.packageDesc;
            ViewBag.receiverName = booking.receiverName;
            ViewBag.RecieverAddress = booking.receiverAddress;
            ViewBag.senderName = booking.senderName;
            return View(booking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DispatchPackage(int BookingId, string senderName)
        {
            if (ModelState.IsValid)
            {
                var b = db.bookings.Where(a => a.bookingID == BookingId).FirstOrDefault();
                b.dispatchedDate = DateTime.Now;
                b.dispatchedUser = 4;              
                db.Entry(b).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("PaidBookingList");
            }
            return View();
        }


        [HttpGet]
        public ActionResult PackageTimeLine(int Id)
        {

            var booking = db.bookings.Where(a => a.package.vesselID == Id).OrderByDescending(x => x.bookingID).FirstOrDefault();
            ViewBag.RequestedDate = booking.date;
            ViewBag.BoardedDate = booking.RecievedPhyscialDate;
            ViewBag.RecievedUser = booking.RecievedPhysicalUser;
            ViewBag.PaymentRecievedUser = booking.paymentRecievedUser;
            ViewBag.PaymentRecievedDate = booking.paymentRecievedDate;
            ViewBag.DispatchedDate = booking.dispatchedDate;
            ViewBag.DispatchedUser = booking.dispatchedUser;
            return View(booking);
        }

        // POST: bookings/Search
        [HttpGet, ActionName("Search")]
        public ActionResult Search()
        {
            List<destination> dest_list = db.destinations.ToList();
            ViewBag.destinations = new SelectList(db.destinations, "destinationID", "destinationName");
            return View("Search");
        }

        // POST: bookings/Search
        [HttpPost]
        public ActionResult searchconfirm(int fromID, int toID, DateTime date)
        {
            int routeid = 0;
            foreach (route rt in db.routes.ToList())
            {
                if (rt.departureportID == fromID)
                {
                    Debug.WriteLine("if");
                    if (rt.arrivalportID == toID)
                    {
                        Debug.WriteLine("double if");
                        routeid = rt.routeID;
                    }
                }
            }
            ViewBag.date = date.Date;
            ViewBag.From = db.destinations.Find(fromID).destinationName;
            ViewBag.To = db.destinations.Find(toID).destinationName;
            List<schedule> schedulelist = new List<schedule>();
            foreach (schedule sc in db.schedules.ToList())
            {
                if (sc.estimateDeparturetime.Date == date)
                {
                    // if(sc.routeID == routeid)
                    //  {
                    schedulelist.Add(sc);
                    // }
                }
            }
            ViewBag.schedulelist = schedulelist;
            ViewBag.Packages = db.packages.ToList();
            return View("SearchResult");
        }

        [HttpGet]
        public ActionResult viewDetails(int vesselids)
        {
            List<package> plist = new List<package>();
            foreach (package p in db.packages.ToList())
            {
                if (p.vesselID == vesselids)
                {
                    plist.Add(p);
                }
            }
            ViewBag.packages = plist;
            ViewBag.vessel = vesselids;
            return View("detailPackage");
        }

        [HttpPost]
        public ActionResult bookingCreate(int vessel)
        {

            ViewBag.vessel = vessel;
            return View("Create");
        }


        // GET: bookings
        public ActionResult PendingBookingList()
        {
            var bookings = db.bookings.Include(b => b.package).Include(b => b.schedule).Include(b => b.user)
                .Where(a => a.RecievedPhyscialDate == null);
            return View(bookings.ToList());
        }


        public ActionResult PaidBookingList()
        {
            var bookings = db.bookings.Include(b => b.package).Include(b => b.schedule).Include(b => b.user)
                .Where(a => a.paymentRecievedDate != null && a.dispatchedDate  == null);
            return View(bookings.ToList());
        }
        // GET: bookings/Details/5
        public ActionResult BookingDetails(int? id)
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


        public ActionResult SendPackage(int Id)
        {
            ViewBag.scheduleID = new SelectList(db.schedules, "scheduleID", "scheduleID");
            ViewBag.customerID = new SelectList(db.users, "userID", "username");
            ViewBag.packageTypeId = new SelectList(db.packageTypes, "packageTypeId", "packageType1");
            ViewBag.VesselId = Id;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendPackage(PackageBooking packageBooking, int VesselId)
        {
            if (ModelState.IsValid)
            {
                var invoice = db.bookings.Where(a => a.invoiceYear == DateTime.Now.Year).OrderByDescending(c => c.invoiceNo).FirstOrDefault();
                var schedule = db.schedules.Where(a => a.vesselID == VesselId).OrderByDescending(c => c.scheduleID).FirstOrDefault();

                var package = new package();
                package.packageTypeId = packageBooking.packageTypeId;
                package.vesselID = VesselId;
                package.volume = packageBooking.volume;
                package.price = packageBooking.price;
                package.enteredDate = DateTime.Now;
                package.enteredUser = 1;
                db.packages.Add(package);
                {
                    var booking = new booking();
                    booking.customerID = 1;
                    booking.scheduleID = schedule.scheduleID;
                    booking.packageID = packageBooking.packageID;
                    booking.quantity = packageBooking.volume;
                    booking.receiverName = packageBooking.receiverName;
                    booking.receiverAddress = packageBooking.receiverAddress;
                    booking.receiverContactNo = packageBooking.receiverContactNo;
                    booking.receiverName = packageBooking.receiverName;
                    booking.receiverAddress = packageBooking.receiverAddress;
                    booking.receiverContactNo = packageBooking.receiverContactNo;
                    booking.senderContactNo = packageBooking.senderContactNo;
                    booking.senderName = packageBooking.senderName;
                    booking.date = DateTime.Now;
                    if (invoice == null)
                    {
                        booking.invoiceNo = 1;
                    }
                    else
                    {
                        booking.invoiceNo = invoice.invoiceNo + 1;

                    }
                    booking.invoiceYear = DateTime.Now.Year;
                    db.bookings.Add(booking);

                    db.SaveChanges();
                    return RedirectToAction("BookingList");
                }



            }
            return View(packageBooking);
        }

        public ActionResult EditBooking(int? id)
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBooking([Bind(Include = "bookingID,scheduleID,customerID,quantity,receiverName,receiverAddress,receiverContactNo,senderName,senderContactNo,packageID,date,invoiceNo,invoiceYear,RecievedPhysicalUser,RecievedPhyscialDate,paymentRecievedUser,paymentRecievedDate,receiptNo,receiptYear,dispatchedUser,dispatchedDate")] booking booking)
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
