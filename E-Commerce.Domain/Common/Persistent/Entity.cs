using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Common.Persistent
{
    public class Entity<TId> : IEquatable<TId>
        where TId : notnull ,ValueObjectId
    {
        
        public TId Id { get; protected set; }

        protected Entity(TId id)
        {
            Id = id;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(obj, null)) return false;
            if (ReferenceEquals(obj, this)) return true;
            return obj is Entity<TId> && Id.Equals(((Entity<TId>)obj).Id);
        }

        public bool Equals(Entity<TId>? other)
        {
            return Equals((object?)other);
        }


        public bool Equals(TId? other)
        {
            if (other is null)
                return false;

            return Id.Equals(other);
        }

        public static bool operator ==(Entity<TId> left, Entity<TId> right)
        {
            return Equals(right, left);
        }

        public static bool operator !=(Entity<TId> left, Entity<TId> right)
        {
            return !Equals(left, right);
        }

        protected void CheckRule(IBusinessRule rule)
        {
            if (rule.IsBroken())
            {
                throw new BusinessRuleValidationException(rule);
            }
        }

    }
}
