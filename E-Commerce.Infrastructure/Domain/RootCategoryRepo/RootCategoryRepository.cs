using E_Commerce.Domain.Model.RootCategory;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Domain.RootCategoryRepo
{
    public class RootCategoryRepository : GenericRepository<RootCategory, RootCategoryId>, IRootCategoryRepository
    {
        private readonly DbContextClass context;
        public RootCategoryRepository(DbContextClass context) : base(context)
        {
            this.context = context;
        }

       
    }
}
