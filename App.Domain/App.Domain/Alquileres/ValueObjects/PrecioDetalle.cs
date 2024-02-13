using App.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Alquileres.ValueObjects
{
    public record PrecioDetalle(
        Moneda precioAlquiler,
        Moneda cuotaMantenimiento,
        Moneda costoAccesorios,
        Moneda PrecioTotal
        
        );
}
