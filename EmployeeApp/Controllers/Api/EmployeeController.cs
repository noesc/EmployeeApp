
using System.Threading.Tasks;
using BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace EmployeeApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private IEmployeeLogic _employeeLogic;

        public EmployeeController(IEmployeeLogic employeeLogic)
        {
            _employeeLogic = employeeLogic;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var employees = await _employeeLogic.GetAll();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetOneById(int id)
        {
            var employee = await _employeeLogic.GetOneById(id);
            return Ok(employee);
        }
    }
}