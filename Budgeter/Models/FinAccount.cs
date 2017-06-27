using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Budgeter.Models
{
    public class FinAccount
    {
        public FinAccount()
        {
            Transactions = new HashSet<Transaction>();
        }

        public int Id { get; set; }

        [Display(Name = "Household")]
        public int HouseholdId { get; set; }

        //Account Name (Checking, Savings, Main, etc.)
        public string Name { get; set; }

        //Starting Balance - Static
        [DataType(DataType.Currency)]
        public double Balance { get; set; }

        //Posted Balance - Dynamic
        [Display(Name = "Reconciled Balance")]
        [DataType(DataType.Currency)]
        public double ReconciledBalance { get; set; }

        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Updated { get; set; }

        public virtual Household Household { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}