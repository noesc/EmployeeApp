using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Factories
{
    public class HourlyContractFactory : ContractFactory
    {
        private decimal _salary;
        public HourlyContractFactory(decimal salary)
        {
            _salary = salary;
        }
        public override IContract GetContract()
        {
            return new HourlyContract(_salary);    
        }
    }
}
