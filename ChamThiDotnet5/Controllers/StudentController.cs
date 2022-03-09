using Microsoft.AspNetCore.Mvc;

namespace ChamThiDotnet5.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Student()
        {
            return View();
        }
    }
}
