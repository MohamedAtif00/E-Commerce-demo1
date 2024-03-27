using Ardalis.Result;
using E_Commerce.Application.Authentication;
using E_Commerce.Application.Common;
using E_Commerce.Application.RefreshToken.AddRefreshToken;
using E_Commerce.Application.RefreshToken.DeleteRefreshToken;
using E_Commerce.Application.RefreshToken.GetRefreshTokenByUserId;
using E_Commerce.Application.User.AddNewUser;
using E_Commerce.Application.User.GetUserById;
using E_Commerce.Domain.Model.Token;
using E_Commerce.Domain.Model.User;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace E_Commerce.Infrastructure.Authentication
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

        public async Task<Result<IdentityUser<Guid>>> Register(string FirstName, string LastName, string Username, string Email, string Password, string PhoneNumber, string Role)
        {
            try
            {

                if (FirstName  == null || LastName == null || Username == null || Email == null || Password == null)
                {
                    return Result.Error("Invalid input");
                }
                if (string.IsNullOrEmpty(Username))
                {
                    return Result.Error("username is required");
                }
                var existinguser = await _userManager.FindByNameAsync(Username);
                if (existinguser != null)
                {
                    return Result.Error("username is already exist");
                }
                var newuser = new IdentityUser<Guid> { UserName = Username, PhoneNumber = PhoneNumber, Email = Email };
                var result = await _userManager.CreateAsync(newuser, Password);
                if (result == IdentityResult.Success)
                {
                    try
                    {
                        //var useragg = await _mediator.Send(new AddNewUserCommand(UserId.Create(Guid.Parse(newuser.Id.ToString())), registerDto.FirstName, registerDto.LastName, registerDto.Username, registerDto.Email));
                        //if (useragg.Value == null) return Result.Error("error");
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
                    return Result.Error("user registration failed");
                }
                //Generate Claims Identity
                //var claimIdentity = GenerateClaimsIdentity(newuser);
                //Generate JWT Token
                //var jwtToken = GenerateAccessToken(claimIdentity);
                //Generate Refresh Toekn
                //var refreshToken = await GenerateRefreshToken(newuser);
                //Set these into jwwtTokenVm
                //jwttokenDto.JwtToken = jwtToken;
                //jwttokenDto.RefreshToken = refreshToken;
                //jwttokenDto.Error = string.Empty;

                return Result.Success(newuser);

            }
            catch (Exception ex)
            {
                return Result.Error(ex.Message);
            }

        }

        public async Task<Result<JwtTokenDto>> Login(LoginDto loginDto)
        {
            try
            {
                IdentityUser<Guid> user;
                var jwttokenDto = new JwtTokenDto();
                if (loginDto == null)
                {
                    jwttokenDto.Error = "Invalid input";
                    return jwttokenDto;
                }
                if (string.IsNullOrEmpty(loginDto.Username))
                {
                    jwttokenDto.Error = "username is required";
                    return jwttokenDto;
                }
                if (loginDto.Email == null)
                {
                    user = await _userManager.FindByNameAsync(loginDto.Username);

                    if (user == null)
                    {
                        jwttokenDto.Error = "username is not found";
                        return Result.NotFound(jwttokenDto.Error);
                    }
                }
                else
                {
                    user = await _userManager.FindByEmailAsync(loginDto.Email);

                    if (user == null)
                    {
                        jwttokenDto.Error = "user is not found";
                        return Result.NotFound(jwttokenDto.Error);
                    }
                }
                //var user = await _userManager.FindByNameAsync(loginDto.Username);
                if (user == null) return Result.NotFound("this user is not exist");
                var locked = await _userManager.IsLockedOutAsync(user);
                if (locked) return Result.Error("Account lockedout");
                var passwordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);
                if (!passwordValid)
                {
                    await _userManager.AccessFailedAsync(user);
                    return Result.Error("username or password is not valid");
                }

                //Generate Claims Identity
                var claimIdentity = await GenerateClaimsIdentity(user);
                //Generate JWT Token
                var jwtToken = await GenerateAccessToken(claimIdentity);
                //Generate Refresh Toekn
                var refreshToken = await GenerateRefreshToken(user);
                //Set these into jwwtTokenVm
                jwttokenDto.JwtToken = jwtToken;
                jwttokenDto.RefreshToken = refreshToken;
                jwttokenDto.Error = string.Empty;

                return Result.Success(jwttokenDto);

            }
            catch (Exception ex)
            {
                return Result.Error(ex.Message);
            }

        }

        public async Task<Result<string>> CheckUsername(string username)
        {
            var result = await _userManager.FindByNameAsync(username);

            if (result == null) return Result.NotFound("this username is not exist");
            return Result.Success(result.UserName);
        }

        public async Task<ClaimsIdentity> GenerateClaimsIdentity(IdentityUser<Guid> user)
        {

            var secondsFromUnixEpoch = (DateTime.UtcNow - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds;
            var unixEpochDatestr = ((long)Math.Round(secondsFromUnixEpoch)).ToString();

            var claims = new List<Claim>
            {
                new Claim("userid",user.Id.ToString()),
                new Claim("usernm",user.UserName),
                new Claim("email",user.Email),
                new Claim("password",user.PasswordHash),
                new Claim(ClaimTypes.Role,"user"),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,unixEpochDatestr)
            };

            return new ClaimsIdentity(new GenericIdentity(user.UserName, "token"), claims);
        }

        public async Task<string> GenerateAccessToken(ClaimsIdentity claimsIdentity)
        {
            var jwtToken = new JwtSecurityToken(
                    _jwtSettings.Issuer,
                    _jwtSettings.Audiance,
                    claimsIdentity.Claims,
                    DateTime.UtcNow.Add(TimeSpan.FromMinutes(_jwtSettings.AccessTokeExpiryMinutes)),
                    DateTime.UtcNow.Add(TimeSpan.FromMinutes(_jwtSettings.RefreshTokenExpiryMinutes)),
                    new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SigningKey)), SecurityAlgorithms.HmacSha256)
                );
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }

        public async Task<string> GenerateRefreshToken(IdentityUser<Guid> newUser)
        {
            var result = await _mediator.Send(new GetUserByIdQuery(UserId.Create(newUser.Id)));
            var id = result.Value.Id;
            var existRefreshToken = await _mediator.Send(new GetRefreshTokenByUserIdQuery(id));

            if (existRefreshToken.Value != null) await _mediator.Send(new DeleteRefreshTokenCommand(existRefreshToken.Value.Id));
            var refreshToken = RefreshToken.Create(result.Value.Id, DateTime.UtcNow, DateTime.UtcNow.AddMinutes(Convert.ToInt32(_jwtSettings.RefreshTokenExpiryMinutes)));
            var newRefreshToken = await _mediator.Send(new AddRefreshTokenCommand(refreshToken.UserId, refreshToken.IssuedUtc, refreshToken.ExpiresUtc));
            return newRefreshToken.Value.Id.ToString() ?? string.Empty;
        }

        public async Task<Result> ConfirmEmail(string userId, string code)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var result = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            if (user == null) { return Result.NotFound("this user is not exist"); }
            var confirm = await _userManager.ConfirmEmailAsync(user, result);
            if (confirm == IdentityResult.Success)
            {
                return Result.Success();
            }
            return Result.Error();
        }

    }

}
