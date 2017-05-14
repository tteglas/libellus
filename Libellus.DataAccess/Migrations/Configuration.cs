using System;
using Libellus.DataAccess.Domain;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using Libellus.CommonConcerns.Constants;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace Libellus.DataAccess.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<Database.LibellusDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Database.LibellusDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            var faculty1 = new Faculty { Id = 1, Name = "Science" };
            var faculty2 = new Faculty { Id = 2, Name = "Engineering" };


            context.Departments.AddOrUpdate(
                d => d.Name,
              new Department { Id = 1, Faculty = faculty1, Name = "Informatics" },

              new Department { Id = 2, Faculty = faculty1, Name = "Information technologies" },

              new Department { Id = 3, Faculty = faculty2, Name = "Constructions" },

              new Department { Id = 4, Faculty = faculty2, Name = "Computer Engineering" }
            );

            context.Faculties.AddOrUpdate(
                f => f.Name,
                faculty1,
                faculty2
                );

            var passwordHash = new PasswordHasher();
            var password = passwordHash.HashPassword("P@ssw0rd");

            context.Users.AddOrUpdate(u => u.UserName,
                new User
                {
                    UserName = "dumitru@zxc.as",
                    Email = "dumitru@zxc.as",
                    PasswordHash = password,
                    PhoneNumber = "12345678",
                    DepartmentId = 1,
                    Department = context.Departments.FirstOrDefault(x => x.Id == 1),
                    SecurityStamp = new Guid().ToString()
                },
                new User
                {
                    UserName = "mona@zxc.as",
                    Email = "mona@zxc.as",
                    PasswordHash = password,
                    PhoneNumber = "12345679",
                    DepartmentId = 2,
                    Department = context.Departments.FirstOrDefault(x => x.Id == 1),
                    SecurityStamp = new Guid().ToString()
                });

            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            var role = new IdentityRole { Name = CommonHelper.ROLE_Professor };
            var role1 = new IdentityRole { Name = CommonHelper.ROLE_Student };

            roleManager.Create(role);
            roleManager.Create(role1);

            var store = new UserStore<User>(context);
            var manager = new UserManager<User>(store);
            var user = context.Users.FirstOrDefault(x => x.UserName == "dumitru@zxc.as");
            var user1 = context.Users.FirstOrDefault(x => x.UserName == "mona@zxc.as");

            manager.AddToRole(user.Id, role.Name);
            manager.AddToRole(user1.Id, role1.Name);

            context.SaveChanges();
        }
    }
}
