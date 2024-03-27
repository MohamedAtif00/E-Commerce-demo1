using E_Commerce.Domain.Common.Persistent;
using E_Commerce.Domain.Model.Product;

namespace E_Commerce.Domain.Model.Category
{
    public sealed class CategoryId : ValueObjectId
    {
        private CategoryId(Guid value):base(value)
        {
        }
        public static CategoryId CreateUnique()
        {
            return new(Guid.NewGuid());
        }
        public static CategoryId Create(Guid value)
        {
            return new(value);
        }
        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return value;
        }
    }
}