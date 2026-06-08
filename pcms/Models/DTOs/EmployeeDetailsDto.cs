using System.ComponentModel.DataAnnotations;

namespace pcms.Models.DTOs
{
    public class EmployeeDetailsDto
    {
        [Required(ErrorMessage = "Employee number is required.")]
        [StringLength(20, ErrorMessage = "Employee number cannot exceed 20 characters.")]
        [Display(Name = "Emp. Number")]
        public string EmpNumber { get; set; }

        [Required(ErrorMessage = "Employee name is required.")]
        [StringLength(150, ErrorMessage = "Employee name cannot exceed 150 characters.")]
        [Display(Name = "Emp. Name")]
        public string EmpName { get; set; }

        [Required(ErrorMessage = "Designation is required.")]
        [StringLength(100, ErrorMessage = "Designation cannot exceed 100 characters.")]
        [Display(Name = "Designation")]
        public string Designation { get; set; }

        [Required(ErrorMessage = "Department is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid department.")]
        [Display(Name = "Dept Code")]
        public int DeptCode { get; set; }
    }
}