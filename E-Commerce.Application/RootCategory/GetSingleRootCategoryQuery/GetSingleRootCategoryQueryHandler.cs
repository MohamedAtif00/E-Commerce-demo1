using Ardalis.Result;
using E_Commerce.Application.Common;
using E_Commerce.Domain.Common.Persistent.UnitOfWork;
using E_Commerce.Domain.Model.RootCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.RootCategory.GetSingleRootCategoryQuery
{
    public class GetSingleRootCategoryQueryHandler : IQueryHandler<GetSingleRootCategoryQuery, Domain.Model.RootCategory.RootCategory>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetSingleRootCategoryQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Domain.Model.RootCategory.RootCategory>> Handle(GetSingleRootCategoryQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.RootCategoryRepository.GetById(RootCategoryId.Create(request.id));

            return Result.Success(result);
        }
    }
}
