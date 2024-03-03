using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Common.Persistent
{
    public class AggregateRoot<TId> :  Entity<TId>,IHasDomainEvents
        where TId : notnull,ValueObjectId
    {
        protected static readonly List<IDomainEvent> _domainEvents = new();
        protected AggregateRoot(TId id) : base(id)
        {
        }

        IReadOnlyList<IDomainEvent> IHasDomainEvents.DomainEvents => _domainEvents;

        public void AddDomainEvent(IDomainEvent domainEvents)
        {
            _domainEvents.Add(domainEvents);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

    }
}
