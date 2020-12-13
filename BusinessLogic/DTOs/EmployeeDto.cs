using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTOs
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContractType { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public decimal Salary { get; set; }
    }
}
