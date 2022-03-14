using ChamThiDotnet5.Models;
using ChamThiDotnet5.Services;
using ChamThiWeb5.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.IO;
using System.Linq;

namespace ChamThiDotnet5.Controllers
{


    public class AccountController : Controller
    {
        AppDbContext db = new AppDbContext();

        private readonly AccountService _accountService;


        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }

        //public IActionResult Index()
        //{
        //    if (HttpContext.Session.GetString("AccountSession") != null)
        //    {
        //        TempData["account"] = HttpContext.Session.GetString("Account");
        //        return View();
        //    }
        //    else
        //    {
        //        return RedirectToAction("Login", "Account");
        //    }
        //}
        [HttpGet]
        public IActionResult Login()
        {
            Account account = new Account();
            return View(account);
        }
        [HttpPost]
        public IActionResult Login(Account account)
        {
            var obj = db.Accounts.Where(x => x.Username.Equals(account.Username) && x.Password.Equals(account.Password)).FirstOrDefault();
            var e = db.Entry(obj);
            e.Reference(ai => ai.AccountType).Load();
            if (obj != null)
            {
                HttpContext.Session.SetString("username", obj.Username);
                HttpContext.Session.SetString("password", obj.Password);
                HttpContext.Session.SetString("accounttype", obj.AccountType.Typename);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["error"] = "Error";
                return View(account);
            }
        }
        public IActionResult Account()
        {
            return View();
        }

    }
}
