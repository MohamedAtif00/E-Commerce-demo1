using E_Commerce.Domain.Common.Persistent;
using E_Commerce.Domain.Model.Product;
using E_Commerce.Domain.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Model.Seller
{
    public class Seller : AggregateRoot<SellerId>
    {
        public UserId UserId { get;private set; }
        protected Seller(SellerId id,UserId userId) : base(id)
        {

        }

        public static Seller Create(UserId userId)
        {
            return new(SellerId.CreateUnique(),userId);
        }
    }
}
