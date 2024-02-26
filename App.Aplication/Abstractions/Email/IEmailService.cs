namespace App.Domain.Users.ValueObjects
{
    public interface IEmailService 
    {
        Task  SendMailAsync(Email email , string subject, string body);
    }
}
