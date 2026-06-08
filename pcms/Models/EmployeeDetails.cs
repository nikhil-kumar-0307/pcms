using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pcms.Models
{
    [Table("EmployeeDetails")]
    public class EmployeeDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        [Column("EmpNumber")]
        public string EmpNumber { get; set; }

        [Required]
        [StringLength(150)]
        [Column("EmpName")]
        public string EmpName { get; set; }

        [Required]
        [StringLength(100)]
        [Column("Designation")]
        public string Designation { get; set; }

        [Required]
        [Column("DeptCode")]
        public int DeptCode { get; set; }

        [Column("CreatedAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("IsActive")]
        public bool IsActive { get; set; } = true;
    }
}