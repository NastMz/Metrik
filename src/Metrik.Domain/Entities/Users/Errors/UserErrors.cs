using Metrik.Domain.Abstractions.Models;

namespace Metrik.Domain.Entities.Users.Errors
{
    /// <summary>
    /// Static class containing error definitions related to users.
    /// </summary>
    public static class UserErrors
    {
        /// <summary>
        /// Error indicating that a user was not found.
        /// </summary>
        public static readonly Error NotFound = new(
            "User.NotFound",
            "The user with the specified identifier was not found.",
            ErrorType.NotFound,
            "Errors.User.NotFound"
        );
    }
}
