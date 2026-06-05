using pcms.Data.Configuration;
using pcms.Models;
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
    }
}