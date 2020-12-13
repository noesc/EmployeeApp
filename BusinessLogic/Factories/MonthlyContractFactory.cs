using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Factories
{
    public class MonthlyContractFactory : ContractFactory
    {
        private decimal _salary;

        public MonthlyContractFactory(decimal salary)
        {
            _salary = salary;
        }
        public override IContract GetContract()
        {
            return new MonthlyContract(_salary);  
        }
    }
}
