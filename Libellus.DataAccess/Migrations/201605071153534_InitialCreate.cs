using System.Data.Entity.Migrations;

namespace Libellus.DataAccess.Migrations
{
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable("dbo.Faculty",
               c => new
               {
                   Id = c.Int(nullable: false, identity: true),
                   Name = c.String(nullable: false, maxLength: 100)
               })
               .PrimaryKey(t => t.Id);

            CreateTable("dbo.Department",
               c => new
               {
                   Id = c.Int(nullable: false, identity: true),
                   Name = c.String(nullable: false, maxLength: 100),
                   FacultyId = c.Int(nullable: false)
               })
               .PrimaryKey(t => t.Id)
               .ForeignKey("dbo.Faculty", t => t.FacultyId, cascadeDelete: true);

            CreateTable("dbo.Project",
               c => new
               {
                   Id = c.Int(nullable: false, identity: true),
                   DepartmentId = c.Int(nullable: false),
                   UserId = c.String(nullable: false, maxLength: 128),
                   Name = c.String(maxLength: 250),
                   Description = c.String(),
                   Status = c.String(maxLength: 100),
                   AddedBy = c.String(maxLength: 100),
                   Progress = c.Decimal(nullable: false, precision: 2),
                   CreatedDate = c.DateTime(nullable: false),
                   ModifiedDate = c.DateTime(nullable: false)
               })
               .PrimaryKey(t => t.Id)
               .ForeignKey("dbo.Department", t => t.DepartmentId, cascadeDelete: true);
            //.ForeignKey("dbo.AspNetUsers", t => t.UserId);

            #region Task

            CreateTable("dbo.Task",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    ProjectId = c.Int(nullable: false),
                    UserId = c.String(nullable: false, maxLength: 128),
                    Description = c.String(maxLength: 50),
                    IsActive = c.Boolean(nullable: false, defaultValue: false),
                    Progress = c.Decimal(nullable: false, precision: 2),
                    CreatedDate = c.DateTime(nullable: false),
                    ModifiedDate = c.DateTime(nullable: false)
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true);
            //.ForeignKey("dbo.Project", t => t.ProjectId);

            #endregion

            CreateTable("dbo.FacultyRoles",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Type = c.String(nullable: false)
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.AspNetRoles",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    Name = c.String(nullable: false, maxLength: 256),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");

            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                {
                    UserId = c.String(nullable: false, maxLength: 128),
                    RoleId = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);

            CreateTable(
                "dbo.AspNetUsers",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    Email = c.String(maxLength: 256),
                    EmailConfirmed = c.Boolean(nullable: false),
                    PasswordHash = c.String(),
                    SecurityStamp = c.String(),
                    PhoneNumber = c.String(),
                    PhoneNumberConfirmed = c.Boolean(nullable: false),
                    TwoFactorEnabled = c.Boolean(nullable: false),
                    LockoutEndDateUtc = c.DateTime(),
                    LockoutEnabled = c.Boolean(nullable: false),
                    AccessFailedCount = c.Int(nullable: false),
                    UserName = c.String(nullable: false, maxLength: 256),
                    ProjectId = c.Int(nullable: true),
                    DepartmentId = c.Int(nullable: false),
                    FacultyRoleId = c.Int(nullable: false)
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Department", t => t.DepartmentId)
                //.ForeignKey("dbo.Project", t => t.ProjectId, cascadeDelete: true)
                //.ForeignKey("dbo.FacultyRoles", t => t.FacultyRoleId, cascadeDelete: true)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");

            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    UserId = c.String(nullable: false, maxLength: 128),
                    ClaimType = c.String(),
                    ClaimValue = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                {
                    LoginProvider = c.String(nullable: false, maxLength: 128),
                    ProviderKey = c.String(nullable: false, maxLength: 128),
                    UserId = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
        }

        public override void Down()
        {
            DropForeignKey("dbo.Faculty", "FacultyId", "dbo.Department");
            DropForeignKey("dbo.Department", "DepartmentId", "dbo.Project");
            DropForeignKey("dbo.AspNetUsers", "UserId", "dbo.Project");
            DropForeignKey("dbo.AspNetUsers", "UserId", "dbo.Task");
            DropForeignKey("dbo.AspNetUsers", "FacultyRoleId", "dbo.FacultyRoles");
            DropForeignKey("dbo.Project", "ProjectId", "dbo.Task");

            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");

            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");

            DropTable("dbo.Faculty");
            DropTable("dbo.Department");
            DropTable("dbo.Project");
            DropTable("dbo.Task");

            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
        }
    }
}
