using System.Web.Mvc;

namespace pcms.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
       
        public ActionResult Index()
        {
            if (Session["Role"]?.ToString() != "Admin")
                return RedirectToAction("Login", "Account");

            return View();
        }       
        public ActionResult Extra()
        {
            if (Session["Role"]?.ToString() != "Admin")
                return RedirectToAction("Login", "Account");

            return View();
        }
    }
}