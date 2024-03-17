using E_Commerce.Domain.Common.Persistent.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Model.User
{
    public interface IUserRepository :IGenericRepository<User,UserId>
    {
    }
}
