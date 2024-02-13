using App.Domain.Abstractions;

namespace App.Domain.Users.Events
{
    public record UserCreatedDomainEvent (Guid Id) : IDomainEvent;
}
