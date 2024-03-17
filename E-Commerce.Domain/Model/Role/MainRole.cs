using E_Commerce.Domain.Common.Persistent;

namespace E_Commerce.Domain.Model.Role
{
    public class MainRole : ValueObject
    {
        private string _roleName { get; set; }
        public MainRole(string Role) 
        {
            
        }
        public static MainRole User() => new("user");
        public static MainRole Seller() => new("seller");
        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return _roleName;
        }
    }
}