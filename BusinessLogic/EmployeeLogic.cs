using BusinessEntities;
using BusinessEntities.Exceptions;
using BusinessLogic.DTOs;
using BusinessLogic.Factories;
using Data.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class EmployeeLogic : IEmployeeLogic
    {
        private IService<Employee> _service;
        private ContractFactory _contractFactory;
        public EmployeeLogic(IService<Employee> service)
        {
            _service = service;
        }
        public async Task<List<EmployeeDto>> GetAll()
        {
            try
            {
                var employees = await _service.GetAll();
                List<EmployeeDto> dtos = new List<EmployeeDto>();

                foreach (var emp in employees)
                {
                    EmployeeDto dto = Map(emp);
                    dtos.Add(dto);
                }

                return dtos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<EmployeeDto> GetOneById(int id)
        {
            try
            {
                var employee = await _service.GetOneById(id);
                if (employee == null)
                    throw new NotFoundException($"Employee with ID {id} not found");
                return Map(employee);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private EmployeeDto Map(Employee employee)
        {
            EmployeeDto dto = new EmployeeDto();
            dto.Id = employee.Id;
            dto.Name = employee.Name;
            dto.ContractType = employee.ContractTypeName;
            dto.RoleName = employee.RoleName;
            dto.RoleDescription = employee.RoleDescription ?? "-";

            switch (employee.ContractTypeName)
            {
                case "MonthlySalaryEmployee":
                    _contractFactory = new MonthlyContractFactory(employee.MonthlySalary);
                    break;
                case "HourlySalaryEmployee":
                    _contractFactory = new HourlyContractFactory(employee.HourlySalary);
                    break;
                default:
                    break;
            }

            IContract _contract = _contractFactory.GetContract();
            dto.Salary = _contract.CalculateSalary();

            return dto;
        }
    }
}
