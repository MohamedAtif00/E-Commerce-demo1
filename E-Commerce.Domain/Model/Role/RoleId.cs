using E_Commerce.Domain.Common.Persistent;
using E_Commerce.Domain.Model.Product;

namespace E_Commerce.Domain.Model.Role
{
    public class RoleId : ValueObjectId
    {
        protected RoleId(Guid id) : base(id)
        {
        }

        public static RoleId CreateUnique()
        {
            return new(Guid.NewGuid());
        }
        public static RoleId Create(Guid value)
        {
            return new(value);
        }
    }
}