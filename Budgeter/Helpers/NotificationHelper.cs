using Budgeter.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace Budgeter.Helpers
{
    public class NotificationHelper
    {
        private static ApplicationDbContext db = new ApplicationDbContext();

        public async static Task OverdraftNotification(Transaction transaction)
        {
            //var budget = db.Budgets.Find(transaction.BudgetId);

            var account = db.FinAccounts.Find(transaction.AccountId);
            var HouseholdEmail = db.Users.Where(x => x.HouseholdId == account.HouseholdId).ToList();

            foreach (var user in HouseholdEmail)
            {
                var myEmail = new EmailModel();
                myEmail.FromEmail = WebConfigurationManager.AppSettings["emailfrom"];
                myEmail.ToEmail = user.Email;
                myEmail.Subject = string.Format("Your account has been overdrafted! Please log into Money Tree to review.");

                var emailBody = new StringBuilder();
                emailBody.AppendFormat("Account Name: {0}", account.Name);
                emailBody.AppendLine();

                myEmail.Body = emailBody.ToString();

                await Email(myEmail);

                var myNot = new BalanceNotification();
                myNot.HouseholdId = user.HouseholdId;
                myNot.Type = "Overdraft";
                myNot.UserId = user.Id;
                myNot.Read = false;
                db.BalanceNotifications.Add(myNot);
                db.SaveChanges();
            }


        }

        //public async static Task WarningNotification(Transaction transaction)
        //{
        //    //var budget = db.Budgets.Find(transaction.BudgetId);

        //    var account = db.FinAccounts.Find(transaction.AccountId);
        //    var HouseholdEmail = db.Users.Where(x => x.HouseholdId == account.HouseholdId).ToList();

        //    foreach (var user in HouseholdEmail)
        //    {
        //        var myEmail = new EmailModel();
        //        myEmail.FromEmail = WebConfigurationManager.AppSettings["emailfrom"];
        //        myEmail.ToEmail = user.Email;
        //        myEmail.Subject = string.Format("Your Account is at warning amount or lower current amount is: {0}", transaction.CurrentbnkAmount);

        //        var emailBody = new StringBuilder();
        //        emailBody.AppendFormat("<b> Account Name </b> {0}", account.Name);
        //        emailBody.AppendLine();
        //        myEmail.Body = emailBody.ToString();

        //        await Email(myEmail);

        //        var myNot = new BalanceNotification();
        //        myNot.HouseholdId = user.HouseholdId;
        //        myNot.Type = "Warning Amount";
        //        myNot.UserId = user.Id;
        //        myNot.Read = false;
        //        db.BalanceNotification.Add(myNot);
        //        db.SaveChanges();
        //    }


        //}


        public async static Task Email(EmailModel email)
        {
            var emails = new MailMessage("donotreply@moneytree.com", email.ToEmail, email.Subject, email.Body);
            var svc = new PersonalEmail();
            await svc.SendAsync(emails);
        }
    }
}