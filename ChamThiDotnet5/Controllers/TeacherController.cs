using Microsoft.AspNetCore.Mvc;

namespace ChamThiDotnet5.Controllers
{
    public class TeacherController : Controller
    {
        public IActionResult Teacher()
        {
            return View();
        }
    }
}
