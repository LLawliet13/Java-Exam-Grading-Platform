
using ChamThiDotnet5.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;


namespace ChamThiWeb5.Data
{
    public class AppDbContext : DbContext
    {

        public DbSet<Account> Accounts { get; set;}
        public DbSet<AccountType> AccountTypes { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Student> Students { get; set; }

        public DbSet<Exam> Exams { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);
            foreach( var fk in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
                modelBuilder.Entity<Teacher>().HasOne(e => e.Account).WithOne(e => e.Teacher).OnDelete(DeleteBehavior.Cascade);
                modelBuilder.Entity<Student>().HasOne(e => e.Account).WithOne(e => e.Student).OnDelete(DeleteBehavior.Cascade);




            }
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfiguration configuration = builder.Build();



            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));  
        }
    }
}
