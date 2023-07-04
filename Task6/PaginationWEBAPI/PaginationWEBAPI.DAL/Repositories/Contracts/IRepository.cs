using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaginationWEBAPI.DAL.Repositories.Contracts
{
    public interface IRepository<T> where T : class
    {
        public Task<T> GetByIdAsync(int id);
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<T> InsertAsync(T entity);
        public Task Update(T entity);
        public Task<T> DeleteAsync(int id);
        public Task<int> Save();

    }
}
