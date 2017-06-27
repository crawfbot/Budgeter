using Budgeter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Budgeter.Helpers
{
    public static class TransactionHelper
    {
        private static ApplicationDbContext db = new ApplicationDbContext();

        // updates the account.Balance based on the transaction.Category.Type
        // used in Transactions/Create and Transactions/Edit ActionResults
        public static void UpdateAccountBalance(this Transaction transaction, string userId)
        {
            var user = db.Users.FirstOrDefault(u => u.Id.Equals(userId));
            var userHHID = Convert.ToInt32(user.HouseholdId);
            var account = db.FinAccounts.FirstOrDefault(a => a.Id == transaction.AccountId);

            if (transaction.TransactionType.Name == "Debit")
            {
                account.Balance -= transaction.Amount; // SUBTRACT
            }
            else
            {
                account.Balance += transaction.Amount; // ADD
            }

            db.SaveChanges();
        }

        // updates (reverts) the account.Balance based on the transaction.Category.Type
        // used in Transactions/Edit and Transactions/Delete ActionResults
        public static void ReverseAccountBalance(this Transaction transaction, string userId)
        {
            var user = db.Users.FirstOrDefault(u => u.Id.Equals(userId));
            var userHHID = Convert.ToInt32(user.HouseholdId);
            var account = db.FinAccounts.FirstOrDefault(a => a.Id == transaction.AccountId);

            if (transaction.TransactionType.Name == "Debit")
            {
                account.Balance += transaction.Amount; // ADD BACK
            }
            else
            {
                account.Balance -= transaction.Amount; // SUBTRACT BACK
            }

            db.SaveChanges();
        }

        public static decimal GetBalance(this decimal amount, string type)
        {
            decimal Balance = 0;
            if (type == "Debit")
            {
                Balance -= amount;
            }
            else
            {
                Balance += amount;
            }
            return Balance;
        }
    }
}