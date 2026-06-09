using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using pcms.Data;
using pcms.Models;
using pcms.Models.DTOs;

namespace pcms.Controllers
{
    public class EmployeeDetailsController : Controller
    {
        private readonly PcmsDbContext _db;

        public EmployeeDetailsController()
        {
            _db = new PcmsDbContext();
        }

        // GET: EmployeeDetails/Index
        public ActionResult Index()
        {
            ViewBag.Departments = GetDepartmentList();
            ViewBag.Employees = GetActiveEmployees();
            ViewBag.EmployeeCount = ((List<EmployeeDetails>)ViewBag.Employees).Count;
            return View(new EmployeeDetailsDto());
        }

        // POST: EmployeeDetails/Insert
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Insert(EmployeeDetailsDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Departments = GetDepartmentList();
                ViewBag.Employees = GetActiveEmployees();
                ViewBag.EmployeeCount = ((List<EmployeeDetails>)ViewBag.Employees).Count;
                return View("Index", dto);
            }

            bool empExists = _db.EmployeeDetails
                .Any(e => e.EmpNumber == dto.EmpNumber && e.IsActive);

            if (empExists)
            {
                ModelState.AddModelError("EmpNumber", "An employee with this number already exists.");
                ViewBag.Departments = GetDepartmentList();
                ViewBag.Employees = GetActiveEmployees();
                ViewBag.EmployeeCount = ((List<EmployeeDetails>)ViewBag.Employees).Count;
                return View("Index", dto);
            }

            var employee = new EmployeeDetails
            {
                EmpNumber = dto.EmpNumber.Trim(),
                EmpName = dto.EmpName.Trim(),
                Designation = dto.Designation.Trim(),
                DeptCode = dto.DeptCode,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            _db.EmployeeDetails.Add(employee);
            _db.SaveChanges();

            TempData["SuccessMessage"] = $"'{employee.EmpName}' added successfully.";
            return RedirectToAction("Index");
        }

        // GET: EmployeeDetails/GoToUpdate?empNumber=090983
        public ActionResult GoToUpdate(string empNumber)
        {
            if (string.IsNullOrWhiteSpace(empNumber))
            {
                TempData["ErrorMessage"] = "Please enter an employee number.";
                return RedirectToAction("Index", "Admin");
            }
            return RedirectToAction("UpdateEmployeeDetails", new { empNumber = empNumber.Trim() });
        }


        // ── GET: EmployeeDetails/UpdateEmployeeDetails?empNumber=090983 ────────────
        public ActionResult UpdateEmployeeDetails(string empNumber)
        {
            if (string.IsNullOrWhiteSpace(empNumber))
            {
                TempData["ErrorMessage"] = "No employee number provided.";
                return RedirectToAction("Index", "Admin");
            }

            var employee = _db.EmployeeDetails
                .FirstOrDefault(e => e.EmpNumber == empNumber && e.IsActive);

            if (employee == null)
            {
                TempData["ErrorMessage"] = $"No active employee found with number \"{empNumber}\".";
                return RedirectToAction("Index", "Admin");
            }

            var dto = new UpdateEmployeeDetailsDto
            {
                Id = employee.Id,
                EmpNumber = employee.EmpNumber,
                EmpName = employee.EmpName,
                Designation = employee.Designation,
                DeptCode = employee.DeptCode,
                IsActive = employee.IsActive
            };

            ViewBag.Departments = GetDepartmentList(employee.DeptCode);
            return View(dto);
        }

        // ── POST: EmployeeDetails/UpdateEmployeeDetails ────────────────────────────
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateEmployeeDetails(UpdateEmployeeDetailsDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Departments = GetDepartmentList(dto.DeptCode);
                return View(dto);
            }

            var employee = _db.EmployeeDetails.Find(dto.Id);

            if (employee == null)
            {
                TempData["ErrorMessage"] = "Employee record not found.";
                return RedirectToAction("Index", "Admin");  // ← changed
            }

            employee.EmpName = dto.EmpName.Trim();
            employee.Designation = dto.Designation.Trim();
            employee.DeptCode = dto.DeptCode;
            employee.IsActive = dto.IsActive;

            _db.SaveChanges();

            TempData["SuccessMessage"] = $"Employee \"{employee.EmpName}\" updated successfully.";
            return RedirectToAction("UpdateEmployeeDetails", new { empNumber = employee.EmpNumber });
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing) _db.Dispose();
            base.Dispose(disposing);
        }

        // ── helpers ──────────────────────────────────────────────────────────

        private List<EmployeeDetails> GetActiveEmployees()
        {
            return _db.EmployeeDetails
                .Where(e => e.IsActive)
                .OrderByDescending(e => e.CreatedAt)
                .ToList();
        }

        private SelectList GetDepartmentList(int? selectedValue = null)
        {
            var departments = new List<SelectListItem>
            {
                new SelectListItem { Value = "1008", Text = "ASH DYKE MANAGEMENT (1008)" },
                new SelectListItem { Value = "1009", Text = "OPERATIONS (1009)"          },
                new SelectListItem { Value = "1010", Text = "FINANCE (1010)"             },
                new SelectListItem { Value = "1011", Text = "HUMAN RESOURCES (1011)"     },
            };
            return new SelectList(departments, "Value", "Text", selectedValue);
        }
    }
}