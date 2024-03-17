using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace E_Commerce.Application.Behavior
{
    public class UnitOfWorkBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                if (!(typeof(TRequest).Equals(typeof(ICommand))))
                {
                    return await next();
                }
                // Process the request using the next delegate
                var response = await next();


                // Commit the transaction if successful
                //await _context.SaveChangesAsync(cancellationToken);
                //transaction.Commit();

                return response;
            }
            catch (Exception ex)
            {
                // Rollback the transaction on any exception
                //transaction.Rollback();

                throw ex;
            }
        }
    }
}
