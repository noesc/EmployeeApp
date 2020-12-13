using BusinessLogic.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public interface IEmployeeLogic
    {
        Task<List<EmployeeDto>> GetAll();
        Task<EmployeeDto> GetOneById(int id);
    }
}
