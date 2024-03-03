using E_Commerce.Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.RootCategory.RemoveRootCategory
{
    public record RemoveRootCategoryCommand(Guid id):ICommand<bool>;
    
    
}
