using E_Commerce.Domain.Common.Persistent;

namespace E_Commerce.Domain.Model.Category
{
    public class VisitorId : ValueObjectId
    {
        public Guid value { get; }
        public VisitorId(Guid value):base(value)
        {
            this.value = value;
        }
        public static VisitorId CreateUnique()
        {
            return new(Guid.NewGuid());
        }
        public static VisitorId Create(Guid value)
        {
            return new(value);
        }
        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return value;
        }
    }
}