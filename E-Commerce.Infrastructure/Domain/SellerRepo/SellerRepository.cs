using E_Commerce.Domain.Model.Seller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Domain.SellerRepo
{
    public class SellerRepository : GenericRepository<Seller, SellerId>, ISellerRepository
    {
        public SellerRepository(DbContextClass context) : base(context)
        {
        }
    }
}
