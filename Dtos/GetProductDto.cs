using BrandUzApi.Entity;

namespace BrandUzApi.Dtos;

public class GetProductDto
{
    public GetProductDto(Product entity)
    {
        Id = entity.Id;
        Name = entity.Name;
        Size = entity.Size;
        Price = entity.Price;
        Description = entity.Description;
        ModifiedDate = entity.ModifiedDate;
        CategoryId = entity.CategoryId;
        Category = entity.Category?.Name;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Size { get; set; }
    public double Price { get; set; }
    public string Description { get; set; }
    public DateTime ModifiedDate { get; set; }
    public int CategoryId { get; set; }
    public virtual string Category { get; set; }
}