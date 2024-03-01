using E_Commerce.Application.Common;
using E_Commerce.Domain.Model.Product;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Product.GetProductById
{
    public record GetProductByIdQuery : IQuery<Domain.Model.Product.Product>
    {
        public Guid Id { get; set; }
    }
}
