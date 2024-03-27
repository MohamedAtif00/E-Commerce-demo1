using Ardalis.Result;
using E_Commerce.Application.Common;
using E_Commerce.Domain.Common.Persistent.UnitOfWork;
using refreshtoken = E_Commerce.Domain.Model.Token.RefreshToken;



namespace E_Commerce.Application.RefreshToken.GetRefreshTokenByUserId
{
    public class GetRefreshTokenByUserIdQueryHandler : IQueryHandler<GetRefreshTokenByUserIdQuery, refreshtoken>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetRefreshTokenByUserIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<refreshtoken>> Handle(GetRefreshTokenByUserIdQuery request, CancellationToken cancellationToken)
        {
            try {
                var refreshToken = await _unitOfWork.RefreshTokenRepository.GetRefreshTokenByUserId(request.UserId);
                if (refreshToken == null)
                {
                    return Result.NotFound("this refresh token is not exist");
                }

                return Result.Success(refreshToken);
            }catch (Exception ex) 
            {
                return Result.Error(ex.Message);
            }
        }
    }
}
