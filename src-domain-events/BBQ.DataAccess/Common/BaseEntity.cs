using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using MediatR;

namespace BBQ.DataAccess.Common
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        
        private readonly List<INotification> _domainEvents = new();

        [NotMapped, JsonIgnore]
        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents.AsReadOnly();

        public void AddDomainEvent(INotification domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}
