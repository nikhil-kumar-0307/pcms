using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using pcms.Data;
using pcms.Models;
using pcms.Models.DTOs;

namespace pcms.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly PcmsDbContext _db;

        public DepartmentController()
        {
            _db = new PcmsDbContext();
        }

        private DepartmentDto BuildViewModel(DepartmentDto dto = null)
        {
            if (dto == null) dto = new DepartmentDto();
            dto.Departments = _db.Departments
                .Where(d => d.IsActive)
                .OrderBy(d => d.DeptName)
                .Select(d => new DepartmentListItem
                {
                    DeptId = d.DeptId,
                    DeptName = d.DeptName,
                    DeptCode = d.DeptCode
                })
                .ToList();
            return dto;
        }

        // GET: /Department/Create
        public ActionResult Create()
        {
            return View(BuildViewModel());
        }

        // POST: /Department/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DepartmentDto dto)
        {
            if (!ModelState.IsValid)
                return View(BuildViewModel(dto));

            bool codeExists = _db.Departments
                .Any(d => d.DeptCode == dto.DeptCode && d.IsActive);

            if (codeExists)
            {
                ModelState.AddModelError("DeptCode", "A department with this code already exists.");
                return View(BuildViewModel(dto));
            }

            var department = new Department
            {
                DeptName = dto.DeptName.Trim(),
                DeptCode = dto.DeptCode.Trim().ToUpper(),
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            _db.Departments.Add(department);
            _db.SaveChanges();

            TempData["SuccessMessage"] = $"Department '{department.DeptName}' created successfully.";
            return RedirectToAction("Create");
        }

        // POST: /Department/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var dept = _db.Departments.Find(id);
            if (dept == null)
                return HttpNotFound();

            dept.IsActive = false;
            _db.Entry(dept).State = EntityState.Modified;
            _db.SaveChanges();

            TempData["SuccessMessage"] = $"Department '{dept.DeptName}' deleted successfully.";
            return RedirectToAction("Create");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _db.Dispose();
            base.Dispose(disposing);
        }
    }
}