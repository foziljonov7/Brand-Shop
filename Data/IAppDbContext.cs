using BrandUzApi.Entity;
using Microsoft.EntityFrameworkCore;

namespace BrandUzApi.Data;
public interface IAppDbContext 
{
    DbSet<Product> Products { get; set;}
    DbSet<Category> Categories { get; set;}
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}