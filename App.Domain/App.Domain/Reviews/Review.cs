using App.Domain.Abstractions;
using App.Domain.Alquileres;
using App.Domain.Alquileres.Enums;
using App.Domain.Reviews.Events;

namespace App.Domain.Reviews
{
    public sealed class Review : DomainEntity
    {
        private Review(
            Guid id,
            Guid? vehiculoId,
            Guid alquilerId,
            Guid? userId,
            int rating,
            string comentario,
            DateTime fechaCreacion
        ) : base(id)
        {
            VehiculoId = vehiculoId;
            AlquilerId = alquilerId;
            UserId = userId;
            Rating = rating;
            Comentario = comentario;
            FechaCreacion = fechaCreacion;
        }

        public static Result<Review> Create(
            Alquiler alquiler,
            int rating,
            string comentario,
            DateTime fechaCreacion
            )
        {
            if(alquiler.AlquilerEstado != AlquilerStatus.Completado)
            {
                return Result.Failure<Review>(ReviewErrors.NotEligible);
            }
            var review = new Review(Guid.NewGuid(), alquiler.VehiculoId, alquiler.Id, alquiler.UsuarioId, rating, comentario, fechaCreacion);
            review.PublishDomainEvent(new ReviewCreatedDomainEvent(review.Id));
            return review;
        }

        public Guid? VehiculoId { get; private set; }

        public Guid? AlquilerId { get; private set; }

        public Guid? UserId { get; private set; }

        public int? Rating { get; private set; }

        public string? Comentario { get; private set; }

        public DateTime? FechaCreacion { get; private set; }
    }
}
