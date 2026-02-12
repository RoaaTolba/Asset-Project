using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Microsoft.AspNetCore.Identity;


namespace AssetsPro.Models
{
    public class MyDbContext:IdentityDbContext<ApplicationUser>
    {
        public MyDbContext() : base()  { }
        public MyDbContext(DbContextOptions options):base(options) { }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<SalaryReport> Salaries { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Database=AssetsDB;Data Source=DESKTOP-LVA4E65\\MSSQLSERVER01;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
            base.OnConfiguring(optionsBuilder);
        }
        //public void CreateDefaultRolesAndUsers(UserManager<ApplicationUser> userManager,
        //                               RoleManager<IdentityRole> roleManager)
        //{
        //    // مثال Role
        //    string roleName = "Admin";
        //    if (!roleManager.RoleExistsAsync(roleName).Result)
        //    {
        //        roleManager.CreateAsync(new IdentityRole(roleName)).Wait();
        //    }

        //    // مثال Default User
        //    string email = "admin@system.com";
        //    string password = "Admin@123";
        //    var user = userManager.FindByEmailAsync(email).Result;
        //    if (user == null)
        //    {
        //        user = new ApplicationUser { UserName = email, Email = email, EmailConfirmed = true };
        //        userManager.CreateAsync(user, password).Wait();
        //        userManager.AddToRoleAsync(user, roleName).Wait();
        //    }
        //}

    }
}
