using Microsoft.AspNetCore.Mvc;

namespace LackingWebSecurityCore.Controllers
{
    public class SqlInjectionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult Rank()
        {
            return View();
        }

        public ActionResult TryIt()
        {
            return View();
        }

        public ActionResult WhyItsImportant()
        {
            return View();
        }

        public ActionResult Defenses()
        {
            return View();
        }

        public ActionResult KnownAssociates()
        {
            return View();
        }
    }
}