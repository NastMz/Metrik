using Metrik.Domain.Abstractions.Models;
using Metrik.Domain.Entities.Users.Events;
using Metrik.Domain.Entities.Users.ValueObjects;

namespace Metrik.Domain.Entities.Users
{
    /// <summary>
    /// Represents a user in the system.
    /// </summary>
    public sealed class User : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class with the specified identifier.
        /// </summary>
        /// <param name="id">Long unique identifier for the user.</param>
        private User(Guid id, FirstName firstName, LastName lastName, Email email) : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        /// <summary>
        /// Default constructor for the User class.
        /// </summary>
        private User()
        {
        }

        /// <summary>
        /// First name of the user.
        /// </summary>
        public FirstName FirstName { get; private set; }

        /// <summary>
        /// Last name of the user.
        /// </summary>
        public LastName LastName { get; private set; }

        /// <summary>
        /// Email address of the user.
        /// </summary>
        public Email Email { get; private set; }

        /// <summary>
        /// Creates a new user instance with the specified first name, last name, and email address.
        /// </summary>
        /// <param name="firstName">The first name of the user.</param>
        /// <param name="lastName">The last name of the user.</param>
        /// <param name="email">The email address of the user.</param>
        /// <returns>The newly created user instance.</returns>
        public static User Create(FirstName firstName, LastName lastName, Email email)
        {
            var user = new User(Guid.NewGuid(), firstName, lastName, email);

            // Raise a domain event for user creation
            user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id));

            return user;
        }
    }
}
