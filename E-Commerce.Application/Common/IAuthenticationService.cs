using Ardalis.Result;
using E_Commerce.Application.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Common
{
    public interface IAuthenticationService
    {
        Task<Result> ConfirmEmail(string userId, string code);
        Task<Result<JwtTokenDto>> Register(RegisterDto registerDto);
    }
}
