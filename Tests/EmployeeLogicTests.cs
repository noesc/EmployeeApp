using BusinessEntities;
using BusinessEntities.Exceptions;
using BusinessLogic;
using BusinessLogic.DTOs;
using Data.Services;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class EmployeeLogicTests
    {
        private const decimal HOURLYSALARY = 70000;
        private const decimal MONTHLYSALARY = 90000;

        private Mock<IService<Employee>> _employeeServiceMock;

        public EmployeeLogicTests()
        {
            _employeeServiceMock = new Mock<IService<Employee>>();
        }

        [Fact]
        public void get_one_by_id_invalid()
        {
            // arrange
            _employeeServiceMock.Setup(x => x.GetOneById(It.IsAny<int>()))
                .Returns((Task<Employee>)null);
            EmployeeLogic employeeLogic = new EmployeeLogic(_employeeServiceMock.Object);

            // act & assert
            Assert.ThrowsAsync<NotFoundException>(() => employeeLogic.GetOneById(It.IsAny<int>()));
        }

        [Fact]
        public void get_one_by_id_valid()
        {
            // arrange
            Employee employee = new Employee()
            {
                Id = 1,
                Name = "Anne Smith",
                ContractTypeName = "HourlySalaryEmployee",
                RoleId = 1,
                RoleName = "",
                RoleDescription = "",
                HourlySalary = 70000,
                MonthlySalary = 0,
            };

            _employeeServiceMock.Setup(x => x.GetOneById(It.IsAny<int>()))
                .ReturnsAsync(employee);
            EmployeeLogic employeeLogic = new EmployeeLogic(_employeeServiceMock.Object);

            // act
            var employeeDTO = employeeLogic.GetOneById(It.IsAny<int>());

            //assert
            Assert.NotNull(employee);
        }

        [Fact]
        public void get_one_by_id_map_hourly_contract()
        {
            // arrange
            Employee employee = new Employee()
            {
                Id = 1,
                Name = "Anne Smith",
                ContractTypeName = "HourlySalaryEmployee",
                RoleId = 1,
                RoleName = "",
                RoleDescription = "",
                HourlySalary = HOURLYSALARY,
                MonthlySalary = 0,
            };

            _employeeServiceMock.Setup(x => x.GetOneById(It.IsAny<int>()))
                .ReturnsAsync(employee);
            EmployeeLogic employeeLogic = new EmployeeLogic(_employeeServiceMock.Object);

            // act
            var employeeDTO = employeeLogic.GetOneById(It.IsAny<int>());

            //assert
            var salary = 120 * employee.HourlySalary * 12;
            Assert.Equal(salary,employeeDTO.Result.Salary);
        }

        [Fact]
        public void get_one_by_id_map_monthly_contract()
        {
            // arrange
            Employee employee = new Employee()
            {
                Id = 1,
                Name = "Anne Smith",
                ContractTypeName = "MonthlySalaryEmployee",
                RoleId = 1,
                RoleName = "",
                RoleDescription = "",
                HourlySalary = 0,
                MonthlySalary = MONTHLYSALARY,
            };

            _employeeServiceMock.Setup(x => x.GetOneById(It.IsAny<int>()))
                .ReturnsAsync(employee);
            EmployeeLogic employeeLogic = new EmployeeLogic(_employeeServiceMock.Object);

            // act
            var employeeDTO = employeeLogic.GetOneById(It.IsAny<int>());

            //assert
            var salary = employee.MonthlySalary * 12;
            Assert.Equal(salary, employeeDTO.Result.Salary);
        }
    }
}
