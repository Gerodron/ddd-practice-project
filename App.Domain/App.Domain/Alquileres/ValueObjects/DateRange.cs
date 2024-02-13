namespace App.Domain.Alquileres.ValueObjects
{
    public sealed record DateRange
    {
        public DateOnly FechaInicioReserva { get; init; }
        public DateOnly FechaFinReserva { get; init; }

        private DateRange(DateOnly fechaInicioReserva, DateOnly fechaFinReserva)
        {
            FechaInicioReserva = fechaInicioReserva;
            FechaFinReserva = fechaFinReserva;
        }

        public static DateRange Create(DateOnly fechaInicioReserva, DateOnly fechaFinReserva)
        {
            if (fechaInicioReserva >= fechaFinReserva)
            {
                throw new ArgumentException("La fecha de inicio de la reserva debe ser anterior a la fecha de fin de la reserva.");
            }

            return new DateRange(fechaInicioReserva, fechaFinReserva);
        }

        public int CalcularDuracionEnDias()
        {
            return FechaFinReserva.DayNumber - FechaInicioReserva.DayNumber;
        }
    }
}
