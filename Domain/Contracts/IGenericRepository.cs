using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IGenericRepository<T> where T : BaseEntity, IHasName
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> FindByNameAsync(string name);
        Task<T?> GetByIdAsync(int id);

        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
