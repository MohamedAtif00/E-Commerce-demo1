using Ardalis.Result;
using E_Commerce.Application.Common;
using E_Commerce.Domain.Common.Persistent.UnitOfWork;
using user = E_Commerce.Domain.Model.User;

namespace E_Commerce.Application.User.GetUserById
{
    public class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, user.User>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUserByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<user.User>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _unitOfWork.UserRepository.GetById(request.UserId);

                if (result == null) return Result.NotFound("this user is not exist");

                return Result.Success(result);
            }catch (Exception ex) 
            {
                return Result.Error(ex.Message);
            }
        }
    }
}
