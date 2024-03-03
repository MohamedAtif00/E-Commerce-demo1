using E_Commerce.Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Category.AddCategoryForRoot
{
    public record AddCategoryForRootCommand(Guid RootCategoryId,string CategoryName):ICommand<bool>;
    
    
}
