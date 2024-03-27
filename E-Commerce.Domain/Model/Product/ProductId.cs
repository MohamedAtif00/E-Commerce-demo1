using E_Commerce.Domain.Common.Persistent;

namespace E_Commerce.Domain.Model.Product
{
    public class ProductId :ValueObjectId
    {
        private ProductId(Guid value): base(value)
        {
        }
        public static ProductId CreateUnique()
        {
            return new(Guid.NewGuid());
        }
        public static ProductId Create(Guid value)
        {
            return new(value);
        }

    }
}