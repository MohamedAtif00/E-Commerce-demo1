using E_Commerce.Domain.Common.Persistent.UnitOfWork;
using E_Commerce.Domain;
using MediatR;
using E_Commerce.Application.Common;
using Ardalis.Result;


namespace E_Commerce.Application.RootCategory.AddRootCategory
{
    public class AddRootCategoryCommandHandler : ICommandHandler<AddRootCategoryCommand,Guid>
    {
        public readonly IUnitOfWork _unitOfWork;

        public AddRootCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(AddRootCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var category = await _unitOfWork.RootCategoryRepository.Add(Domain.Model.RootCategory.RootCategory.Create(request.name));

                _unitOfWork.save();
                return Result.Success(category.Id.value);

            } catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }

        }
    }
}
