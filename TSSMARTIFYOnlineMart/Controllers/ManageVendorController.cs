using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TSSMARTIFYOnlineMart.Models;

namespace TSSMARTIFYOnlineMart.Controllers
{
    public class ManageVendorController : Controller
    {
        private MartifyOnlineMartDBContext db = new MartifyOnlineMartDBContext();

        // GET: ManageVendor
        public ActionResult Index()
        {
            var customers = db.Customers.Include(c => c.LoginType);
            return View(customers.ToList());
        }

        // GET: ManageVendor/Details/5
        public ActionResult Home()
        {

                    

            int id = Convert.ToInt32(Session["VendorId"]);
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            return RedirectToAction("Details/" + customer.CustomerID, "ManageVendor");


        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: ManageVendor/Create
        public ActionResult Create()
        {
            ViewBag.LoginTypeID = new SelectList(db.LoginTypes, "LoginTypeID", "LoginTypeName");
            return View();
        }

        // POST: ManageVendor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerID,CustomerName,LoginTypeID,Email,Password,Contact,Address,City,PinCode")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LoginTypeID = new SelectList(db.LoginTypes, "LoginTypeID", "LoginTypeName", customer.LoginTypeID);
            return View(customer);
        }

        // GET: ManageVendor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            ViewBag.LoginTypeID = new SelectList(db.LoginTypes.Where(l => l.LoginTypeID == 1).ToList(), "LoginTypeID", "LoginTypeName", customer.LoginTypeID);
            return View(customer);
        }

        // POST: ManageVendor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerID,CustomerName,LoginTypeID,Email,Password,Contact,Address,City,PinCode")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details/" + customer.CustomerID, "ManageVendor");

            }
            ViewBag.LoginTypeID = new SelectList(db.LoginTypes.Where(l => l.LoginTypeID == 1).ToList(), "LoginTypeID", "LoginTypeName", customer.LoginTypeID);
            return View(customer);
        }

        // GET: ManageVendor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: ManageVendor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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
