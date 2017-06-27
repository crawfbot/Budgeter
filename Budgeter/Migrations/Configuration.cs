namespace Budgeter.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Budgeter.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Budgeter.Models.ApplicationDbContext context)
        {
            // Seed Roles
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }
            if (!context.Roles.Any(r => r.Name == "Comptroller"))
            {
                roleManager.Create(new IdentityRole { Name = "Comptroller" });
            }
            if (!context.Roles.Any(r => r.Name == "Member"))
            {
                roleManager.Create(new IdentityRole { Name = "Member" });
            }

            // Seed Users

            //Admin
            if (!context.Users.Any(u => u.Email == "jacobcrawford1990@gmail.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "jacobcrawford1990@gmail.com",
                    Email = "jacobcrawford1990@gmail.com",
                    FirstName = "Jacob",
                    LastName = "Crawford",
                    DisplayName = "Jarvis",
                    Role = "Admin",
                }, ".CoderFoundry$");
            }

            var userId = userManager.FindByEmail("jacobcrawford1990@gmail.com").Id;
            userManager.AddToRole(userId, "Admin");

            //Comptroller
            if (!context.Users.Any(u => u.Email == "tony_moneytree@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "tony_moneytree@mailinator.com",
                    Email = "tony_moneytree@mailinator.com",
                    FirstName = "Tony",
                    LastName = "Stark",
                    DisplayName = "Iron Man",
                    Role = "Comptroller",
                }, ".CoderFoundry$");
            }

            userId = userManager.FindByEmail("tony_moneytree@mailinator.com").Id;
            userManager.AddToRole(userId, "Comptroller");

            //Member
            if (!context.Users.Any(u => u.Email == "rhodey_moneytree@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "rhodey_moneytree@mailinator.com",
                    Email = "rhodey_moneytree@mailinator.com",
                    FirstName = "James",
                    LastName = "Rhodes",
                    DisplayName = "War Machine",
                    Role = "Member",
                }, ".CoderFoundry$");
            }
            userId = userManager.FindByEmail("rhodey_moneytree@mailinator.com").Id;
            userManager.AddToRole(userId, "Member");

            // Guest Admin
            if (!context.Users.Any(u => u.Email == "ari_moneytree@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "ari_moneytree@mailinator.com",
                    Email = "ari_moneytree@mailinator.com",
                    FirstName = "Guest",
                    LastName = "Admin",
                    DisplayName = "Ari Addworth",
                    Role = "Admin",
                }, "LearnToCode1");
            }
            userId = userManager.FindByEmail("ari_moneytree@mailinator.com").Id;
            userManager.AddToRole(userId, "Admin");

            //Guest Comptroller
            if (!context.Users.Any(u => u.Email == "cameron_moneytree@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "cameron_moneytree@mailinator.com",
                    Email = "cameron_moneytree@mailinator.com",
                    FirstName = "Guest",
                    LastName = "Comptroller",
                    DisplayName = "Cameron Cash",
                    Role = "Comptroller",
                }, "LearnToCode1");
            }
            userId = userManager.FindByEmail("cameron_moneytree@mailinator.com").Id;
            userManager.AddToRole(userId, "Comptroller");

            //Guest Member
            if (!context.Users.Any(u => u.Email == "morgan_moneytree@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "morgan_moneytree@mailinator.com",
                    Email = "morgan_moneytree@mailinator.com",
                    FirstName = "Guest",
                    LastName = "Member",
                    DisplayName = "Morgan Moneypenny",
                    Role = "Member",
                }, "LearnToCode1");
            }
            userId = userManager.FindByEmail("morgan_moneytree@mailinator.com").Id;
            userManager.AddToRole(userId, "Member");

            context.TransactionTypes.AddOrUpdate(t => t.Name,
                new TransactionType() { Name = "Debit" },
                new TransactionType() { Name = "Credit" }

            );
        }
    }
}
