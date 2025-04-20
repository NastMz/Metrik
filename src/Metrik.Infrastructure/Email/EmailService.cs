using Metrik.Application.Abstractions.Interfaces.Email;

namespace Metrik.Infrastructure.Email
{
    /// <summary>
    /// Implementation of the IEmailService interface for sending emails.
    /// </summary>
    internal sealed class EmailService : IEmailService
    {
        /// <inheritdoc />
        public Task SendAsync(Domain.Entities.Users.ValueObjects.Email recipient, string subject, string body)
        {
            // Here implement the logic to send an email.

            return Task.CompletedTask;
        }
    }
}
