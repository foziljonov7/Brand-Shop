namespace BrandUzApi.Entity;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Size { get; set; }
    public double Price { get; set; }
    public string Description { get; set; }
    public DateTime ModifiedDate { get; set; } 
    public int CategoryId { get; set; }
    public virtual Category Category { get; set; }
    public virtual List<Product> Items { get; set; }
}