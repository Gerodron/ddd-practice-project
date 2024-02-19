using App.Domain.Abstractions;

namespace App.Domain.Users
{
    public static class UserErros
    {

        public static Error NotFound = new Error(
            "User.Found",
            "No existe el usuario buscado por este id"
        );


        public static Error Invalid = new Error(
            "User.Invalid",
            "Las credenciales son incorrectas"
        );

    }
}
