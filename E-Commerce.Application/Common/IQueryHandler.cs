using Ardalis.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Common
{
    public interface IQueryHandler<TQuery,TResult> : IRequestHandler<TQuery,Result<TResult>>
        where TQuery : IQuery<TResult>
        where TResult : notnull
    {
    }
}
