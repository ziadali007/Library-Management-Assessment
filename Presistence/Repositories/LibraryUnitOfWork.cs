using Domain.Contracts;
using Domain.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence.Repositories
{
    public class LibraryUnitOfWork : IUnitOfWork
    {
        private readonly LibraryDbContext _libraryDb;
        private readonly ConcurrentDictionary<Type, object> _repositories;
        public IGenericRepository<T> GetRepository<T>() where T : BaseEntity, IHasName
        {
            return (IGenericRepository<T>)_repositories.GetOrAdd(typeof(T), (type) => new LibraryGenericRepository<T>(_libraryDb));
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _libraryDb.SaveChangesAsync();
        }
    }
}
