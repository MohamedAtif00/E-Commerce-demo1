using Ardalis.Result;
using E_Commerce.Application.Authentication;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Common
{
    public interface IAuthenticationService
    {
        Task<Result<string>> CheckUsername(string username);
        Task<Result> ConfirmEmail(string userId, string code);
        Task<string> GenerateAccessToken(ClaimsIdentity claimsIdentity);
        Task<ClaimsIdentity> GenerateClaimsIdentity(IdentityUser<Guid> user);
        Task<string> GenerateRefreshToken(IdentityUser<Guid> newUser);
        Task<Result<JwtTokenDto>> Login(LoginDto loginDto);
        Task<Result<IdentityUser<Guid>>> Register(string FirstName, string LastName, string Username, string Email, string Password, string PhoneNumber, string Role);
    }
}
