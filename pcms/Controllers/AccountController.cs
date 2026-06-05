using pcms.Data;
using pcms.Models.DTOs;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace pcms.Controllers
{
    public class AccountController : Controller
    {
        private readonly PcmsDbContext _db = new PcmsDbContext();

        [HttpGet]
        public ActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(EmployeeDtos model)
        {
            if (!ModelState.IsValid)
                return View("Login", model);

            var employee = _db.Employees
                .FirstOrDefault(e => e.EmployeeNumber == model.EmployeeNumber);

            if (employee == null || !BCrypt.Net.BCrypt.Verify(model.Password, employee.PasswordHash))
            {
                ModelState.AddModelError("", "Invalid Employee Number or Password.");
                return View("Login", model);
            }

            // Update last login
            employee.LastLoginAt = DateTime.Now;
            _db.SaveChanges();

            // Forms Authentication cookie
            FormsAuthentication.SetAuthCookie(employee.EmployeeNumber, model.RememberMe);

            // Store role in session
            Session["Role"] = employee.Role;
            Session["EmployeeNumber"] = employee.EmployeeNumber;

            // Redirect based on role
            if (employee.Role == "Admin")
                return RedirectToAction("Index", "Admin");

            return RedirectToAction("Index", "Employee");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}