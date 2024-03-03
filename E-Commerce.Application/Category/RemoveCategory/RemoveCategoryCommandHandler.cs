using Ardalis.Result;
using E_Commerce.Application.Common;
using E_Commerce.Domain.Common.Persistent.UnitOfWork;
using E_Commerce.Domain.Model.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Category.RemoveCategory
{
    public class RemoveCategoryCommandHandler : ICommandHandler<RemoveCategoryCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RemoveCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(RemoveCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var category = await _unitOfWork.CategoryRepository.GetById(CategoryId.Create(request.categoryId));

                if (category == null) return Result.NotFound("this category is not exist");

                await _unitOfWork.CategoryRepository.Delete(category);

                await _unitOfWork.save();
                return Result.Success(true);
                
            }catch (Exception ex) 
            {
                return Result.Error(ex.Message);
            }


        }
    }
}
