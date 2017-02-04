using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Libellus.DataAccess.Domain;

namespace Libellus.DataAccess.Database
{
    public class LibellusDbContext : IdentityDbContext<Domain.User>
    {
        public LibellusDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static LibellusDbContext Create()
        {
            return new LibellusDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Faculty>().ToTable("Faculty");
            modelBuilder.Entity<Department>().ToTable("Department");
            modelBuilder.Entity<Project>().ToTable("Project");
            modelBuilder.Entity<Task>().ToTable("Task");
            modelBuilder.Entity<User>().ToTable("AspNetUsers");

            //modelBuilder.Entity<User>().HasMany(x => x.Projects)
            //    .WithRequired().HasForeignKey(x => x.UserId);
            //modelBuilder.Entity<Department>().HasRequired(x => x.User).WithMany(x => x.Department);
            //modelBuilder.Entity<User>().HasOptional(x => x.Projects).WithOptionalPrincipal();
        }

        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<FacultyRole> FacultyRoles { get; set; }
    }
}