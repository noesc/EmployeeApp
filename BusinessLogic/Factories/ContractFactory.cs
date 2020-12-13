using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Factories
{
    public abstract class ContractFactory
    {
        public abstract IContract GetContract();
    }
}
