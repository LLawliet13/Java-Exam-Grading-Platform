//using ChamThiWeb5.Models;
using ChamThiDotnet5.Models;
using Microsoft.EntityFrameworkCore;

namespace ChamThiWeb5.Data
{
    public class AppDbContext : DbContext
    {

        public DbSet<Account> Accounts { get; set; }
        private const string connectionString = @"server=(local); database=Test_db; uid=sa; pwd=123456;Trusted_Connection=True;";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(connectionString);  
        }
    }
}
