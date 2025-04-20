using Metrik.Domain.Entities.Accounts;
using Metrik.Domain.Entities.Accounts.ValueObjects;
using Metrik.Domain.Entities.Users;
using Metrik.Domain.Shared.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Metrik.Infrastructure.Configurations
{
    /// <summary>
    /// Configuration for the Account entity.
    /// </summary>
    internal sealed class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("accounts");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Name)
                .HasMaxLength(200)
                .HasConversion(name => name.Value, value => new AccountName(value));

            builder.Property(a => a.Type)
                .HasMaxLength(100)
                .HasConversion(type => type.ToString(), value => new AccountType(value));

            builder.OwnsOne(a => a.Balance, balanceBuilder =>
            {
                balanceBuilder.Property(b => b.Currency)
                    .HasConversion(currency => currency.Code, code => Currency.FromCode(code));
            });

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(a => a.UserId);

            builder.HasIndex(a => a.Name)
                .IsUnique();

            builder.Property<uint>("Version")
                .IsRowVersion()
                .IsConcurrencyToken();
        }
    }
}
