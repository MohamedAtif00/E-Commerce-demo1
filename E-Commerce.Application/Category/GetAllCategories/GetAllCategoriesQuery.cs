using E_Commerce.Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Category.GetAllCategories
{
    public record GetAllCategoriesQuery():IQuery<List<Domain.Model.Category.Category>>;
    
    
}
