using E_Commerce.Domain.Common.Persistent.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Result;
using E_Commerce.Application.Common;

namespace E_Commerce.Application.Product.GetProductById
{
    public record GetProductByIdQueryHandler : IQueryHandler<GetProductByIdQuery,Domain.Model.Product.Product>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProductByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<Result<Domain.Model.Product.Product>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.ProductRepository.GetById(request.Id);

            return product == null ?Result.NotFound() :Result.Success(product);
        }
    }
}
