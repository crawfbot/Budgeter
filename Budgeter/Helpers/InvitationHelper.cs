using Budgeter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace Budgeter.Helpers
{
    public class InvitationHelper
    {
        private static ApplicationDbContext db = new ApplicationDbContext();

        public async Task Registered(Invitation user, string link)
        {
            var House = db.Households.Find(user.HouseholdId);
            if (!string.IsNullOrEmpty(user.Email))
            {
                var myEmail = new EmailModel();
                myEmail.FromEmail = WebConfigurationManager.AppSettings["emailfrom"];
                myEmail.ToEmail = user.Email;
                myEmail.Subject = string.Format("Money Tree - You Have Been Invited to Join a Household!");

                var emailBody = new StringBuilder();
                emailBody.AppendFormat("Household Name: {0}", House.Name);
                emailBody.AppendLine();
                emailBody.AppendLine();
                emailBody.AppendLine("Please click the following link to join the above household:" + link + "");

                myEmail.Body = emailBody.ToString();

                await Email(myEmail);
            }
        }
        public async Task Unregistered(Invitation user, string link)
        {
            var House = db.Households.Find(user.HouseholdId);
            if (!string.IsNullOrEmpty(user.Email))
            {
                var myEmail = new EmailModel();
                myEmail.FromEmail = WebConfigurationManager.AppSettings["emailfrom"];
                myEmail.ToEmail = user.Email;
                myEmail.Subject = string.Format("Money Tree - You Have Been Invited to Join a Household!");

                var emailBody = new StringBuilder();
                emailBody.AppendFormat("Household Name: {0}", House.Name);
                emailBody.AppendLine();
                emailBody.AppendLine();
                emailBody.AppendLine("Please click the following link to join the above household: " + link + "");

                myEmail.Body = emailBody.ToString();

                await Email(myEmail);
            }
        }
        public async Task Email(EmailModel email)
        {
            var emails = new MailMessage("donotreply@MoneyTree.com", email.ToEmail, email.Subject, email.Body);
            var svc = new PersonalEmail();
            await svc.SendAsync(emails);
        }
    }
}