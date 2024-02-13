using App.Domain.Alquileres.ValueObjects;
using App.Domain.Shared;
using App.Domain.Vehiculos;

namespace App.Domain.Alquileres.Services
{
    public class PrecioDomainService
    {
        public PrecioDetalle CalcularPrecio(Vehiculo vehiculo, DateRange periodoAlquiler)
        {
            var tipoMoneda = vehiculo.PrecioAlquiler!.TipoMoneda;
            var duracionAlquilerEnDias = periodoAlquiler.CalcularDuracionEnDias();
            var precioBaseAlquiler = vehiculo.PrecioAlquiler.Monto;
            var precioTotalPeriodoAlquiler = new Moneda(duracionAlquilerEnDias * precioBaseAlquiler, tipoMoneda);


            decimal porcentageChange = 0;
            foreach (var accesorio in vehiculo.Accesorios)
            {
                porcentageChange += accesorio switch
                {
                    Accesorio.AppleCar or Accesorio.AndroidCar => 0.05m,
                    Accesorio.AireAcondicionado => 0.01m,
                    Accesorio.Mapas => 0.01m,
                    _ => 0
                };
            }

            var accesorioCharges = Moneda.Zero(tipoMoneda);

            if (porcentageChange > 0)
            {
                accesorioCharges = new Moneda(precioTotalPeriodoAlquiler.Monto * porcentageChange, tipoMoneda);
            }


            var precioTotal = Moneda.Zero(tipoMoneda);
            precioTotal += precioTotalPeriodoAlquiler;
            if (!vehiculo!.CuotaMantenimiento!.IsZero())
            {
                precioTotal += vehiculo.CuotaMantenimiento;
            }

            precioTotal += accesorioCharges;

            return new PrecioDetalle(precioTotalPeriodoAlquiler, vehiculo.CuotaMantenimiento, accesorioCharges, precioTotal);
        }

    }
}


//namespace App.Domain.Alquileres
//{
//    public class PrecioDomainService
//    {
//        public PrecioDetalle CalcularPrecio(Vehiculo vehiculo, DateRange periodoAlquiler)
//        {
//            if (vehiculo == null)
//                throw new ArgumentNullException(nameof(vehiculo));
//            if (periodoAlquiler == null)
//                throw new ArgumentNullException(nameof(periodoAlquiler));

//            var precioBaseAlquiler = vehiculo!.PrecioAlquiler!.Monto;
//            var tipoMoneda = vehiculo.PrecioAlquiler.TipoMoneda;
//            var duracionAlquilerEnDias = periodoAlquiler.CalcularDuracionEnDias();
//            var precioTotalPeriodoAlquiler = new Moneda(duracionAlquilerEnDias * precioBaseAlquiler, tipoMoneda);

//            decimal porcentajeChange = CalcularPorcentajeCambios(vehiculo);

//            var accesorioCharges = CalcularCargosPorAccesorios(vehiculo, precioTotalPeriodoAlquiler.Monto * porcentajeChange, tipoMoneda);

//            var precioTotal = CalcularPrecioTotal(precioTotalPeriodoAlquiler, vehiculo!.CuotaMantenimiento!, accesorioCharges);

//            return new PrecioDetalle(precioTotalPeriodoAlquiler, vehiculo!.CuotaMantenimiento!, accesorioCharges, precioTotal);
//        }

//        private decimal CalcularPorcentajeCambios(Vehiculo vehiculo)
//        {
//            decimal porcentajeChange = 0;
//            foreach (var accesorio in vehiculo.Accesorios)
//            {
//                porcentajeChange += accesorio switch
//                {
//                    Accesorio.AppleCar or Accesorio.AndroidCar => 0.05m,
//                    Accesorio.AireAcondicionado => 0.01m,
//                    Accesorio.Mapas => 0.01m,
//                    _ => 0
//                };
//            }
//            return porcentajeChange;
//        }

//        private Moneda CalcularCargosPorAccesorios(Vehiculo vehiculo, decimal precioTotal, TipoMoneda tipoMoneda)
//        {
//            decimal porcentajeChange = CalcularPorcentajeCambios(vehiculo);
//            if (porcentajeChange > 0)
//            {
//                return new Moneda(precioTotal, tipoMoneda);
//            }
//            return Moneda.Zero(tipoMoneda);
//        }

//        private Moneda CalcularPrecioTotal(Moneda precioTotalPeriodoAlquiler, Moneda cuotaMantenimiento, Moneda accesorioCharges)
//        {
//            var precioTotal = precioTotalPeriodoAlquiler + cuotaMantenimiento + accesorioCharges;
//            return precioTotal;
//        }
//    }
//}
