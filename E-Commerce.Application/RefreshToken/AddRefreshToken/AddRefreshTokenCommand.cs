using E_Commerce.Application.Common;
using E_Commerce.Domain.Model.Token;
using E_Commerce.Domain.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.RefreshToken.AddRefreshToken
{
    public record AddRefreshTokenCommand(UserId UserId, DateTime IssuedUtc, DateTime ExpiresUtc) :ICommand<Domain.Model.Token.RefreshToken>;
    
}
