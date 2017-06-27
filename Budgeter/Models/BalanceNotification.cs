using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Budgeter.Models
{
    public class BalanceNotification
    {
        public int Id { get; set; }

        [Display(Name = "Household Id")]
        public int? HouseholdId { get; set; }

        public string Type { get; set; }

        [Display(Name = "Household Member")]
        public string UserId { get; set; }

        public bool Read { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Household Household { get; set; }
    }
}