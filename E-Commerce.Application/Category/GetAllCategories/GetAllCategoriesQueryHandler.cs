using Ardalis.Result;
using E_Commerce.Application.Common;
using E_Commerce.Domain.Common.Persistent.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Category.GetAllCategories
{
    public class GetAllCategoriesQueryHandler : IQueryHandler<GetAllCategoriesQuery, List<Domain.Model.Category.Category>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllCategoriesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<Domain.Model.Category.Category>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var categories = await _unitOfWork.CategoryRepository.GetAll();

                if (categories == null) return Result.CriticalError("Something Went Wronge");
                return Result.Success(categories);
            }catch (Exception ex) 
            {
                return Result.Error(ex.Message);
            }
        }
    }
}
