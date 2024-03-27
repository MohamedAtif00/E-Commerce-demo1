using Ardalis.Result;
using E_Commerce.Application.Authentication;
using E_Commerce.Application.User.AddNewUser;
using E_Commerce.Domain.Model.User;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Interceptor
{
    public class UserCustomeStore : UserStore<IdentityUser>
    {
        private readonly DbContextClass _context;
        private readonly IMediator _mediator;
        public UserCustomeStore(DbContext context, IdentityErrorDescriber? describer = null, IMediator mediator = null) : base(context, describer)
        {
            _context = context as DbContextClass;
            _mediator = mediator;
        }

        
    }
}
