﻿using ChamThiDotnet5.DAO;
using ChamThiDotnet5.Models;
using ChamThiDotnet5.Services;
using ChamThiWeb5.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;


namespace ChamThiDotnet5.Controllers
{


    public class AccountController : Controller
    {
        AppDbContext db = new AppDbContext();
        AccountDAO accountDAO = new AccountDAO(); 
        private readonly AccountService _accountService;

        
        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }

        
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
            if (obj != null)
            {
                var e = db.Entry(obj);
                e.Reference(ai => ai.AccountType).Load();
                HttpContext.Session.SetString("username", obj.Username);
                HttpContext.Session.SetString("password", obj.Password);
                HttpContext.Session.SetString("accounttype", obj.AccountType.Typename);
                HttpContext.Session.SetString("accountid", obj.Id.ToString());
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["error"] = "Error";
                return View(account);
            }
        }
        [HttpGet]
        public IActionResult Logout()
        {

            HttpContext.Session.Clear();
            HttpContext.Session.Remove("username");
            HttpContext.Session.Remove("password");
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Index()
        {
            ViewBag.Account = accountDAO.ReadAAccount(int.Parse(HttpContext.Session.GetString("accountid")));
            return View();
        }
        [HttpPost]
        public string ChangePassword(Account account,string newPassword)
        {
            Account account1 = accountDAO.ReadAAccount(account.Id);
            if (!account1.Password.Equals(account.Password)) return "Wrong Old Password";
            account.Password = newPassword;
            
            int n = 0;
            n = accountDAO.UpdateAccount(account.Id, account);
            if (n > 0) return "changed";
            return "DB Cant be Changed";
        }

    }
}
