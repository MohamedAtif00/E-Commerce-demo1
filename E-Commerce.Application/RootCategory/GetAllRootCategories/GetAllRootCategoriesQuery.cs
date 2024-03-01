using E_Commerce.Application.Common;
using E_Commerce.Domain.Model.RootCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.RootCategory.GetAllRootCategories
{
    public record GetAllRootCategoriesQuery() :IQuery<List<Domain.Model.RootCategory.RootCategory>>;
    
    
}
