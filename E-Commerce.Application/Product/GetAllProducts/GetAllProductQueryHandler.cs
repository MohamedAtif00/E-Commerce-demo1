using Ardalis.Result;
using E_Commerce.Application.Common;
using E_Commerce.Domain.Common.Persistent.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Product.GetAllProducts
{
    internal class GetAllProductQueryHandler : IQueryHandler<GetAllProductsQuery, List<Domain.Model.Product.Product>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllProductQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<Domain.Model.Product.Product>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _unitOfWork.ProductRepository.GetAll();
            return products.Count() == 0? Result.NotFound() : Result.Success(products.ToList());
        }
    }
}
