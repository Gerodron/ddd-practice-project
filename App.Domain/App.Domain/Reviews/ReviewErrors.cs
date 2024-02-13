using App.Domain.Abstractions;

namespace App.Domain.Reviews
{
    public static class ReviewErrors
    {
        public static Error NotEligible => new Error(
            "Review.NotEligible",
            "Actualmente no cumple con los requisitos para dejar una reseña sobre este alquiler. Agradecemos su comprension."
        );
    }
}
