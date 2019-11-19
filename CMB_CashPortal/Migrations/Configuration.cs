namespace CMB_CashPortal.Migrations
{
    using CMB_CashPortal.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Configuration;

    internal sealed class Configuration : DbMigrationsConfiguration<CMB_CashPortal.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(CMB_CashPortal.Models.ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var seededUserPassword = WebConfigurationManager.AppSettings["SeededUserPassword"];

            #region Seed Roles
            //Roles - Admin, HeadofHousehold, Member, Roleless


            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }

            if (!context.Roles.Any(r => r.Name == "HOH"))
            {
                roleManager.Create(new IdentityRole { Name = "HOH" });
            }

            if (!context.Roles.Any(r => r.Name == "Member"))
            {
                roleManager.Create(new IdentityRole { Name = "Member" });
            }

            #endregion

            #region Seed Users
            //Seed yourself

            if (!context.Users.Any(u => u.Email == "LeeC@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "LeeC@Mailinator.com",
                    Email = "LeeC@Mailinator.com",
                    FirstName = "Lee",
                    LastName = "Connelly",
                    AvatarUrl = "/Avatars/avatar1.jpg"
                }, seededUserPassword);
            }
            #endregion

            #region Assign Roles to Users
            //Asign yourself role of Admin
            var adminId = userManager.FindByEmail("LeeC@mailinator.com").Id;
            userManager.AddToRole(adminId, "Admin");
            #endregion

            #region Seed Transaction Types
            //Seed types of Deposit, Withdrawal,Transfer, Adjustment Up, Adjustment Down
            context.TransactionTypes.AddOrUpdate(t => t.Type,
                new TransactionType { Type = "Deposit" },
                new TransactionType { Type = "Withdrawal" },
                new TransactionType { Type = "Transfer" },
                new TransactionType { Type = "Adjustment" },
                new TransactionType { Type = "Fee" }
                );
            #endregion

            #region Seed Account Types
            //Seed Checking, Savings, Money Market,...
            context.AccountTypes.AddOrUpdate(a => a.Type,
                new AccountType { Type = "Checking", Description = "This is a basic checking account for making purchases and paying bills." },
                new AccountType { Type = "Savings", Description = "This is a basic savings account for deposits and earning simple interest." },
                new AccountType { Type = "Money Market", Description = "This is a savings/investment for buying and selling stocks and bonds, investing in funds, etc. to build wealth." },
                new AccountType { Type = "Roth IRA", Description = "This is a savings account for deposits to earn interest and build a nest egg for retirement." },
                new AccountType { Type = "CD", Description = "This is a short term savings account that earns higher interest rates than a regular savings account but cannot be withdrawn before term is up." }
                );
            #endregion

            #region Seed House
            context.Households.AddOrUpdate(h => h.Name,
                new Household
                {
                    Name = "The HOHiest HouseHold",
                    Greeting = "Welcome to the Good Life!",
                    Created = DateTime.Now,

                }

                );
            context.SaveChanges();
            #endregion

            #region HOH User
            if (!context.Users.Any(r => r.UserName == "HOH@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    Household = context.Households.FirstOrDefault(h => h.Name == "The HOHiest Household"),
                    UserName = "HOH@mailinator.com",
                    Email = "HOH@mailinator.com",
                    FirstName = "Headley",
                    LastName = "Housester",
                    AvatarUrl = "/Avatars/avatar2.jpg"
                }, seededUserPassword);
            }

            var hohId = userManager.FindByEmail("HoH@mailinator.com").Id;
            userManager.AddToRole(hohId, "HOH");
            #endregion
        }
    }
}
