using Ardalis.Result;
using E_Commerce.Application.Common;
using E_Commerce.Domain.Common.Persistent.UnitOfWork;
using E_Commerce.Domain.Model.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Product.RemoveProduct
{
    public class RemoveProductCommandHandler : ICommandHandler<RemoveProductCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RemoveProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(RemoveProductCommand request, CancellationToken cancellationToken)
        {
            try 
            {
                var product = await _unitOfWork.ProductRepository.GetById(ProductId.Create(request.id)) ;

                if (product == null) return Result.NotFound("this product is not exist");

                await _unitOfWork.ProductRepository.Delete(product);

                await _unitOfWork.save();

                return Result.Success(true);
            }catch (Exception ex) 
            {
                return Result.CriticalError(ex.Message);
            }
        }
    }
}
