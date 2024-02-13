namespace App.Domain.Shared
{
    public record TipoMoneda
    {
        public static readonly TipoMoneda Usd = new("USD");
        public static readonly TipoMoneda Eur = new("EUR");
        public static readonly TipoMoneda None = new("");

        private TipoMoneda(string codigo)
        {
            Codigo = codigo;
        }

        public string? Codigo { get; init; }

        public static readonly IReadOnlyCollection<TipoMoneda> MonedasDisponibles = new[]
        {
            Usd,
            Eur,
        };

        public static TipoMoneda ObtenerPorCodigo(string codigo)
        {
            return MonedasDisponibles.FirstOrDefault(moneda => moneda.Codigo == codigo)
                ?? throw new ApplicationException("El tipo de moneda ingresado es inválido");
        }
    }
}
