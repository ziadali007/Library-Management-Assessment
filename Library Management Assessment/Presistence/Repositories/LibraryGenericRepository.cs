using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Presistence.Repositories
{
    public class LibraryGenericRepository<T> : IGenericRepository<T> where T : BaseEntity, IHasName
    {
        private readonly LibraryDbContext _libraryDb;
        public LibraryGenericRepository(LibraryDbContext libraryDb) 
        {
           _libraryDb = libraryDb;
        }
        public IQueryable<T> AsQueryable()
        {
            return _libraryDb.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _libraryDb.Set<T>().AsNoTracking().ToListAsync();
        }


        public async Task<T?> GetByIdAsync(int id)
        {
            return await _libraryDb.Set<T>().FindAsync(id);
        }
        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _libraryDb.Set<T>();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.Where(predicate).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _libraryDb.Set<T>().Where(predicate).ToListAsync();
        }
        public async Task AddAsync(T entity)
        {
            await _libraryDb.Set<T>().AddAsync(entity);
        }
        public void Update(T entity)
        {
            _libraryDb.Set<T>().Update(entity);
        }
        public void Delete(T entity)
        {
            _libraryDb.Set<T>().Remove(entity);
        }


   
    }
}
