using Libellus.DataAccess.Domain;
using System.Data.Entity.Migrations;
using System.Linq;

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
              //new Domain.Department { Id = 1, Faculty = new Domain.Faculty { Id = context.Faculties.FirstOrDefault(x=> x.Id == 1).Id }, Name = "Informatics" },
              //new Domain.Department { Id = 1, Faculty = context.Faculties.FirstOrDefault(x => x.Id == 1), Name = "Informatics" },
              new Department { Id = 1, Faculty = faculty1, Name = "Informatics" },


              //new Domain.Department { Id = 2, Faculty = new Domain.Faculty { Id = 1 }, Name = "Information technologies" },
              //new Department { Id = 2, Faculty = context.Faculties.FirstOrDefault(x => x.Id == 1), Name = "Information technologies" },
              new Department { Id = 2, Faculty = faculty1, Name = "Information technologies" },

              //new Domain.Department { Id = 3, Faculty = new Domain.Faculty { Id = 2 }, Name = "Constructions" },
              new Department { Id = 3, Faculty = faculty2, Name = "Constructions" },
              //new Domain.Department { Id = 4, Faculty = new Domain.Faculty { Id = 2 }, Name = "Computer Engineering" }
              new Department { Id = 4, Faculty = faculty2, Name = "Computer Engineering" }
            );

            //context.Faculties.AddOrUpdate(
            //    f => f.Name,
            //    new Faculty { Id = 1, Name = "Science" },
            //    new Faculty { Id = 2, Name = "Engineering" }
            //    );
            context.Faculties.AddOrUpdate(
                f => f.Name,
                faculty1,
                faculty2
                );

            context.FacultyRoles.AddOrUpdate(
                r => r.Type,
                new FacultyRole { Id = 1, Type = "Professor" },
                new FacultyRole { Id = 2, Type = "Student" }
                );

            context.SaveChanges();
        }
    }
}
