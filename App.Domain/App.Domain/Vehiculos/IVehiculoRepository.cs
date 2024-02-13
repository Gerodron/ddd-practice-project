namespace App.Domain.Vehiculos
{
    public interface IVehiculoRepository
    {
        Task<Vehiculo> GetByIdAsync(Guid id, CancellationToken cancellation = default);
    }
}
