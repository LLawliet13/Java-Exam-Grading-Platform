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
            ;

            // truyen doi tuong sang trang web
            this.ViewData["info"] = _accountService.start();
            return View("CustomNamePage");
        }
    }
}
