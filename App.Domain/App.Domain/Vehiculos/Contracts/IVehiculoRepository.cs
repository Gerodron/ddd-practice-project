namespace App.Domain.Vehiculos.Contracts
{
    public interface IVehiculoRepository
    {
        Task<Vehiculo> GetByIdAsync(Guid id, CancellationToken cancellation = default);
    }
}
