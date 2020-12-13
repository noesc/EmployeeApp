using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace Data.Services
{
    public class EmployeeService : IService<Employee>
    {
        private List<Employee> _employees;
        private IMemoryCache _cache;
        private IConfiguration _configuration;

        public EmployeeService(IMemoryCache memoryCache, IConfiguration configuration)
        {
            _cache = memoryCache;
            _configuration = configuration;
        }

        private void SaveInCache()
        {
             var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(60));

            _cache.Set("_EmployeeList", _employees, TimeSpan.FromSeconds(60));
        }

        private async Task<List<Employee>> RefreshEmployees()
        {
            var url = _configuration.GetSection("EmployeeAPI:URL").Value;

            try
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(url))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        _employees = JsonConvert.DeserializeObject<List<Employee>>(apiResponse);
                        SaveInCache();
                    }
                    return _employees;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Employee>> GetAll()
        {
            List<Employee> employees = new List<Employee>();
            _employees = _cache.Get<List<Employee>>("_EmployeeList");
            if (_employees != null)
                return _employees;

            return await RefreshEmployees();
        }

        public async Task<Employee> GetOneById(int id)
        {
            try
            {
                _employees = _cache.Get<List<Employee>>("_EmployeeList");

                if (!_employees.Any(x => x.Id == id))
                    await RefreshEmployees();

                var employee = _employees.FirstOrDefault(x => x.Id == id);
                return employee;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
