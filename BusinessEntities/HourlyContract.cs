using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class HourlyContract : IContract
    {
        private decimal _salary;

        public HourlyContract(decimal salary)
        {
            _salary = salary;
        }
        public decimal CalculateSalary()
        {
            return 120 * _salary * 12;
        }
    }
}
