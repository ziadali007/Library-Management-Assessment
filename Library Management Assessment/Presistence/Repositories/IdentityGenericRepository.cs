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
    public class IdentityGenericRepository<T> : IGenericRepository<T> where T : BaseEntity, IHasName
    {
        private readonly LibraryIdentityDbContext _identityDb;
        public IdentityGenericRepository(LibraryIdentityDbContext identityDb)
        {
            _identityDb = identityDb;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _identityDb.Set<T>().AsNoTracking().ToListAsync();
        }
        public IQueryable<T> AsQueryable()
        {
            return _identityDb.Set<T>();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _identityDb.Set<T>().FindAsync(id);
        }
        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _identityDb.Set<T>();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.Where(predicate).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _identityDb.Set<T>().Where(predicate).ToListAsync();
        }
        public async Task AddAsync(T entity)
        {
            await _identityDb.Set<T>().AddAsync(entity);
        }
        public void Update(T entity)
        {
            _identityDb.Set<T>().Update(entity);
        }
        public void Delete(T entity)
        {
            _identityDb.Set<T>().Remove(entity);
        }

    }
}
