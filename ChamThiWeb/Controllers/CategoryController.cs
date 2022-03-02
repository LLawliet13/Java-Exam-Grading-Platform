using Microsoft.AspNetCore.Mvc;

namespace ChamThiWeb.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
