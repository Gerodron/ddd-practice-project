using App.Application.Abstraccions.Clock;
using App.Application.Abstractions.Messaging;
using App.Domain.Abstractions;
using App.Domain.Alquileres;
using App.Domain.Alquileres.Contracts;
using App.Domain.Alquileres.Services;
using App.Domain.Alquileres.ValueObjects;
using App.Domain.Shared;
using App.Domain.Users;
using App.Domain.Vehiculos;
using App.Domain.Vehiculos.Contracts;

namespace App.Application.Alquileres.ReservarAlquiler
{
    internal sealed class ReservarAlquilerCommandHandler : ICommandHandler<ReservarAlquilerCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IVehiculoRepository _vehiculoRepository;
        private readonly IAlquilerRepository _alquilerRepository;
        private readonly PrecioDomainService _precioService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;

        public ReservarAlquilerCommandHandler(
            IUserRepository userRepository,
            IVehiculoRepository vehiculoRepository,
            IAlquilerRepository alquilerRepository,
            PrecioDomainService precioService,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider 
        )
        {
            _userRepository = userRepository;
            _vehiculoRepository = vehiculoRepository;
            _alquilerRepository = alquilerRepository;
            _precioService = precioService;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<Result<Guid>> Handle(
            ReservarAlquilerCommand request,
            CancellationToken cancellationToken
        )
        {
            var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
            if (user is null)
            {
                return Result.Failure<Guid>(UserErros.NotFound);
            }

            var vehiculo = await _vehiculoRepository.GetByIdAsync(request.VehiculoId, cancellationToken);
            if (vehiculo is null)
            {
                return Result.Failure<Guid>(VehiculoError.NotFound);
            }

            var periodoAlquiler = DateRange.Create(request.FechaInicioReserva, request.FechaFinReserva);

            if (await _alquilerRepository.IsOverlapingAsync(vehiculo, periodoAlquiler, cancellationToken))
            {
                return Result.Failure<Guid>(AlquilerErrors.OverLap);
            }

            var alquiler = Alquiler.Reservar(vehiculo, user.Id, periodoAlquiler, _dateTimeProvider.CurrentTime, _precioService);

            _alquilerRepository.Add(alquiler);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return alquiler.Id;
        }
    }
}
