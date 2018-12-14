namespace StackOverFlowAuth.Migrations.ApplicationDbContext
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using StackOverFlowAuth.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<StackOverFlowAuth.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\ApplicationDbContext";
        }

        protected override void Seed(StackOverFlowAuth.Models.ApplicationDbContext context)
        {
            string[] roles = new string[] { "Admin", "User" };
            foreach (string role in roles)
            {
                if (!context.Roles.Any(r => r.Name == role))
                {
                    context.Roles.Add(new IdentityRole(role));
                }
            }

            //create user UserName:Owner Role:Admin
            if (!context.Users.Any(u => u.UserName == "john@john.com"))
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var user = new ApplicationUser
                {
                    Email = "john@john.com",
                    UserName = "john@john.com",
                    PhoneNumber = null,
                    EmailConfirmed = false,
                    PhoneNumberConfirmed = false,
                    SecurityStamp = Guid.NewGuid().ToString("D"),
                    PasswordHash = userManager.PasswordHasher.HashPassword("Aa123456*"),
                    LockoutEnabled = true,
                };
                userManager.Create(user);
                userManager.AddToRole(user.Id, "Admin");

            }

            if (!context.Users.Any(u => u.UserName == "mary@mary.com"))
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var user = new ApplicationUser
                {
                    Email = "mary@mary.com",
                    UserName = "mary@mary.com",
                    PhoneNumber = null,
                    EmailConfirmed = false,
                    PhoneNumberConfirmed = false,
                    SecurityStamp = Guid.NewGuid().ToString("D"),
                    PasswordHash = userManager.PasswordHasher.HashPassword("Aa123456*"),
                    LockoutEnabled = true,
                };
                userManager.Create(user);
                userManager.AddToRole(user.Id, "User");

            }

            if (!context.Users.Any(u => u.UserName == "mark@mark.com"))
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var user = new ApplicationUser
                {
                    Email = "mark@mark.com",
                    UserName = "mark@mark.com",
                    PhoneNumber = null,
                    EmailConfirmed = false,
                    PhoneNumberConfirmed = false,
                    SecurityStamp = Guid.NewGuid().ToString("D"),
                    PasswordHash = userManager.PasswordHasher.HashPassword("Aa123456*"),
                    LockoutEnabled = true,
                };
                userManager.Create(user);
                userManager.AddToRole(user.Id, "User");

            }

            context.SaveChanges();
        }
    }
}
