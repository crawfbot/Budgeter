using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Budgeter.Models
{
    public class Invitation
    {
        public int Id { get; set; }

        [Display(Name = "Household")]
        public int HouseholdId { get; set; }

        [Required]
        [Display(Name = "Invitee")]
        public string InviteeName { get; set; }

        [Required(ErrorMessage = "Please provide an email address for the invited party.")]
        [EmailAddress]
        public string Email { get; set; }

        public string InvitationCode { get; set; }
        public string InvitedBy { get; set; }
        public DateTimeOffset InvitedDate { get; set; }

        [Display(Name = "Give Comptroller Rights?")]
        public bool HasAdminRights { get; set; }

        public bool Accepted { get; set; }
        
        public virtual Household Household { get; set; }
        
    }
}