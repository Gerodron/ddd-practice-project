using App.Domain.Abstractions;
using App.Domain.Shared;

namespace App.Domain.Reviews.ValueObjects
{
    public sealed record Rating
    {
        private Rating(int value)
        {
            Value = value;  
        }

        public static Result<Rating> Create(int value)
        {
            if (value < 1 || value > 5)
            {
                return Result.Failure<Rating>(Invalid);
            };
            return new Rating(value);
        }

        public static readonly Error Invalid = new Error(
           "Rating.Invalid",
           "El rating es invalido"
        );

        public int Value { get; init; }
    }
}
