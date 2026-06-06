using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pcms.Models
{
    [Table("Departments")]
    public class Department
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DeptId { get; set; }

        [Required(ErrorMessage = "Department Name is required.")]
        [StringLength(100, ErrorMessage = "Department Name cannot exceed 100 characters.")]
        [Display(Name = "DEPT NAME")]
        public string DeptName { get; set; }

        [Required(ErrorMessage = "Department Code is required.")]
        [StringLength(20, ErrorMessage = "Department Code cannot exceed 20 characters.")]
        [Display(Name = "DEPT CODE")]
        public string DeptCode { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool IsActive { get; set; } = true;
    }
}