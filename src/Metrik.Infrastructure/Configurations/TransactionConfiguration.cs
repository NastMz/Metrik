using Metrik.Domain.Entities.Accounts;
using Metrik.Domain.Entities.Categories;
using Metrik.Domain.Entities.Transactions;
using Metrik.Domain.Entities.Transactions.ValueObjects;
using Metrik.Domain.Entities.Users;
using Metrik.Domain.Shared.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Metrik.Infrastructure.Configurations
{
    internal sealed class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("transactions");

            builder.HasKey(t => t.Id);

            builder.OwnsOne(t => t.Amount, amountBuilder =>
            {
                amountBuilder.Property(a => a.Currency)
                    .HasConversion(currency => currency.Code, code => Currency.FromCode(code));
            });

            builder.Property(t => t.Type)
                .HasMaxLength(100)
                .HasConversion<int>();

            builder.Property(t => t.Description)
                .HasMaxLength(500)
                .HasConversion(description => description.Value, value => new TransactionDescription(value));

            builder.Property(t => t.Date);

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<Account>()
                .WithMany()
                .HasForeignKey(t => t.AccountId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<Category>()
                .WithMany()
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
