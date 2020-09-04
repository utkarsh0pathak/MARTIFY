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
    public class CustomersController : Controller
    {
        private MartifyOnlineMartDBContext db = new MartifyOnlineMartDBContext();


        public ActionResult ShowOrderedDetails()
        {
            int CustomerId = Convert.ToInt32(Session["CustomerId"]);
            var transactionDetails = db.TransactionDetails.Where(t => t.Bill.CustomerID == CustomerId).Include(t => t.Bill).Include(t => t.Product);

            Bill bill = db.Bills.FirstOrDefault(b => b.CustomerID == CustomerId && b.TotalAmount!= 0);
            ViewBag.BillNo = bill.BillID;
            ViewBag.BillDate = bill.BillDate;
            ViewBag.BillIPaymentMode = bill.PaymentMode;
            ViewBag.BillTotalAmount = bill.TotalAmount;
            return View(transactionDetails.ToList());


        }
        public ActionResult CartCheckOut()
        {
            int CustomerId = Convert.ToInt32(Session["CustomerId"]);
            Bill bill = new Bill();
            Cart dcart;
            TransactionDetail transactionDetail;
            var carts = db.Carts.Where(c => c.Customer.CustomerID == CustomerId).ToList();

            if (carts.Count()>0 )
            {
                
                bill.CustomerID = CustomerId;
                bill.PaymentModeID = 1;
                bill.BillDate = DateTime.Now;
                db.Bills.Add(bill);
                db.SaveChanges();

                bill = db.Bills.FirstOrDefault(b => b.CustomerID == CustomerId && b.TotalAmount <= 0);
                foreach (Cart cart in carts)
                {
                    Product product = db.Products.FirstOrDefault(p => p.ProductQTY >= cart.PurchaseQTY && p.ProductID == cart.ProductID);
                    if (product!=null)
                    {
                        product.ProductQTY = product.ProductQTY - cart.PurchaseQTY;

                        db.Entry(product).State = EntityState.Modified;
                        db.SaveChanges();

                        transactionDetail = new TransactionDetail();
                        transactionDetail.BillID = bill.BillID;
                        transactionDetail.ProductID = product.ProductID;
                        transactionDetail.PruchaseQTY = cart.PurchaseQTY;
                        transactionDetail.PurchaseAmout = cart.PurchaseQTY * product.Price;
                        db.TransactionDetails.Add(transactionDetail);
                        db.SaveChanges();


                        dcart = db.Carts.Find(cart.CartID);
                        db.Carts.Remove(cart);
                        db.SaveChanges();

                    }

                    


                }


            }


            carts = db.Carts.Where(c => c.Customer.CustomerID == CustomerId).ToList();
            if (carts.Count() > 0)
            {
                TempData["CartStatus"]= "Some are not avaiable in stock";
                
            }
            
            if (db.TransactionDetails.Where(t => t.BillID == bill.BillID).Count() > 0)
            {
                double BillTotalAmount = db.TransactionDetails.Where(t => t.BillID == bill.BillID).Select(t => t.PurchaseAmout).Sum();

                bill = db.Bills.Find(bill.BillID);
                bill.TotalAmount = BillTotalAmount;

                db.Entry(bill).State = EntityState.Modified;
                db.SaveChanges();

            }


            return RedirectToAction("ShowCart");
        }

        public ActionResult ItemDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cart cart = db.Carts.Find(id);
            if (cart == null)
            {
                return HttpNotFound();
            }
            return View(cart);
        }

        // POST: Carts/Delete/5
        [HttpPost, ActionName("ItemDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult ItemDeleteConfirmed(int id)
        {
            Cart cart = db.Carts.Find(id);
            db.Carts.Remove(cart);
            db.SaveChanges();
            return RedirectToAction("ShowCart");
        }



        public ActionResult SignOut()
        {
            Session.Abandon();

            return RedirectToAction("Login", "Customers");
        }
        public ActionResult Home()
        {

            int id = Convert.ToInt32(Session["CustomerId"]);
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            return RedirectToAction("Details/" + customer.CustomerID, "Customers");


        }
    
        public ActionResult ShowOrderDetails(int? id)
        {
            Cart cart = db.Carts.FirstOrDefault(c => c.CartID == id);
            Product product = db.Products.FirstOrDefault(p => p.ProductID == cart.ProductID);
            ViewBag.OrderQty = cart.PurchaseQTY;

            return View(product);
        }

        public ActionResult ShowCart()
        {
            int CustomerId = Convert.ToInt32(Session["CustomerId"]);
            var carts = db.Carts.Where(c => c.Customer.CustomerID == CustomerId).Include(c => c.Customer).Include(c => c.Product);
            return View(carts.ToList());
        }

        // GET: Customers
        public ActionResult Index()
        {
            var customers = db.Customers.Include(c => c.LoginType);
            return View(customers.ToList());
        }

        [HttpGet]
        public ActionResult ProductList()
        {
            var products = db.Products.Include(p => p.Category);
            return View(products.ToList());
        }


        
        public ActionResult AddtoCart(int? id)
        {
            int CustomerId = Convert.ToInt32(Session["CustomerId"]);
            ViewBag.CustomerID = new SelectList(db.Customers.Where(c => c.CustomerID == CustomerId).ToList(), "CustomerID", "CustomerName");
            ViewBag.ProductID = new SelectList(db.Products.Where(p => p.ProductID == id).ToList(), "ProductID", "ProductName");

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddtoCart([Bind(Include = "CartID,CustomerID,ProductID,PurchaseQTY")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                db.Carts.Add(cart);
                db.SaveChanges();
                return RedirectToAction("ProductList","Customers");
            }

            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerName", cart.CustomerID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName", cart.ProductID);
            return View(cart);
        }


        [HttpGet]
        public ActionResult Login()
        {
            ViewBag.LoginTypeID = new SelectList(db.LoginTypes, "LoginTypeID", "LoginTypeName");

            return View();
        }
        [HttpPost]
        public ActionResult Login(Customer customer)
        {

            Customer Cust = db.Customers.FirstOrDefault(e => e.Password == customer.Password && e.Email == customer.Email && e.LoginTypeID == customer.LoginTypeID);

            if (Cust != null && Cust.LoginTypeID == 1)
            {
                Session["VendorId"] = Cust.CustomerID;

                return RedirectToAction("Details/" + Cust.CustomerID, "ManageVendor");

            }
            else if (Cust != null && Cust.LoginTypeID == 2)
            {
                //int ? id = mPLOYEE.EMPLOYEEID;

                Session["CustomerId"] = Cust.CustomerID;
                return RedirectToAction("Details/" + Cust.CustomerID, "Customers");
            }
            else if (customer.Email == "admin@admin.com" && customer.Password == "admin@admin.com")
            {
                Session["Admin"] = "Admin";
                return RedirectToAction("Index", "Categories");
            }
            else
            {
                ViewBag.Status = "Invalid user or password";
                return RedirectToAction("Login", "Customers");


            }

        }
        [HttpGet]
        public ActionResult AfterLogin(Customer customer)
        {
            if (Session["CustomerId"] != null)
            {


                if (customer == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Customer Cust = db.Customers.Find(customer.CustomerID);
                if (Cust == null)
                {
                    return HttpNotFound();
                }
                return View(Cust);
            }
            else
            {
                ViewBag.LoginTypeID = new SelectList(db.LoginTypes, "LoginTypeID", "LoginTypeName");

                return RedirectToAction("Login");

            }


        }

        // GET: Customers/Details/5
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

        // GET: Customers/Create
        public ActionResult Create()
        {
            ViewBag.LoginTypeID = new SelectList(db.LoginTypes, "LoginTypeID", "LoginTypeName");
            return View();
        }

        // POST: Customers/Create
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
                return RedirectToAction("Login");
            }

            ViewBag.LoginTypeID = new SelectList(db.LoginTypes, "LoginTypeID", "LoginTypeName", customer.LoginTypeID);
            return View(customer);
        }

        // GET: Customers/Edit/5
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
            ViewBag.LoginTypeID = new SelectList(db.LoginTypes.Where(l => l.LoginTypeID == 2).ToList(), "LoginTypeID", "LoginTypeName", customer.LoginTypeID);
            return View(customer);
        }

        // POST: Customers/Edit/5
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
                return RedirectToAction("Index");
            }

            ViewBag.LoginTypeID = new SelectList(db.LoginTypes.Where(l => l.LoginTypeID == 2).ToList(), "LoginTypeID", "LoginTypeName", customer.LoginTypeID);
            return View(customer);
        }

        // GET: Customers/Delete/5
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

        // POST: Customers/Delete/5
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
