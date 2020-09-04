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
    public class LoginTypesController : Controller
    {
        private MartifyOnlineMartDBContext db = new MartifyOnlineMartDBContext();

        // GET: LoginTypes
        public ActionResult Index()
        {
            return View(db.LoginTypes.ToList());
        }

        // GET: LoginTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoginType loginType = db.LoginTypes.Find(id);
            if (loginType == null)
            {
                return HttpNotFound();
            }
            return View(loginType);
        }

        // GET: LoginTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoginTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LoginTypeID,LoginTypeName")] LoginType loginType)
        {
            if (ModelState.IsValid)
            {
                db.LoginTypes.Add(loginType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(loginType);
        }

        // GET: LoginTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoginType loginType = db.LoginTypes.Find(id);
            if (loginType == null)
            {
                return HttpNotFound();
            }
            return View(loginType);
        }

        // POST: LoginTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LoginTypeID,LoginTypeName")] LoginType loginType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loginType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loginType);
        }

        // GET: LoginTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoginType loginType = db.LoginTypes.Find(id);
            if (loginType == null)
            {
                return HttpNotFound();
            }
            return View(loginType);
        }

        // POST: LoginTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LoginType loginType = db.LoginTypes.Find(id);
            db.LoginTypes.Remove(loginType);
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
