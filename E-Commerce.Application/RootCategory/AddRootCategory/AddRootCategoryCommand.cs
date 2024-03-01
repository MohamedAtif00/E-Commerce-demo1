using E_Commerce.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace E_Commerce.Application.RootCategory.AddRootCategory
{
    public record AddRootCategoryCommand(string name) : ICommand<Guid>;


}
