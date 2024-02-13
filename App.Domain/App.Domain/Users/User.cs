using App.Domain.Abstractions;
using App.Domain.Users.Events;
using App.Domain.Users.ValueObjects;

namespace App.Domain.Users
{
    public sealed class User : DomainEntity
    {
        private User(
            Guid id,
            Nombre nombre,
            Apellido apellido,
            Email email
        ) : base(id)
        {
            Nombre   = nombre;
            Apellido = apellido;
            Email    = email;
        }

        public static User Create(
            Nombre nombre,
            Apellido apellido,
            Email email
        )
        {
            var user = new User(Guid.NewGuid(), nombre, apellido, email);
            user.PublishDomainEvent(new UserCreatedDomainEvent(user.Id));
            return user;
        }

        public Nombre? Nombre { get; private set; }

        public Apellido? Apellido { get; private set; }

        public Email? Email { get; private set; }
    }
}
