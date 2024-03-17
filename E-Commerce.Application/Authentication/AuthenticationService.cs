using Ardalis.Result;
using E_Commerce.Application.Common;
using E_Commerce.Domain.Model.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;
using Microsoft.IdentityModel.Tokens;
using MediatR;
using E_Commerce.Application.User.AddNewUser;

namespace E_Commerce.Application.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<IdentityUser<Guid>> _userManager;
        private readonly JwtSettings _jwtSettings;
        private readonly IMediator _mediator;
        private readonly IEmailSender _emailSender;
        //private readonly PasswordHasher<IdentityUser> _passwordHasher;

        public AuthenticationService(UserManager<IdentityUser<Guid>> userManager, IOptions<JwtSettings> options, IMediator mediator, IEmailSender emailSender)
        {
            _userManager = userManager;
            _jwtSettings = options.Value;
            _mediator = mediator;
            _emailSender = emailSender;
        }

        public async Task<Result<JwtTokenDto>> Register(RegisterDto registerDto)
        {
            try 
            {

                var jwttokenDto = new JwtTokenDto();
                if (registerDto == null)
                {
                    jwttokenDto.Error = "Invalid input";
                    return jwttokenDto;
                }
                if (string.IsNullOrEmpty(registerDto.Username))
                {
                    jwttokenDto.Error = "username is required";
                    return jwttokenDto;
                }
                var existinguser = await _userManager.FindByNameAsync(registerDto.Username);
                if (existinguser != null)
                {
                    jwttokenDto.Error = "username is already exist";
                    return jwttokenDto;
                }
                var newuser = new IdentityUser<Guid> { UserName= registerDto.Username,PhoneNumber = registerDto.PhoneNumber,Email = registerDto.Email};
                var result = await _userManager.CreateAsync(newuser, registerDto.Password);
                if (result == IdentityResult.Success)
                {
                    try
                    {
                        var useragg = await _mediator.Send(new AddNewUserCommand(UserId.Create(Guid.Parse(newuser.Id.ToString())), registerDto.FirstName, registerDto.LastName, registerDto.Username, registerDto.Email));
                        if (useragg.Value == null) return Result.Error("error");
                        var conformationToken = await _userManager.GenerateEmailConfirmationTokenAsync(newuser);
                        await _emailSender.SendEmailAsync("bicijoj973@dovesilo.com", "Helloo", $"<h1> HelloWorld</h1>  http://localhost:4200/ {newuser.Id} Confirmation Token is {conformationToken}");

                    }
                    catch (Exception ex)
                    {
                        return Result.Error(ex.Message);
                    }
                }
                else 
                { 
                    return Result.Error(/*string.Join(",", result.Errors.Select(x =>x.Description))*/); 
                }

                //if (result.Errors.Any())
                //{
                //    jwttokenDto.Error = string.Join(",", result.Errors);
                //    return jwttokenDto;
                //}
                if (!result.Succeeded)
                {
                    jwttokenDto.Error = "user registration failed";
                    return jwttokenDto;
                }
                //Generate Claims Identity
                var claimIdentity = GenerateClaimsIdentity(newuser);
                //Generate JWT Token
                var jwtToken = GenerateAccessToken(claimIdentity);
                //Generate Refresh Toekn

                //Set these into jwwtTokenVm
                jwttokenDto.JwtToken = jwtToken;
                jwttokenDto.RefreshToken = string.Empty;
                jwttokenDto.Error = string.Empty;

                return Result.Success(jwttokenDto);

            }catch (Exception ex) 
            {
                return Result.Error(ex.Message);
            }

        }

        private static ClaimsIdentity GenerateClaimsIdentity(IdentityUser<Guid> user) 
        {

            var secondsFromUnixEpoch = (DateTime.UtcNow - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds;
            var unixEpochDatestr = ((long)Math.Round(secondsFromUnixEpoch)).ToString();

            var claims = new List<Claim>
            {
                new Claim("userid",user.Id.ToString()),
                new Claim("usernm",user.UserName),
                new Claim("email",user.Email),
                new Claim("password",user.PasswordHash),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,unixEpochDatestr)
            };

            return new ClaimsIdentity(new GenericIdentity(user.UserName,"token"),claims);
        }

        private string GenerateAccessToken(ClaimsIdentity claimsIdentity)
        {
            var jwtToken = new JwtSecurityToken(
                    _jwtSettings.Issuer,
                    _jwtSettings.Audiance,
                    claimsIdentity.Claims,
                    DateTime.UtcNow.Add(TimeSpan.FromMinutes(_jwtSettings.AccessTokeExpiryMinutes)),
                    DateTime.UtcNow.Add(TimeSpan.FromMinutes(_jwtSettings.RefreshTokenExpiryMinutes)),
                    new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SigningKey)),SecurityAlgorithms.HmacSha256)
                );
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }

        public async Task<Result> ConfirmEmail(string userId , string code)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var result = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            if (user == null) { return Result.NotFound("this user is not exist"); }
            var confirm = await _userManager.ConfirmEmailAsync(user,result);
            if (confirm == IdentityResult.Success)
            {
                return Result.Success();
            }
            return Result.Error();
        }

    }
}
