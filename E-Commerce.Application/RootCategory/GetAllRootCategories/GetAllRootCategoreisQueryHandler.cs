using Ardalis.Result;
using E_Commerce.Application.Common;
using E_Commerce.Domain.Common.Persistent.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.RootCategory.GetAllRootCategories
{
    public class GetAllRootCategoreisQueryHandler : IQueryHandler<GetAllRootCategoriesQuery, List<Domain.Model.RootCategory.RootCategory>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllRootCategoreisQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<Domain.Model.RootCategory.RootCategory>>> Handle(GetAllRootCategoriesQuery request, CancellationToken cancellationToken)
        {
            var rootCategories = await _unitOfWork.RootCategoryRepository.GetAll();

            return Result.Success(rootCategories);
        }
    }
}
