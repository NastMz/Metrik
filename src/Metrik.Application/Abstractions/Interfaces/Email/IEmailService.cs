namespace Metrik.Application.Abstractions.Interfaces.Email
{
    /// <summary>
    /// Interface for sending emails.
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Sends an email asynchronously.
        /// </summary>
        /// <param name="recipient">The recipient's email address.</param>
        /// <param name="subject">The subject of the email.</param>
        /// <param name="body">The body of the email.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task SendAsync(Domain.Entities.Users.ValueObjects.Email recipient, string subject, string body);
    }
}
