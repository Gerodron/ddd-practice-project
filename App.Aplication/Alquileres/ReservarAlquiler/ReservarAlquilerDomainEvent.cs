using App.Domain.Alquileres.Contracts;
using App.Domain.Alquileres.Events;
using App.Domain.Users;
using App.Domain.Users.ValueObjects;
using MediatR;

namespace App.Application.Alquileres.ReservarAlquiler
{
    public class ReservarAlquilerDomainEvent : INotificationHandler<NuevoAlquilerGeneradoDomainEvent>
    {
        /*
            SE MANDAR UN CORREO ELECTRONICO AL USUARIO PARA CONFIRMAR SU 
            ALQUILER 

         */

        private readonly IUserRepository _userRepository;
        private readonly IAlquilerRepository _alquilerRepository;
        private readonly IEmailService _emailService;

        public ReservarAlquilerDomainEvent(IUserRepository userRepository, IAlquilerRepository alquilerRepository, IEmailService emailService)
        {
            _userRepository = userRepository;
            _alquilerRepository = alquilerRepository;
            _emailService = emailService;
        }

        public async Task Handle(NuevoAlquilerGeneradoDomainEvent notification, CancellationToken cancellationToken)
        {
            var alquiler = await _alquilerRepository.GetByIdAsync(notification.id, cancellationToken);
            if (alquiler is null) return;

            var usuario = await _userRepository.GetByIdAsync(alquiler.UsuarioId, cancellationToken);
            if (usuario is null) return;

            await _emailService.SendMailAsync(
                usuario.Email,
                "Confirmación de Alquiler",
                "Tiene 2 horas para confirmar su alquiler, de lo contrario será cancelado."
            );

        }
    }
}
