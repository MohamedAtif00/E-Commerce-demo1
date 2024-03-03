using Ardalis.Result;
using E_Commerce.Application.Common;
using E_Commerce.Domain.Common.Persistent.UnitOfWork;
using E_Commerce.Domain.Model.RootCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.RootCategory.RemoveRootCategory
{
    public class RemoveRootCategoryCommandHandler : ICommandHandler<RemoveRootCategoryCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RemoveRootCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(RemoveRootCategoryCommand request, CancellationToken cancellationToken)
        {
            try 
            {
                var rootCategory = await _unitOfWork.RootCategoryRepository.GetById(RootCategoryId.Create(request.id));

                if (rootCategory == null) return Result.NotFound("this Category is not exist");

                await _unitOfWork.RootCategoryRepository.Delete(rootCategory);

                await _unitOfWork.save();

                return Result.Success(true);    
            }catch (Exception ex) 
            {
                return Result.CriticalError(ex.Message);
            }
        }
    }
}
