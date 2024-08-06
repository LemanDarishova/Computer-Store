using ComputerStore.Configuration;
using ComputerStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ComputerStore.Data;

public class ComputerStoreDbContext : DbContext
{

    public DbSet<Categorie> Categories { get; set; }
    public DbSet<Computer> Computers { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Sizee> Sizes { get; set; }

    public ComputerStoreDbContext(DbContextOptions<ComputerStoreDbContext> options) : base(options)
    {   
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ComputerConfiguration());
        modelBuilder.ApplyConfiguration(new OrderConfiguration());
        modelBuilder.ApplyConfiguration(new CustomerConfiguration());
        modelBuilder.ApplyConfiguration(new CategorieConfiguration());
        modelBuilder.ApplyConfiguration(new SizeConfiguration());

        base.OnModelCreating(modelBuilder);
    }

}
