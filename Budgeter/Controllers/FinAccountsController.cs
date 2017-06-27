using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Budgeter.Models;

namespace Budgeter.Controllers
{
    public class FinAccountsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: FinAccounts
        public ActionResult Index()
        {
            var accounts = db.FinAccounts.Include(f => f.Household);
            return View(accounts.ToList());
        }

        // GET: FinAccounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FinAccount finAccount = db.FinAccounts.Find(id);
            if (finAccount == null)
            {
                return HttpNotFound();
            }
            return View(finAccount);
        }

        // GET: FinAccounts/Create
        public ActionResult Create()
        {
            ViewBag.HouseholdId = new SelectList(db.Households, "Id", "Name");
            return View();
        }

        // POST: FinAccounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,HouseholdId,Name,Balance,ReconciledBalance,Created,Updated")] FinAccount finAccount)
        {
            if (ModelState.IsValid)
            {
                finAccount.Created = DateTimeOffset.Now;
                db.FinAccounts.Add(finAccount);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.HouseholdId = new SelectList(db.Households, "Id", "Name", finAccount.HouseholdId);
            return View(finAccount);
        }

        // GET: FinAccounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FinAccount finAccount = db.FinAccounts.Find(id);
            if (finAccount == null)
            {
                return HttpNotFound();
            }
            ViewBag.HouseholdId = new SelectList(db.Households, "Id", "Name", finAccount.HouseholdId);
            return View(finAccount);
        }

        // POST: FinAccounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,HouseholdId,Name,Balance,ReconciledBalance,Created,Updated")] FinAccount finAccount)
        {
            if (ModelState.IsValid)
            {
                finAccount.Updated = DateTimeOffset.Now;
                db.Entry(finAccount).State = EntityState.Modified;
                db.Entry(finAccount).Property("Created").IsModified = false;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HouseholdId = new SelectList(db.Households, "Id", "Name", finAccount.HouseholdId);
            return View(finAccount);
        }

        // GET: FinAccounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FinAccount finAccount = db.FinAccounts.Find(id);
            if (finAccount == null)
            {
                return HttpNotFound();
            }
            return View(finAccount);
        }

        // POST: FinAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FinAccount finAccount = db.FinAccounts.Find(id);
            db.FinAccounts.Remove(finAccount);
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
