namespace App.Domain.Abstractions
{
    public record Error(string codeError, string descriptionError)
    {
        public static Error None = new(string.Empty, string.Empty);

        public static Error NullValue = new("Error.NullValue", "Un valor null fue ingresado");
    }
}
