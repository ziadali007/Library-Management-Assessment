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
    public class IdentityUnitOfWork : IUnitOfWork
    {
        private readonly LibraryIdentityDbContext _identityDb;
        private readonly ConcurrentDictionary<Type, object> _repositories;

        public IdentityUnitOfWork(LibraryIdentityDbContext identityDb)
        {
            _identityDb = identityDb;
            _repositories = new ConcurrentDictionary<Type, object>();
        }
        public IGenericRepository<T> GetRepository<T>() where T : BaseEntity, IHasName
        {
           return (IGenericRepository<T>)_repositories.GetOrAdd(typeof(T), (type) => new IdentityGenericRepository<T>(_identityDb));
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _identityDb.SaveChangesAsync();
        }
    }
}
