using Ardalis.Result;
using E_Commerce.Application.Common;
using E_Commerce.Domain.Common.Persistent.UnitOfWork;
using E_Commerce.Domain.Model.Product;
using MediatR;

namespace E_Commerce.Application.Product.AddProduct
{
    public class AddProductCommandHandler : ICommandHandler<AddProductCommand,Domain.Model.Product.Product>
    {
        public readonly IUnitOfWork _unitOfWork;

        public AddProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Domain.Model.Product.Product>> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            try
            { 
                var product = await _unitOfWork.ProductRepository.Add(Domain.Model.Product.Product.Create(
                                                                                            request.CategoryId,
                                                                                            request.name,
                                                                                            request.description,
                                                                                            request.price));
                var result = await  _unitOfWork.save();
                if (!(result > 0)) return Result.CriticalError("Save didn;t complete");

                await _unitOfWork.save();

                return Result.Success(product);
            }catch (Exception ex)
            {
                return Result.CriticalError(ex.Message);
            }
        }
    }
}
