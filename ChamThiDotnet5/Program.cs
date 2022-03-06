using ChamThiDotnet5.DAO;
using ChamThiDotnet5.Models;
using ChamThiWeb5.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChamThiDotnet5
{
    public class Program
    {
        static void CreateDatabase()
        {
            using var dbcontext = new AppDbContext();
            Console.WriteLine(dbcontext.Database.GetDbConnection().Database);
            //dbcontext.Database.EnsureCreated();
            dbcontext.Database.Migrate();
        }
        static bool DropDatabase()
        {
            using var dbcontext = new AppDbContext();
            return dbcontext.Database.EnsureDeleted();
        }
        public static void Main(string[] args)
        {
            //DropDatabase();
            CreateDatabase();
            //var ans = CreateDatabase();
            //AccountDAO AccountDAO = new AccountDAO();
            //AccountTypeDAO AccountTypeDao = new AccountTypeDAO();
            //AccountTypeDao.AddNewAccountType(new AccountType()
            //{
            //    Typename = "Vip"
            //});
            //AccountDAO.AddNewAccount(new Account()
            //{
            //    AccountTypeId = 1,
            //    Email = "Dung@gmail.com",
            //    Username = "username1",
            //    Password = "123456"
            //});
            //int n = 0 ;
            //try
            //{
            //    n = AccountTypeDao.DeleteAccountType(1);
            //}
            //catch (Exception ex)
            //{

            //    Console.Error.WriteLine(ex.Message);
            //}
            //if (n == 0) Console.WriteLine("cant delete");
            //else Console.WriteLine("deleted");
            //List<Account> AccountList = AccountDAO.ReadAllAccount();


            //Account account = AccountDAO.ReadAAccount(1);
            //Console.WriteLine(account.Username);
            //    account.Username = "username1";
            //    AccountDAO.UpdateAccount(1, account);
            //    Console.WriteLine(AccountDAO.ReadAAccount(1).Username);
            
            
            CreateHostBuilder(args).Build().Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
