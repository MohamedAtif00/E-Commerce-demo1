using Ardalis.Result;
using E_Commerce.Application.Common;
using E_Commerce.Domain.Common.Persistent.UnitOfWork;
using E_Commerce.Domain.Model.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Category.UpdateCategory
{
    public class UpdateCategoryCommandHandler : ICommandHandler<UpdateCategoryCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            try 
            {
                var category = await _unitOfWork.CategoryRepository.GetById(CategoryId.Create(request.categoryId));

                category.Update(request.name);

                var result = await _unitOfWork.CategoryRepository.Update(category);

                if (result == null) return Result.CriticalError("this Action didn't Complete");

                await _unitOfWork.save();

                return Result.Success(true);
            }catch (Exception ex) 
            {
                return Result.CriticalError(ex.Message);
            }
        }
    }
}
