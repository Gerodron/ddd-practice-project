using App.Domain.Abstractions;
using App.Domain.Alquileres.Enums;
using App.Domain.Alquileres.Events;
using App.Domain.Alquileres.Services;
using App.Domain.Alquileres.ValueObjects;
using App.Domain.Shared;
using App.Domain.Users;
using App.Domain.Vehiculos;
using System;

namespace App.Domain.Alquileres
{
    public sealed class Alquiler : DomainEntity
    {
        private Alquiler(
            Guid id,
            Guid vehiculoId,
            Guid usuarioId,
            Moneda cuotaMantenimiento,
            Moneda costoAccesorios,
            Moneda precioTotal,
            Moneda precioAlquiler,
            AlquilerStatus alquilerEstado,
            DateRange periodoAlquiler,
            DateTime fechaReservacion
        ) : base(id)
        {
            VehiculoId = vehiculoId;
            UsuarioId = usuarioId;
            CuotaMantenimiento = cuotaMantenimiento;
            CostoAccesorios = costoAccesorios;
            PrecioTotal = precioTotal;
            PrecioAlquiler = precioAlquiler;
            AlquilerEstado = alquilerEstado;
            PeriodoAlquiler = periodoAlquiler;
            FechaReservacion = fechaReservacion;
        }

        public static Alquiler Reservar(
           Vehiculo vehiculo, 
           Guid usuarioId, 
           DateRange periodoAlquiler, 
           DateTime fechaReservacion,
           PrecioDomainService precioDomainService
        )
        {
            var precioDetalle = precioDomainService.CalcularPrecio(vehiculo, periodoAlquiler);
            vehiculo.FechaUltimaRenta = fechaReservacion;


            var alquiler = new Alquiler(
               Guid.NewGuid(),
               vehiculo.Id,
               usuarioId,
               precioDetalle.CuotaMantenimiento,
               precioDetalle.CostoAccesorios,
               precioDetalle.PrecioTotal,
               precioDetalle.PrecioAlquiler,
               AlquilerStatus.Reservado,
               periodoAlquiler,
               fechaReservacion);

            alquiler.PublishDomainEvent(new NuevoAlquilerGeneradoDomainEvent(alquiler.Id));

            return alquiler;
        }

        public Result? Confirmar(DateTime utcNow)
        {
            if(AlquilerEstado != AlquilerStatus.Reservado)
            {
                return Result.Failure(AlquilerErrors.NotReserved);
            }

            AlquilerEstado = AlquilerStatus.Confirmado;
            FechaConfirmacion = utcNow;
            PublishDomainEvent(new AlquilerConfirmadoDomainEvent(Id));
            return Result.Success();    
        }

        public Result? Rechazar(DateTime utcNow)
        {
            if(AlquilerEstado != AlquilerStatus.Reservado)
            {
                return Result.Failure(AlquilerErrors.NotReserved);
            }

            AlquilerEstado = AlquilerStatus.Descartado;
            FechaAnulacion = utcNow;
            PublishDomainEvent(new AlquilerRechazadoDomainEvent(Id));
            return Result.Success();
        }

        public Result? Cancelar(DateTime utcNow)
        {
            if(AlquilerEstado != AlquilerStatus.Confirmado)
            {
                return Result.Failure(AlquilerErrors.NotConfirmed);
            }

            var currentDay = DateOnly.FromDateTime(utcNow);

            if(currentDay > PeriodoAlquiler!.FechaInicioReserva)
            {
                return Result.Failure(AlquilerErrors.AlreadyStarted);
            }

            AlquilerEstado = AlquilerStatus.Abandonado;
            FechaCancelacion = utcNow;
            PublishDomainEvent(new AlquilerCanceladoDomainEvent(Id));
            return Result.Success();    
        }

        public Result? Completado(DateTime utcNow)
        {
            if(AlquilerEstado != AlquilerStatus.Confirmado)
            {
                return Result.Failure(AlquilerErrors.NotConfirmed);
            }
            AlquilerEstado = AlquilerStatus.Completado;
            FechaCompletado = utcNow;
            PublishDomainEvent(new AlquilerCompletadoDomainEvent(Id));
            return Result.Success();
        }

        public Guid VehiculoId { get; private set; }

        public Guid UsuarioId { get; private set; }

        public Moneda? CuotaMantenimiento { get; private set; }

        public Moneda? CostoAccesorios { get; private set; }

        public Moneda? PrecioTotal { get; private set; }

        public Moneda? PrecioAlquiler { get; private set; }

        public AlquilerStatus? AlquilerEstado { get; private set; }

        public DateRange? PeriodoAlquiler { get; private set; }

        public DateTime FechaReservacion { get; private set; }

        public DateTime FechaConfirmacion { get; private set; }

        public DateTime? FechaCancelacion { get; private set; }

        public DateTime FechaAnulacion { get; private set; }

        public DateTime? FechaCompletado { get; private set; }

    }
}
