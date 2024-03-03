using E_Commerce.Application.Common;
using E_Commerce.Domain.Model.RootCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.RootCategory.GetSingleRootCategoryQuery
{
    public record GetSingleRootCategoryQuery(Guid id):IQuery<Domain.Model.RootCategory.RootCategory>;
    
    
}
