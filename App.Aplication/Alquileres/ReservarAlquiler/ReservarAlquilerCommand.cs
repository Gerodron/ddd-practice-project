using App.Application.Abstractions.Messaging;

namespace App.Application.Alquileres.ReservarAlquiler
{
    public record ReservarAlquilerCommand(
        Guid VehiculoId,
        Guid UsuarioId,
        DateOnly FechaInicio,
        DateOnly FechaFin
        ) : ICommand<Guid>;
    
}
