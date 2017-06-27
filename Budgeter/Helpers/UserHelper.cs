using Budgeter.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Budgeter.Helpers
{
    public static class UserHelper
    {
        private static ApplicationDbContext db = new ApplicationDbContext();

        public static string GetDisplayNameFromId(string Id)
        {
            return db.Users.Find(Id).DisplayName;
        }

        public static Household GetHouseHoldMembers(this string userId)
        {
            var user = db.Users.Find(userId);

            if (user == null || user.HouseholdId == null)
            {
                return null;
            }

            var household = db.Households.Find(user.HouseholdId);

            return household;
        }
    }
}