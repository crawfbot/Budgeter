using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Budgeter.Models
{
    public class DashboardViewModel
    {
        public IEnumerable<Transaction> Transactions { get; set; }
        public IEnumerable<FinAccount> FinAccounts { get; set; }

        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Budget> Budgets { get; set; }
    }
}