using Budgeter.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DotNet.Highcharts;
using Microsoft.AspNet.Identity;
using System.ComponentModel.DataAnnotations;
using Budgeter.Helpers;

namespace Budgeter.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(int? number)
        {
            var userId = User.Identity.GetUserId();

            var accounts = new List<FinAccount>();
            var transactions = new List<Transaction>();
            var budgets = new List<Budget>();
            var transactiontypes = new List<TransactionType>();
            var categories = new List<Category>();

            transactions = db.Transactions.ToList();

            var model = new DashboardViewModel()
            {
                FinAccounts = accounts,
                Transactions = transactions,
                Categories = categories,
                Budgets = budgets

            };

            // Start: Panel Counts
            ViewBag.AccountsTotal = db.FinAccounts.Sum(x => (double?)x.Balance) ?? 0;
            ViewBag.DebitsTotal = db.Transactions.Where(t => t.TransactionTypeId == 1).Sum(x => (double?)x.Amount) ?? 0;
            ViewBag.CreditsTotal = db.Transactions.Where(t => t.TransactionTypeId == 2).Sum(x => (double?)x.Amount) ?? 0;
            // End: Panel Counts

            // Start: Actual Expenditure Counts
            ViewBag.Food = db.Transactions.Where(t => t.CategoryId == 7).Sum(x => (double?)x.Amount) ?? 0;
            ViewBag.Housing = db.Transactions.Where(t => t.CategoryId == 2).Sum(x => (double?)x.Amount) ?? 0;
            ViewBag.Emergency = db.Transactions.Where(t => t.CategoryId == 1).Sum(x => (double?)x.Amount) ?? 0;
            ViewBag.Utilities = db.Transactions.Where(t => t.CategoryId == 4).Sum(x => (double?)x.Amount) ?? 0;
            ViewBag.Auto = db.Transactions.Where(t => t.CategoryId == 10).Sum(x => (double?)x.Amount) ?? 0;
            ViewBag.Debt = db.Transactions.Where(t => t.CategoryId == 6).Sum(x => (double?)x.Amount) ?? 0;
            ViewBag.Insurance = db.Transactions.Where(t => t.CategoryId == 11).Sum(x => (double?)x.Amount) ?? 0;
            ViewBag.Misc = db.Transactions.Where(t => t.CategoryId == 12).Sum(x => (double?)x.Amount) ?? 0;
            ViewBag.Tax = db.Transactions.Where(t => t.CategoryId == 14).Sum(x => (double?)x.Amount) ?? 0;
            // End: Actual Expenditure Counts

            // Start: Income Counts
            ViewBag.Income = DashboardHelper.FiscalYear(1).ToArray();
            // End: Income Counts

            // Start: Budgeted Counts
            ViewBag.FoodB = db.Budgets.Where(t => t.CategoryId == 7).Sum(x => (double?)x.Amount) ?? 0;
            ViewBag.HousingB = db.Budgets.Where(t => t.CategoryId == 2).Sum(x => (double?)x.Amount) ?? 0;
            ViewBag.EmergencyB = db.Budgets.Where(t => t.CategoryId == 1).Sum(x => (double?)x.Amount) ?? 0;
            ViewBag.UtilitiesB = db.Budgets.Where(t => t.CategoryId == 4).Sum(x => (double?)x.Amount) ?? 0;
            ViewBag.AutoB = db.Budgets.Where(t => t.CategoryId == 10).Sum(x => (double?)x.Amount) ?? 0;
            ViewBag.DebtB = db.Budgets.Where(t => t.CategoryId == 6).Sum(x => (double?)x.Amount) ?? 0;
            ViewBag.InsuranceB = db.Budgets.Where(t => t.CategoryId == 11).Sum(x => (double?)x.Amount) ?? 0;
            ViewBag.MiscB = db.Budgets.Where(t => t.CategoryId == 12).Sum(x => (double?)x.Amount) ?? 0;
            ViewBag.TaxB = db.Budgets.Where(t => t.CategoryId == 14).Sum(x => (double?)x.Amount) ?? 0;
            // End: Budgeted Counts

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            EmailModel model = new EmailModel();

            return View(model);
        }
        public ActionResult Sent()
        {
            // ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact(EmailModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var from = "MoneyTree<jacobcrawford1990@gmail.com>";

                    var body = "<p>Email From: <strong>{0}</strong> ({1})</p><p>Message:</p><p>{2}</p>";

                    //model.Body = "This is a message from your portfolio site. The name and the email of the contacting person is above.";
                    var subject = "Money Tree - ";

                    var email = new MailMessage(from, ConfigurationManager.AppSettings["emailto"])
                    { Subject = subject + model.Subject, Body = string.Format(body, model.FromName, model.FromEmail, model.Body), IsBodyHtml = true };

                    var svc = new PersonalEmail(); await svc.SendAsync(email);

                    return RedirectToAction("Sent");
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); await Task.FromResult(0); }
            }
            return View(model);
        }
    }
}