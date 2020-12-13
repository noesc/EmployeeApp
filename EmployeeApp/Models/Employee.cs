using EmployeeApp.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeApp.Models
{
    public class Employee
    {
        [MinValue(1,ErrorMessage ="The Employee ID must be greater than 0")]
        public int Id { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Contract Type")]
        public string ContractType { get; set; }
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
        [Display(Name = "Description")]
        public string RoleDescription { get; set; }
        [Display(Name = "Salary")]
        public decimal Salary { get; set; }
    }
}
