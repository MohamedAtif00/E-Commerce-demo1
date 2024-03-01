using E_Commerce.Domain.Common.Persistent.UnitOfWork;
using E_Commerce.Domain.Model.Product;
using MediatR;

namespace E_Commerce.Application.Product.AddProduct
{
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand>
    {
        public readonly IUnitOfWork _unitOfWork;

        public AddProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.ProductRepository.Add(Domain.Model.Product.Product.Create(
                                                                                        request.CategoryId,
                                                                                        request.name,
                                                                                        request.description,
                                                                                        request.price));

        }
    }
}
