using E_Commerce.Application.Authentication;
using E_Commerce.Application.Common;
using E_Commerce.Domain.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.User.AddNewUser
{
    public record AddNewUserCommand(string FirstName,string LastName,string Username,string Email,string Password,string PhoneNumber,string Role) : ICommand<JwtTokenDto>;
    
}
