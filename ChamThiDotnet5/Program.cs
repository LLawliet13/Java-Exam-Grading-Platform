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
        static bool CreateDatabase()
        {
            using var dbcontext = new AppDbContext();
            Console.WriteLine(dbcontext.Database.GetDbConnection().Database);
            return dbcontext.Database.EnsureCreated();
        }
        static bool DropDatabase()
        {
            using var dbcontext = new AppDbContext();
            return dbcontext.Database.EnsureDeleted();
        }
        public static void Main(string[] args)
        {
            //DropDatabase();
            var ans = CreateDatabase();
            if(ans)
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
