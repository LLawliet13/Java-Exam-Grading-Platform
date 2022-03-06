using ChamThiDotnet5.Services;
using Microsoft.AspNetCore.Mvc;

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
            return View();
        }
    }
}
