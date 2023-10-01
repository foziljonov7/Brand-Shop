namespace BrandUzApi.Dtos;

public class UpdateProductDto
{
    public string Name { get; set; }
    public int Size { get; set; }
    public double Price { get; set; }
    public string Description { get; set; }
    public int CategoryId { get; set; }
}