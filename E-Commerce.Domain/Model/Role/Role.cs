using E_Commerce.Domain.Common.Persistent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Model.Role
{
    public class Role : AggregateRoot<RoleId>
    {
        public MainRole role { get;private set; }
        protected Role(RoleId id,MainRole role) : base(id)
        {
            this.role = role;
        }

        public static Role CreateUser()
        {
            return new(RoleId.CreateUnique(),MainRole.User());
        }
        public static Role CreateSeller()
        {
            return new(RoleId.CreateUnique(), MainRole.Seller());
        }


    }
}
