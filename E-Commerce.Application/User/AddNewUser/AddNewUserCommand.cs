using E_Commerce.Application.Common;
using E_Commerce.Domain.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.User.AddNewUser
{
    public record AddNewUserCommand(UserId UserId,string FirstName,string LastName,string Username,string Email) : ICommand<Domain.Model.User.User>;
    
}
