using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Budgeter.Models
{
    public class Household
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Updated { get; set; }

        public Household()
        {
            Budgets = new HashSet<Budget>();
            Accounts = new HashSet<FinAccount>();
            Members = new HashSet<ApplicationUser>();
            Invitations = new HashSet<Invitation>();

            //Categories = new HashSet<Category>();
        }

        public virtual ICollection<Budget> Budgets { get; set; }
        public virtual ICollection<FinAccount> Accounts { get; set; }
        public virtual ICollection<ApplicationUser> Members { get; set; }
        public virtual ICollection<Invitation> Invitations { get; set; }

        //public virtual ICollection<Category> Categories { get; set; }

    }
}