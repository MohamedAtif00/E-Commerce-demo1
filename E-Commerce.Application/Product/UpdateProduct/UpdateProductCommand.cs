using E_Commerce.Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Product.UpdateProduct
{
    public record UpdateProductCommand(Guid productId,string productName ,string description,decimal price):ICommand<Domain.Model.Product.Product>;
    
    
}
