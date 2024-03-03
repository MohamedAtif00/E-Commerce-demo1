using E_Commerce.Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.RootCategory.ChangeRootCategoryName
{
    public record ChangeRootCategoryNameCommand(Guid id,string name) : ICommand<bool>;
    
}
