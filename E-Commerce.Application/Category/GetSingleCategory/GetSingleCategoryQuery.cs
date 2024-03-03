using E_Commerce.Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Category.GetSingleCategory
{
    public record GetSingleCategoryQuery(Guid id):IQuery<Domain.Model.Category.Category>;
    
    
}
