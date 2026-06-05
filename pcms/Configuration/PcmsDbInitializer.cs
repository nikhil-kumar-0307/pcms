using pcms.Models;
using System.Data.Entity;


namespace pcms.Data.Configuration
{
    public class PcmsDbInitializer : CreateDatabaseIfNotExists<PcmsDbContext>
    {
        protected override void Seed(PcmsDbContext context)
        {
            // Admin user
            context.Employees.Add(new Employee
            {
                EmployeeNumber = "ADMIN001",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                Role = "Admin"
            });

            // Regular employee
            context.Employees.Add(new Employee
            {
                EmployeeNumber = "EMP001",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Employee@123"),
                Role = "Employee"
            });

            context.SaveChanges();
            base.Seed(context);
        }
    }
}