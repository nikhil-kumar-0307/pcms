namespace pcms.Migrations
{
    using pcms.Models;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<pcms.Data.PcmsDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(pcms.Data.PcmsDbContext context)
        {
            context.Employees.AddOrUpdate(
                e => e.EmployeeNumber,

                new Employee
                {
                    EmployeeNumber = "ADMIN001",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                    Role = "Admin"
                },

                new Employee
                {
                    EmployeeNumber = "EMP001",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Employee@123"),
                    Role = "Employee"
                }
            );
        }
    }
}