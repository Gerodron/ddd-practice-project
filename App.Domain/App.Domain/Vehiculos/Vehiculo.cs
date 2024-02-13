using App.Domain.Abstractions;
using App.Domain.Shared;
using App.Domain.Vehiculos.ValueObjects;
using App.Domain.Vehiculos.ValuesObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Vehiculos
{
    public sealed class Vehiculo : DomainEntity
    {
        public Vehiculo(
            Guid id,
            Modelo modelo,
            Vin vin,
            Direccion direccion,
            Moneda precioVenta,
            Moneda cuotaMantenimiento ,
            DateTime? fechaUltimaRenta ,
            List<Accesorio> accesorios 
        ) : base(id)
        {
            Modelo = modelo;
            Vin = vin;
            Direccion = direccion;
            PrecioAlquiler = precioVenta;
            CuotaMantenimiento = cuotaMantenimiento;
            FechaUltimaRenta = fechaUltimaRenta;
            Accesorios = accesorios;
        }



        public Modelo? Modelo { get; private set; }

        public Vin? Vin { get; private set; }

        public Direccion? Direccion { get; private set; }

        public Moneda? PrecioAlquiler { get; private set; }

        public Moneda? CuotaMantenimiento { get; private set; }

        public DateTime? FechaUltimaRenta { get; internal set; }

        public List<Accesorio> Accesorios { get; private set; } = new ();
    }
}
