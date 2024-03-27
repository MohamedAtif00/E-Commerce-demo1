using E_Commerce.Domain.Common.Persistent;
using E_Commerce.Domain.Model.Product;

namespace E_Commerce.Domain.Model.Token
{
    public class RefreshTokenId : ValueObjectId
    {
        protected RefreshTokenId(Guid id) : base(id)
        {
        }
        public static RefreshTokenId CreateUnique()
        {
            return new(Guid.NewGuid());
        }
        public static RefreshTokenId Create(Guid value)
        {
            return new(value);
        }
    }
}