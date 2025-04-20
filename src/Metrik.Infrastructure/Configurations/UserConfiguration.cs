using Metrik.Domain.Entities.Users;
using Metrik.Domain.Entities.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Metrik.Infrastructure.Configurations
{
    /// <summary>
    /// Configuration for the User entity.
    /// </summary>
    internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.FirstName)
                .HasMaxLength(200)
                .HasConversion(firstName => firstName.Value, value => new FirstName(value));

            builder.Property(u => u.LastName)
                .HasMaxLength(200)
                .HasConversion(lastName => lastName.Value, value => new LastName(value));

            builder.Property(u => u.Email)
                .HasMaxLength(200)
                .HasConversion(email => email.Value, value => new Domain.Entities.Users.ValueObjects.Email(value));

            builder.HasIndex(u => u.Email).IsUnique();
        }
    }
}
