using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Services
{
    public interface IService<T>
    {
        Task<List<T>> GetAll();
        Task<T> GetOneById(int id);
    }
}
