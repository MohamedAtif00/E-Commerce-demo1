using E_Commerce.Domain.Model.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Domain.ProductRepo
{
    public class ProductRepository : GenericRepository<Product, ProductId>,IProductRepository
    {
        public ProductRepository(DbContextClass context) : base(context)
        {
        }

        
    }
}
