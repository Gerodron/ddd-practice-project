using App.Application.Abstraccions.Messaging;

namespace App.Application.Alquileres.ReservarAlquiler
{
    public record ReservarAlquilerCommand(
        Guid VehiculoId,
        Guid UserId,
        DateOnly FechaInicioReserva,
        DateOnly FechaFinReserva
        
    ) : ICommand<Guid>;
    
}
