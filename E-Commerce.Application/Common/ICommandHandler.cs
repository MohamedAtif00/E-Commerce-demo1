using Ardalis.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Common
{
    public interface ICommandHandler<TCommand,TResult> : IRequestHandler<TCommand,Result<TResult>>
        where TCommand:ICommand<TResult>
        where TResult : notnull
    {
    }

    public interface ICommandHandler<TCommand> : IRequestHandler<TCommand>
        where TCommand : ICommand
    {
    }
}
