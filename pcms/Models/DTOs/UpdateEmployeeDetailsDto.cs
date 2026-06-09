using System.ComponentModel.DataAnnotations;

namespace pcms.Models.DTOs
{
    public class UpdateEmployeeDetailsDto
    {
        // Hidden field — not editable, used to identify the record
        public int Id { get; set; }

        [Required]
        [Display(Name = "Employee Number")]
        public string EmpNumber { get; set; }

        [Required(ErrorMessage = "Employee name is required.")]
        [StringLength(150, ErrorMessage = "Name cannot exceed 150 characters.")]
        [Display(Name = "Employee Name")]
        public string EmpName { get; set; }

        [Required(ErrorMessage = "Designation is required.")]
        [StringLength(100, ErrorMessage = "Designation cannot exceed 100 characters.")]
        [Display(Name = "Designation")]
        public string Designation { get; set; }

        [Required(ErrorMessage = "Department is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid department.")]
        [Display(Name = "Department")]
        public int DeptCode { get; set; }

        [Display(Name = "Status")]
        public bool IsActive { get; set; }
    }
}
