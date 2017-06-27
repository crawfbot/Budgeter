using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Budgeter.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        [Display(Name = "Account")]
        public int AccountId { get; set; }

        public string Description { get; set; }

        //Date options: created, transaction, posted/reconciled
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTimeOffset Date { get; set; }

        [DataType(DataType.Currency)]
        public double Amount { get; set; }

        [Display(Name = "Transaction Type")]
        public int TransactionTypeId { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Display(Name = "Entered By")]
        public string EnteredById { get; set; }

        //Posted? Yes or no.
        public bool Reconciled { get; set; }

        [Display(Name = "Reconciled Amount")]
        [DataType(DataType.Currency)]
        public double ReconciledAmount { get; set; }

        public virtual FinAccount Account { get; set; }
        public virtual Category Category { get; set; }
        public virtual TransactionType TransactionType { get; set; }
        public virtual ApplicationUser EnteredBy { get; set; }
    }
}