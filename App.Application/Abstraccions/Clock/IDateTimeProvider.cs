namespace App.Application.Abstraccions.Clock
{
    public interface IDateTimeProvider
    {
        DateTime CurrentTime { get; }
    }
}
