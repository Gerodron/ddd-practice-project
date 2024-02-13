using App.Domain.Abstractions;

namespace App.Domain.Alquileres
{
    public static class AlquilerErrors
    {
        public static Error NotFound = new Error(
            "Alquiler.Found",
            "El alquiler con el identificador especificado no fue encontrado en el sistema."
        );

        public static Error OverLap = new Error(
            "Alquiler.Overlap",
            "El alquiler está siendo solicitado por 2 o más clientes al mismo tiempo en la misma fecha."
        );

        public static Error NotReserved = new Error(
            "Alquiler.NotReserved",
            "El alquiler no está reservado. Por favor, realice una reserva antes de continuar."
        );

        public static Error NotConfirmed = new Error(
            "Alquiler.NotConfirmed",
            "El alquiler no está confirmado. Espere la confirmación antes de continuar."
        );

        public static Error AlreadyStarted = new Error(
            "Alquiler.AlreadyStarted",
            "El alquiler ya ha comenzado. No se puede realizar ninguna acción sobre un alquiler que ya ha empezado."
        );

    }
}
