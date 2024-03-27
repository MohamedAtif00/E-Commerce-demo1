

using E_Commerce.Application.Common;
using E_Commerce.Domain.Model.Token;

namespace E_Commerce.Application.RefreshToken.DeleteRefreshToken
{
    public record DeleteRefreshTokenCommand(RefreshTokenId RefreshTokenId):ICommand<bool>;    
    
}
