using System.Web.Mvc;

namespace pcms.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["Role"]?.ToString() != "Employee")
                return RedirectToAction("Login", "Account");

            return View();
        }
    }
}