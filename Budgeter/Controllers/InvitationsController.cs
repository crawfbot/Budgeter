using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Budgeter.Models;
using Budgeter.UniqueKey;
using Budgeter.Helpers;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;

namespace Budgeter.Controllers
{
    public class InvitationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Invitations
        public ActionResult Index()
        {
            //var invitations = db.Invitations.Include(i => i.Household);
            //return View(invitations.ToList());

            return View(db.Invitations.ToList());
        }

        // GET: Invitations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invitation invitation = db.Invitations.Find(id);
            if (invitation == null)
            {
                return HttpNotFound();
            }
            return View(invitation);
        }

        // GET: Invitations/Create
        public ActionResult Create(int id)
        {
            //ViewBag.HouseholdId = new SelectList(db.Households, "Id", "Name");
            //return View();

            var invitation = new Invitation();
            invitation.HouseholdId = id;
            return View(invitation);
        }

        // POST: Invitations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> Create([Bind(Include = "Id,HouseholdId,InviteeName,Email,HasAdminRights")] Invitation invitation)
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            var IH = new InvitationHelper();

            if (ModelState.IsValid)
            {
                var code = KeyGenerator.GetUniqueKey(8);

                //Check this url
                var callbackUrl = Url.Action("Index", "Home");
                if (db.Users.Any(x => x.Email == invitation.Email))
                {
                    //Check this url
                    callbackUrl = Url.Action("JoinRegistered", "Invitations", new { code = code }, protocol: Request.Url.Scheme);
                }
                else
                {
                    //Check this url
                    callbackUrl = Url.Action("RegisterHousehold", "Account", new { inccode = code }, protocol: Request.Url.Scheme);
                }
                invitation.InvitationCode = code;
                invitation.InvitedBy = user.Id;
                invitation.InvitedDate = DateTimeOffset.Now;
                db.Invitations.Add(invitation);
                db.SaveChanges();
                if (db.Users.Any(x => x.Email == invitation.Email))
                {
                    await IH.Registered(invitation, callbackUrl);
                }
                else
                {
                    await IH.Unregistered(invitation, callbackUrl);
                }
                //Check this url
                return RedirectToAction("Index", "Home");
            }

            
            return View(invitation);
        }

        // GET
        public ActionResult JoinRegistered(string code)
        {
            if (db.Invitations.Any(x => x.InvitationCode == code))
            {
                var invitedUser = db.Invitations.First(x => x.InvitationCode == code);
                var users = db.Users.First(x => x.Email == invitedUser.Email);
                users.HouseholdId = invitedUser.HouseholdId;
                db.SaveChanges();
            }

            return RedirectToAction("Index", "Home");
        }

        // GET: Invitations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invitation invitation = db.Invitations.Find(id);
            if (invitation == null)
            {
                return HttpNotFound();
            }
            ViewBag.HouseholdId = new SelectList(db.Households, "Id", "Name", invitation.HouseholdId);
            return View(invitation);
        }

        // POST: Invitations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,HouseholdId,InviteeName,Email,InvitationCode,InvitedBy,InvitedDate,HasAdminRights,Accepted")] Invitation invitation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invitation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HouseholdId = new SelectList(db.Households, "Id", "Name", invitation.HouseholdId);
            return View(invitation);
        }

        // GET: Invitations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invitation invitation = db.Invitations.Find(id);
            if (invitation == null)
            {
                return HttpNotFound();
            }
            return View(invitation);
        }

        // POST: Invitations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Invitation invitation = db.Invitations.Find(id);
            db.Invitations.Remove(invitation);
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
