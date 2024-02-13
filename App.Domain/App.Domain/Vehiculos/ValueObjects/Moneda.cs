namespace App.Domain.Vehiculos.ValueObjects
{
    public record Moneda(decimal Monto, TipoMoneda TipoMoneda)
    {

        public static Moneda operator +(Moneda cuotaAnterior, Moneda cuotaActual)
        {
            if (cuotaAnterior.TipoMoneda != cuotaActual.TipoMoneda)
            {
                throw new InvalidOperationException("El tipo de moneda debe ser el mismo");
            }

            return  new  Moneda(cuotaAnterior.Monto + cuotaActual.Monto, cuotaAnterior.TipoMoneda);
        }


        public static Moneda Zero() => new(0, TipoMoneda.None);

        public static Moneda Zero(TipoMoneda tipoMoneda) => new(0, tipoMoneda);

        public bool IsZero () => this == Zero(TipoMoneda);

    }
}
