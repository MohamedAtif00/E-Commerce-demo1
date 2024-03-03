using Ardalis.Result;
using E_Commerce.Application.Common;
using E_Commerce.Domain.Common.Persistent.UnitOfWork;
using E_Commerce.Domain.Model.Category;
using Cate = E_Commerce.Domain.Model.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.Domain.Model.RootCategory;

namespace E_Commerce.Application.Category.AddCategoryForRoot
{
    public class AddCategoryForRootCommandHandler : ICommandHandler<AddCategoryForRootCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddCategoryForRootCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(AddCategoryForRootCommand request, CancellationToken cancellationToken)
        {
            try 
            {
                var result = await _unitOfWork.CategoryRepository.Add(Cate.Category.CreateRootCategory(request.CategoryName,RootCategoryId.Create(request.RootCategoryId)));
                if (result == null) return Result.Conflict("category didn't created");

                await _unitOfWork.save();
                return Result.Success(true);
            }catch (Exception ex) 
            {
                return Result.CriticalError(ex.Message);
            }

        }
    }
}
