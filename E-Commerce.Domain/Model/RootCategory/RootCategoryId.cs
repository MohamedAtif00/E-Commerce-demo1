using E_Commerce.Domain.Common.Persistent;
using E_Commerce.Domain.Model.Product;

namespace E_Commerce.Domain.Model.RootCategory
{
    public class RootCategoryId : ValueObjectId
    {
        
        public RootCategoryId(Guid value) : base(value) 
        {
        }
        public static RootCategoryId CreateUnique()
        {
            return new(Guid.NewGuid());
        }
        public static RootCategoryId Create(Guid value)
        {
            return new(value);
        }

    }
}