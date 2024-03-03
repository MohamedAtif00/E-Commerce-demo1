using Ardalis.Result;
using E_Commerce.Application.Common;
using E_Commerce.Domain.Common.Persistent.UnitOfWork;
using E_Commerce.Domain.Model.RootCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.RootCategory.ChangeRootCategoryName
{
    internal class ChangeRootCategoryNameCommandHandler : ICommandHandler<ChangeRootCategoryNameCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ChangeRootCategoryNameCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(ChangeRootCategoryNameCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var rootCategoryId = RootCategoryId.Create(request.id);
                var rootCategory = await _unitOfWork.RootCategoryRepository.GetById(rootCategoryId);

                if (rootCategory == null)
                {
                    return Result.Error("Root category not found.");
                }

                rootCategory.Update(request.name);

                await _unitOfWork.RootCategoryRepository.Update(rootCategory);

                var res = await _unitOfWork.save();

                return Result.Success(true);
            }
            catch (Exception ex)
            {
                return Result.CriticalError(ex.Message);
            }

        }
    }
}
