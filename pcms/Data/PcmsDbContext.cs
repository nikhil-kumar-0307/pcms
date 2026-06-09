using pcms.Data.Configuration;
using pcms.Models;
using PCMS.Models;          // <-- add this for LotMaster
using System.Data.Entity;

namespace pcms.Data
{
    public class PcmsDbContext : DbContext
    {
        public PcmsDbContext() : base("PcmsDbContext")
        {
            Database.SetInitializer(new PcmsDbInitializer());
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<LotMaster> LotMasters { get; set; }
        public DbSet<ItStock> ItStocks { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<EmployeeDetails> EmployeeDetails { get; set; }

    }
}