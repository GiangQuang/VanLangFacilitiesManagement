using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VLFM.Core.Interfaces;

namespace VLFM.Infrastructure.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DataContext _dataContext;
        protected GenericRepository(DataContext dataContext)
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
        public async Task Add(T entity)
        {
            await _dataContext.Set<T>().AddAsync(entity);
        }
        public void Delete(T entity)
        {
            _dataContext.Set<T>().Remove(entity);
        }
        public void Update(T entity)
        {
            _dataContext.Set<T>().Update(entity);
        }
    }
}
