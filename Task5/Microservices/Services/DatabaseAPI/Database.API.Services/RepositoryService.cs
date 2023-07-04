using Database.API.Infrastructure;
using Database.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.API.Services
{
    public class RepositoryService<T> : IRepositoryService<T> where T : class
    {
        private readonly DataContext _dataContext;

        public RepositoryService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dataContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _dataContext.Set<T>().FindAsync(id);
        }

        public async Task Insert(T entity)
        {
            await _dataContext.Set<T>().AddAsync(entity);
        }

        public Task Update(T entity)
        {
            _dataContext.Entry(entity).State = EntityState.Modified;
            return Task.CompletedTask;
        }
        public Task Delete(T entity)
        {
            _dataContext.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }

        public async Task CommitAsync(bool state = true)
        {
            await _dataContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dataContext.Dispose();
        }      

    }
}
