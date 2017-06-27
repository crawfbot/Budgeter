using Budgeter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Budgeter.Helpers
{
    public static class DashboardHelper
    {
        private static ApplicationDbContext db = new ApplicationDbContext();

        public static List<double> FiscalYear(int number)
        {
            List<double> year = new List<double>();

            for (int i = number; i <= 12; i += 1)
            {
                year.Add(db.Transactions.Where(t => t.CategoryId == 13).Where(t => t.Date.Month == i).Sum(x => (double?)x.Amount) ?? 0);
            }

            return year;
        }
    }
}