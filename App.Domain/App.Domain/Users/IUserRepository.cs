namespace App.Domain.Users
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(Guid id, CancellationToken cancellation = default);

        void Add(User user, CancellationToken cancellation = default);

        
    }
}
