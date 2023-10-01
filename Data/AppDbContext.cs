using BrandUzApi.Entity;
using Microsoft.EntityFrameworkCore;

namespace BrandUzApi.Data;

public class AppDbContext : DbContext, IAppDbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options)
        :base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .HasKey(p => p.Id);
        modelBuilder.Entity<Product>()
            .Property(p => p.Name)
            .HasMaxLength(50)
            .IsRequired();
        modelBuilder.Entity<Product>()
            .Property(p => p.Price)
            .IsRequired();
        modelBuilder.Entity<Product>()
            .Property(p => p.Size)
            .IsRequired();
        modelBuilder.Entity<Product>()
            .Property(P => P.Description)
            .HasMaxLength(255);
        modelBuilder.Entity<Product>()
            .Property(p => p.ModifiedDate)
            .HasDefaultValue(DateTime.UtcNow);
        modelBuilder.Entity<Product>()
            .HasOne(c => c.Category)
            .WithMany()
            .HasForeignKey(p => p.CategoryId)
            .IsRequired();

        modelBuilder.Entity<Category>()
            .HasKey(c => c.Id);
        modelBuilder.Entity<Category>()
            .Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(50);
        modelBuilder.Entity<Category>()
            .HasData(
                new { Id = 1, Name = "Shim" },
                new { Id = 2, Name = "Koylak" },
                new { Id = 3, Name = "FUtbolka" },
                new { Id = 4, Name = "Oyoq kiyim" });

        modelBuilder.Entity<Product>()
            .HasMany(p => p.Items)
            .WithMany();
    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => base.SaveChangesAsync(cancellationToken);
}