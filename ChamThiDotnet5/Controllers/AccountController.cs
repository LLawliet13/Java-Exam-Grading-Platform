using ChamThiDotnet5.Models;
using ChamThiDotnet5.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ChamThiDotnet5.Controllers
{

    public class AccountController : Controller
    {

        private readonly AccountService _accountService;


        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }

        public IActionResult Index()
        {
            ;

            // truyen doi tuong sang trang web
            this.ViewData["info"] = _accountService.start();
            return View("Login");
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
            return View();
        }
        public IActionResult Account()
        {
            return View();
        }

    }
}
