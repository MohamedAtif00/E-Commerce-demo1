using E_Commerce.Domain.Common.Persistent;
using E_Commerce.Domain.Model.Category;

namespace E_Commerce.Domain.Model.User
{
    public class ApplicationUserId : ValueObject
    {
        public Guid value { get; }
        public ApplicationUserId(Guid value)
        {
            this.value = value;
        }
        public static ApplicationUserId CreateUnique()
        {
            return new(Guid.NewGuid());
        }
        public static ApplicationUserId Create(Guid value)
        {
            return new(value);
        }
        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return value;
        }
    }
}