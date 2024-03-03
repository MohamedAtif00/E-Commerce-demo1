using Ardalis.Result;
using E_Commerce.Application.Common;
using E_Commerce.Domain.Common.Persistent.UnitOfWork;
using cate = E_Commerce.Domain.Model.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.Domain.Model.Category;

namespace E_Commerce.Application.Category.AddCategoryForChild
{
    public class AddCategoryForChildCommandHandler : ICommandHandler<AddCategoryForChildCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddCategoryForChildCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(AddCategoryForChildCommand request, CancellationToken cancellationToken)
        {
            try 
            {
                var result = await _unitOfWork.CategoryRepository.Add(cate.Category.CreateChildCategory(request.categoryName,CategoryId.Create(request.categoryId)));

                if (result == null) return Result.CriticalError("Category didn't be createdd");

                await _unitOfWork.save();

                return Result.Success(true);
            }catch (Exception ex) 
            {
                return Result.CriticalError(ex.Message);
            }
        }
    }
}
