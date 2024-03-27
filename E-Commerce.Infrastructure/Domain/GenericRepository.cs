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
        where T : AggregateRoot<TKey>
        where TKey :ValueObjectId
    {
        public readonly DbContextClass _context;

        public GenericRepository(DbContextClass context)
        {
            _context = context;
        }

        public virtual async Task<T> Add(T entity)
        {
            var res = await _context.Set<T>().AddAsync(entity);
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

        public virtual async Task<T> GetById(TKey id)
        {

            //return await _context.Set<T>().FirstOrDefaultAsync(e => EF.Property<TKey>(e, nameof(AggregateRoot<TKey>.Id.value)).Equals(id.value));
            //return await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(e => EF.Property<TKey>(e, nameof(AggregateRoot<TKey>.Id))!.Equals(id)) ?? null;
            return await _context.Set<T>().FindAsync(id);
        }



        public virtual async Task<T> Update(T entity)
        {
            var entry =  _context.Set<T>().Update(entity);
            return entry.Entity;
        }
    }
}
