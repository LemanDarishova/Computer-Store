using ComputerStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComputerStore.Configuration;

public class CategorieConfiguration : IEntityTypeConfiguration<Categorie>
{
    public void Configure(EntityTypeBuilder<Categorie> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.CategoryName)
            .IsRequired();
        builder.HasMany(x => x.Computers)
            .WithOne(x => x.Categorie)
            .HasForeignKey(x => x.CategorieId);
    }
}
