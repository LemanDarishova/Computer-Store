using ComputerStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Drawing;

namespace ComputerStore.Configuration;

public class SizeConfiguration : IEntityTypeConfiguration<Sizee>
{
    public void Configure(EntityTypeBuilder<Sizee> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(20);
        builder.HasMany(x => x.Computers)
            .WithOne(x => x.Sizes)
            .HasForeignKey(x => x.SizesId);
    }
}