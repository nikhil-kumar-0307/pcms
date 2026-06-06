using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace pcms.Models.DTOs
{
    public class DepartmentListItem
    {
        public int DeptId { get; set; }
        public string DeptName { get; set; }
        public string DeptCode { get; set; }
    }

    public class DepartmentDto
    {
        public int DeptId { get; set; }

        [Required(ErrorMessage = "Department Name is required.")]
        [StringLength(100, ErrorMessage = "Department Name cannot exceed 100 characters.")]
        [Display(Name = "DEPT NAME")]
        public string DeptName { get; set; }

        [Required(ErrorMessage = "Department Code is required.")]
        [StringLength(20, ErrorMessage = "Department Code cannot exceed 20 characters.")]
        [Display(Name = "DEPT CODE")]
        public string DeptCode { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool IsActive { get; set; }

        // List of all active departments shown below the form
        public List<DepartmentListItem> Departments { get; set; } = new List<DepartmentListItem>();
    }
}