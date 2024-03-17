using E_Commerce.Domain.Common.Persistent.UnitOfWork;
using E_Commerce.Domain.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Domain.UserRepo
{
    public class UserRepository : GenericRepository<User, UserId>,IUserRepository
    {
        private readonly DbContextClass _context;
        public UserRepository(DbContextClass context) : base(context)
        {
            _context = context;
        }
    }
}
