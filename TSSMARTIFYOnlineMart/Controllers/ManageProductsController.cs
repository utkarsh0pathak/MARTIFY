using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.IO;
using TSSMARTIFYOnlineMart.Models;

namespace TSSMARTIFYOnlineMart.Controllers
{
    public class ManageProductsController : Controller
    {
        public static string fileName;
        private MartifyOnlineMartDBContext db = new MartifyOnlineMartDBContext();

        // GET: ManageProducts
        public ActionResult Index()
        {
             int VendorId = Convert.ToInt32(Session["VendorId"]);
            var products = db.Products.Where(c => c.Customer.CustomerID== VendorId).Include(p => p.Category ).Include(p => p.Customer);
            return View(products.ToList());
        }

        // GET: ManageProducts/Details/5

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }


        public ActionResult EditUploadFile(HttpPostedFileBase file, int id)
        {
            Product product = db.Products.Find(id);

            try
            {
                if (file.ContentLength > 0)
                {
                    string StrExt = Path.GetExtension(file.FileName);
                    fileName = "PROPIC" + DateTime.Now.ToString("ddMMyyyyHHmmss") + StrExt; // Path.GetFileName(file.FileName);
                    file.SaveAs(Server.MapPath("~/Upload/") + fileName);

                }
                ViewBag.Message = "File upload successfully!!";
                ViewBag.Path = String.Format("/Upload/{0}", fileName.Replace('+', '_'));

                product.ProductImage = String.Format("/Upload/{0}", fileName.Replace('+', '_'));
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();

                //ViewBag.Path = String.Format("/Upload/{0}", fileName.Replace('+', '_'));

                //ViewBag.PicUrl =  "~/Upload/" + fileName;
                int VendorId = Convert.ToInt32(Session["VendorId"]);
                ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName");
                ViewBag.CustomerID = new SelectList(db.Customers.Where(c => c.CustomerID == VendorId).ToList(), "CustomerID", "CustomerName");

                return RedirectToAction("Edit/" + product.ProductID);
            }
            catch (Exception)
            {
                ViewBag.Message = "File upload failed!";
                return RedirectToAction("Edit/" + product.ProductID);

            }
        }
        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    string StrExt = Path.GetExtension(file.FileName);
                    fileName = "PROPIC" + DateTime.Now.ToString("ddMMyyyyHHmmss") + StrExt; // Path.GetFileName(file.FileName);
                    file.SaveAs(Server.MapPath("~/Upload/") + fileName);

                }
                ViewBag.Message = "File upload successfully!!";
                //Server.MapPath("~") + @"Content\Upload\"+ fileName;

                ViewBag.Path = String.Format("/Upload/{0}", fileName.Replace('+', '_'));
                //ViewBag.PicUrl =  "~/Upload/" + fileName;
                int VendorId = Convert.ToInt32(Session["VendorId"]);
                ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName");
                ViewBag.CustomerID = new SelectList(db.Customers.Where(c => c.CustomerID == VendorId).ToList(), "CustomerID", "CustomerName");
                return View("Create");
            }
            catch (Exception)
            {
                ViewBag.Message = "File upload failed!";
                return View("Create");

            }
        }

        // GET: ManageProducts/Create
        public ActionResult Create()
        {
            int VendorId = Convert.ToInt32(Session["VendorId"]);
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName");
            ViewBag.CustomerID = new SelectList(db.Customers.Where(c => c.CustomerID == VendorId).ToList(), "CustomerID", "CustomerName");
            return View();
        }

        // POST: ManageProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,CustomerID,ProductName,Description,ProductQTY,CategoryID,Price,ProductImage")] Product product)
        {
            int VendorId = Convert.ToInt32(Session["VendorId"]);
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
            ViewBag.CustomerID = new SelectList(db.Customers.Where(c => c.CustomerID == VendorId).ToList(), "CustomerID", "CustomerName", product.CustomerID);

            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
           return View(product);
        }

        // GET: ManageProducts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            ViewBag.Path = product.ProductImage;
            if (product == null)
            {
                return HttpNotFound();
            }
            int VendorId = Convert.ToInt32(Session["VendorId"]);
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
            ViewBag.CustomerID = new SelectList(db.Customers.Where(c => c.CustomerID == VendorId).ToList(), "CustomerID", "CustomerName", product.CustomerID);
            return View(product);
        }

        // POST: ManageProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,CustomerID,ProductName,Description,ProductQTY,CategoryID,Price,ProductImage")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerName", product.CustomerID);
            return View(product);
        }

        // GET: ManageProducts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: ManageProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
