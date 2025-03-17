using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BBQ.DataAccess.Common
{
    public static class DomainEventsExtensions
    {
        public static ICollection<INotification> GetDomainEvents(this DbContext context)
        {
            if (context == null) { throw new ArgumentNullException(nameof(context)); }

            var entities = context.ChangeTracker
                .Entries<BaseEntity>()
                .Where(e => e.Entity.DomainEvents.Any())
                .Select(e => e.Entity);

            var domainEvents = entities
                .SelectMany(e => e.DomainEvents)
                .ToList();

            entities.ToList().ForEach(e => e.ClearDomainEvents());
            return domainEvents;
        }
        
        public static async Task DispatchDomainEvents(this IPublisher mediator, ICollection<INotification> domainEvents)
        {
            if (mediator == null) { throw new ArgumentNullException(nameof(mediator)); }

            foreach (var domainEvent in domainEvents)
            {
                await mediator.Publish(domainEvent);
            }
        }
    }
}
