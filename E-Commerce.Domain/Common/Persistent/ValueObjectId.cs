using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Common.Persistent
{
    public class ValueObjectId : ValueObject
    {
        public Guid value { get;private set; }

        protected ValueObjectId(Guid id)
        {
            value = id;
        }

        public static ValueObjectId Create(Guid id)
        {
            return new(id);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return value;
        }
    }
}
