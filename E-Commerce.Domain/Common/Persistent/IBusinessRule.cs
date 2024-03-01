using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Common.Persistent
{
    public interface IBusinessRule
    {
        bool IsBroken();

        string Message { get; }
    }
}
