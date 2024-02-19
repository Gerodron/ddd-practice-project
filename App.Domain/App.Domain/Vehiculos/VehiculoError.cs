using App.Domain.Abstractions;

namespace App.Domain.Vehiculos
{
    public static class VehiculoError
    {
        public static Error NotFound = new Error(
            "Vehiculo.NotFound",
            "No se encontró ningún vehículo con el identificador proporcionado."
        );
    }
}
