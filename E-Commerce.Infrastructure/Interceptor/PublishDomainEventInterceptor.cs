using E_Commerce.Domain.Common.Persistent;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Interceptor
{
    public class PublishDomainEventInterceptor : SaveChangesInterceptor
    {
        private readonly IPublisher publisher;

        public PublishDomainEventInterceptor(IPublisher publisher)
        {
            this.publisher = publisher;
        }

        public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
        {
            try
            {
                PublishDomainEvents(eventData.Context);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return base.SavedChanges(eventData, result);
        }

        public override async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
        {
            using var transaction = await eventData.Context.Database.BeginTransactionAsync(cancellationToken);
            try
            {

                var domainResult = await PublishDomainEvents(eventData.Context);
                await transaction.CommitAsync(cancellationToken);
                if (domainResult) await eventData.Context.SaveChangesAsync();
                await base.SavedChangesAsync(eventData, result, cancellationToken);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw; // Re-throw the exception after rolling back the transaction
            }


            return await base.SavedChangesAsync(eventData, result, cancellationToken);
        }

        public async Task<bool> PublishDomainEvents(DbContext context)
        {
            int count = 0;
            do
            {
                // Hold entities
                var entities = context.ChangeTracker.Entries<IHasDomainEvents>()
                    .Where(x => x.Entity.DomainEvents.Any())
                    .Select(x => x.Entity)
                    .ToList();

                // Hold domain events
                var domainEvents = entities.SelectMany(x => x.DomainEvents).ToList();

                // If there are no domain events, exit the loop
                if (!domainEvents.Any())
                {
                    break;
                }
                count++;
                // Clear domain events
                entities.ForEach(x => x.ClearDomainEvents());

                // Publish domain events
                foreach (var domainEvent in domainEvents)
                {
                    await publisher.Publish(domainEvent);
                }

                // Set the flag to indicate that events have been published

            } while (true);

            return count > 0;
        }
    }
}
