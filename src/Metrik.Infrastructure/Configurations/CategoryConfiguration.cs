using Metrik.Domain.Entities.Categories;
using Metrik.Domain.Entities.Categories.ValueObjects;
using Metrik.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Metrik.Infrastructure.Configurations
{
    /// <summary>
    /// Configuration for the Category entity.
    /// </summary>
    internal sealed class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("categories");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .HasMaxLength(200)
                .HasConversion(name => name.Value, value => new CategoryName(value));

            builder.Property(c => c.Type)
                .HasMaxLength(200)
                .HasConversion(type => type.Value, value => new CategoryType(value));

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
