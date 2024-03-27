using E_Commerce.Application.Common;
using E_Commerce.Domain.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.User.GetUserById
{
    public record GetUserByIdQuery(UserId UserId):IQuery<Domain.Model.User.User>;
    
    
}
