namespace BrandUzApi.Dtos;

public class SetProductDto
{
    public Guid MyProperty { get; set; }
    public IEnumerable<GetProductDto> SetItems {get; set;}
}