using Ardalis.Result;
using E_Commerce.Application.Common;
using E_Commerce.Domain.Common.Persistent.UnitOfWork;
using E_Commerce.Domain.Model.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Category.GetSingleCategory
{
    public class GetSingleCategoryQueryHandler : IQueryHandler<GetSingleCategoryQuery, Domain.Model.Category.Category>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetSingleCategoryQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Domain.Model.Category.Category>> Handle(GetSingleCategoryQuery request, CancellationToken cancellationToken)
        {
            try
            {
                
                var category = await _unitOfWork.CategoryRepository.GetById(CategoryId.Create(request.id));

                if (category == null) return Result.NotFound("this category is not exist");

                return Result.Success(category);    
            }catch  (Exception ex) 
            {
                return Result.Error(ex.Message);
            }
        }
    }
}
