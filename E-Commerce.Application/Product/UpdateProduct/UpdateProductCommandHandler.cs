using Ardalis.Result;
using E_Commerce.Application.Common;
using E_Commerce.Domain.Common.Persistent.UnitOfWork;
using E_Commerce.Domain.Model.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Product.UpdateProduct
{
    public class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand, Domain.Model.Product.Product>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Domain.Model.Product.Product>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            try 
            {
                var product = await _unitOfWork.ProductRepository.GetById(ProductId.Create(request.productId));

                if (product == null) return Result.NotFound("Product is not exist");

                product.Update(request.productName,request.description,request.price);

                var result = await _unitOfWork.ProductRepository.Update(product);

                if (result == null) return Result.CriticalError("Something Went Wronge");

                await _unitOfWork.save();

                return Result.Success(result);
            }catch (Exception ex) 
            {
                return Result.CriticalError(ex.Message);
            }
        }
    }
}
