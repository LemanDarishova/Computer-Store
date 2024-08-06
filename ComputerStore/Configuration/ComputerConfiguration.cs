using ComputerStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComputerStore.Configuration
{
    public class ComputerConfiguration : IEntityTypeConfiguration<Computer>
    {
        public void Configure(EntityTypeBuilder<Computer> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Model)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(x => x.Brands)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(x => x.Price)
                .IsRequired()
                .HasColumnType("decimal(5,3)");

            builder.Property(x => x.Stock)
                .IsRequired();

            builder.HasMany(x => x.Orders)
                .WithOne(x => x.Computer)
                .HasForeignKey(x => x.ComputerId);
        }
    }
}
