using E_Commerce.Domain.Common.Persistent;
using E_Commerce.Domain.Model.Product;

namespace E_Commerce.Domain.Model.Seller
{
    public class SellerId : ValueObjectId
    {
        public SellerId():base(Guid.NewGuid())
        { }
        private SellerId(Guid id) : base(id)
        {
        }

        public static SellerId CreateUnique()
        {
            return new(Guid.NewGuid());
        }
        public static SellerId Create(Guid value)
        {
            return new(value);
        }
    }
}