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
    public class TransactionDetailsController : Controller
    {
        private MartifyOnlineMartDBContext db = new MartifyOnlineMartDBContext();

        // GET: TransactionDetails
        public ActionResult Index()
        {
            var transactionDetails = db.TransactionDetails.Include(t => t.Bill).Include(t => t.Product);
            return View(transactionDetails.ToList());
        }

        // GET: TransactionDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransactionDetail transactionDetail = db.TransactionDetails.Find(id);
            if (transactionDetail == null)
            {
                return HttpNotFound();
            }
            return View(transactionDetail);
        }

        // GET: TransactionDetails/Create
        public ActionResult Create()
        {
            ViewBag.BillID = new SelectList(db.Bills, "BillID", "BillID");
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName");
            return View();
        }

        // POST: TransactionDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TrasactionID,BillID,ProductID,PruchaseQTY,PurchaseAmout")] TransactionDetail transactionDetail)
        {
            if (ModelState.IsValid)
            {
                db.TransactionDetails.Add(transactionDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BillID = new SelectList(db.Bills, "BillID", "BillID", transactionDetail.BillID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName", transactionDetail.ProductID);
            return View(transactionDetail);
        }

        // GET: TransactionDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransactionDetail transactionDetail = db.TransactionDetails.Find(id);
            if (transactionDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.BillID = new SelectList(db.Bills, "BillID", "BillID", transactionDetail.BillID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName", transactionDetail.ProductID);
            return View(transactionDetail);
        }

        // POST: TransactionDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TrasactionID,BillID,ProductID,PruchaseQTY,PurchaseAmout")] TransactionDetail transactionDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transactionDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BillID = new SelectList(db.Bills, "BillID", "BillID", transactionDetail.BillID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName", transactionDetail.ProductID);
            return View(transactionDetail);
        }

        // GET: TransactionDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransactionDetail transactionDetail = db.TransactionDetails.Find(id);
            if (transactionDetail == null)
            {
                return HttpNotFound();
            }
            return View(transactionDetail);
        }

        // POST: TransactionDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TransactionDetail transactionDetail = db.TransactionDetails.Find(id);
            db.TransactionDetails.Remove(transactionDetail);
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
