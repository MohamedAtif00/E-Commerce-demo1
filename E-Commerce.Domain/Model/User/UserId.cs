using E_Commerce.Domain.Common.Persistent;
using E_Commerce.Domain.Model.Role;

namespace E_Commerce.Domain.Model.User
{
    public class UserId:ValueObjectId
    {
        private UserId(Guid value) : base(value)
        {
        }

        public static UserId CreateUnique()
        {
            return new(Guid.NewGuid());
        }
        public static UserId Create(Guid value)
        {
            return new(value);
        }
    }
}