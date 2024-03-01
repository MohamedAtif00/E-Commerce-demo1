using E_Commerce.Domain.Model.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Domain.CategoryRepo
{
    public class CategoryRepository : GenericRepository<Category, CategoryId>,ICategoryRepository
    {
        public CategoryRepository(DbContextClass context) : base(context)
        {
        }
    }
}
