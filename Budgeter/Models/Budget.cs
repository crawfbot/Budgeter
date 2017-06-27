using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Budgeter.Models
{
    public class Budget
    {
        //public Budget()
        //{
        //    Categories = new HashSet<Category>();
        //}

        public int Id { get; set; }
        public string Name { get; set; }

        [Display(Name = "Household")]
        public int? HouseholdId { get; set; }

        [DataType(DataType.Currency)]
        public double Amount { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Updated { get; set; }

        public virtual Household Household { get; set; }
        public virtual Category Category { get; set; }

        //public virtual ICollection<Category> Categories { get; set; }
    }
}