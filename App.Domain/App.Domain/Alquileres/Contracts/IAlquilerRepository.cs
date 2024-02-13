using App.Domain.Alquileres.ValueObjects;
using App.Domain.Vehiculos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Alquileres.Contracts
{
    public interface IAlquilerRepository
    {
        Task<Alquiler?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> IsOverlapingAsync(Vehiculo vehiculo, DateRange dateRange, CancellationToken cancellationToken = default);
        void Add(Alquiler alquiler, CancellationToken cancellationToken = default);

    }
}
