using App.Domain.Abstractions;

namespace App.Domain.Users
{
    public static class UserErros
    {

        public static Error NotFound = new Error(
            "User.NotFound",
            "No existe el usuario buscado por este id"
        );


        public static Error InvalidCredentials = new Error(
            "User.InvalidCredentials",
            "Las credenciales son incorrectas"
        );

    }
}
