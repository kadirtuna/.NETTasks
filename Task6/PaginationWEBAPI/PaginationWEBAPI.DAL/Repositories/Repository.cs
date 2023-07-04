using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PaginationWEBAPI.DAL.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaginationWEBAPI.DAL.Repositories
{
    public class Repository<T> : IRepository<T>, IUnitOfWork where T : class
    {
        private readonly DataContext _dataContext;
        //private readonly IDbContextTransaction transaction = null;

        public Repository(DataContext dataContext)
        {
            _dataContext = dataContext;
            //transaction = _dataContext.Database.BeginTransaction();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dataContext.Set<T>().FindAsync(id); 
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dataContext.Set<T>().ToListAsync();
        }

        public async Task<T> InsertAsync(T entity)
        {
            await _dataContext.Set<T>().AddAsync(entity);
            await _dataContext.SaveChangesAsync();

            return entity;
        }

        public Task Update(T entity)
        {
            _dataContext.Entry(entity).State = EntityState.Modified;

            return Task.CompletedTask;
        }

        public async Task<T> DeleteAsync(int id)
        {
            var entity = await _dataContext.Set<T>().FindAsync(id);

            if (entity == null)
            {
                return entity;
            }

            _dataContext.Set<T>().Remove(entity);

            return entity;
        }

        public async Task<int> Save()
        {
            return await _dataContext.SaveChangesAsync();
        }

        public async Task CommitAsync(bool state = true)
        {
            await Save();

            //if(state)
            //    transaction.Commit();
            //else
            //    transaction.Rollback();

            //Dispose();
        }

        public void Dispose()
        {
            _dataContext.Dispose();
        }

    }
}
