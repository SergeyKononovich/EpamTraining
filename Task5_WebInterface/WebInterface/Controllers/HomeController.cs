using System.Web.Mvc;

namespace WebInterface.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
                return RedirectToAction("Index", "Sales");

            return RedirectToAction("About");
        }

        [Authorize(Roles = "admin")]
        public ActionResult Users()
        {
            return View();
        }

        public ActionResult Statistic()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Service for sales management.";

            return View();
        }

        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "Contact page.";

            return View();
        }
    }
}