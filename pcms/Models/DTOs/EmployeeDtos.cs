using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace pcms.Models.DTOs
{
    public class EmployeeDtos
    {
        [Required(ErrorMessage = "Employee Number is Required.")]
        public string EmployeeNumber { get; set; }  
        [Required(ErrorMessage = "Password is Required.")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}