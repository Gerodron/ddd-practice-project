using App.Application.Abstractions.Clock;
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
        private readonly IVehiculoRepository _vehiculoRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAlquilerRepository _alquilerRepository;
        private readonly PrecioDomainService _precioDomainService;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IUnitOfWork _unitOfWork;

        public ReservarAlquilerCommandHandler(
            IVehiculoRepository vehiculoRepository,
            IUserRepository userRepository,
            IAlquilerRepository alquilerRepository,
            PrecioDomainService precioDomainService,
            IDateTimeProvider dateTimeProvider,
            IUnitOfWork unitOfWork
        )
        {
            _vehiculoRepository = vehiculoRepository;
            _userRepository = userRepository;
            _alquilerRepository = alquilerRepository;
            _precioDomainService = precioDomainService;
            _dateTimeProvider = dateTimeProvider;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(ReservarAlquilerCommand request, CancellationToken cancellationToken)
        {
            // Obtener un usuario
            var usuario = await _userRepository.GetByIdAsync(request.UsuarioId, cancellationToken);
            if (usuario is null)
                return Result.Failure<Guid>(UserErros.NotFound);

            // Obtener vehículo
            var vehiculo = await _vehiculoRepository.GetByIdAsync(request.VehiculoId, cancellationToken);
            if (vehiculo is null)
                return Result.Failure<Guid>(VehiculoError.NotFound);

            // Duración del alquiler
            var periodoAlquiler = DateRange.Create(request.FechaInicio, request.FechaFin);
            if (await _alquilerRepository.IsOverlapingAsync(vehiculo, periodoAlquiler))
                return Result.Failure<Guid>(AlquilerErrors.OverLap);

            var alquiler = Alquiler.Reservar(
                vehiculo,
                usuario.Id,
                periodoAlquiler,
                _dateTimeProvider.CurrentTime,
                _precioDomainService
            );

            _alquilerRepository.Add(alquiler);
            await _unitOfWork.SaveChangesAsync();

            return alquiler.Id;
        }

    }
}
