using E_Commerce.Domain.Common.Persistent;
using E_Commerce.Domain.Common.Persistent.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Domain
{
    public class GenericRepository<T, TKey> : IGenericRepository<T, TKey> 
        where T : Entity<TKey>
        where TKey : notnull,ValueObjectId
    {
        public readonly DbContextClass _context;

        public GenericRepository(DbContextClass context)
        {
            _context = context;
        }

        public virtual async Task<T> Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        public virtual async Task Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public virtual async Task<List<T>> GetAll()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        public virtual async Task<T> GetById(Guid id)
        {

            return await _context.Set<T>().FirstOrDefaultAsync(e => EF.Property<TKey>(e, nameof(Entity<TKey>.Id.value))!.Equals(id));
        }

        public virtual Task<T> Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
