using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class MonthlyContract : IContract
    {
        private decimal _salary;

        public MonthlyContract(decimal salary)
        {
            _salary = salary;
        }
        public decimal CalculateSalary()
        {
            return _salary * 12;
        }
    }
}
