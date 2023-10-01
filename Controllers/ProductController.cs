using BrandUzApi.Data;
using BrandUzApi.Dtos;
using BrandUzApi.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BrandUzApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public partial class ProductController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateProduct(
        [FromBody] CreateProductDto productDto,
        [FromServices] IAppDbContext dbContext)
    {
        var created = dbContext.Products.Add(new Product
        {
            Id = Guid.NewGuid(),
            Name = productDto.Name,
            Size = productDto.Size,
            Price = productDto.Price,
            Description = productDto.Description,
            ModifiedDate = DateTime.UtcNow,
            CategoryId = productDto.CategoryId
        });

        await dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProduct), new { id = created.Entity.Id }, new GetProductDto(created.Entity));
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts(
        [FromServices] IAppDbContext dbContext,
        [FromQuery] string search)
    {
        var productQuery = dbContext.Products.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
            productQuery = productQuery.Where(p =>
                p.Name.ToLower().Contains(search.ToLower()));

        var products = await productQuery
            .Include(p => p.Category)
            .Select(p => new GetProductDto(p))
            .ToListAsync();

        return Ok(products);
    }
    [HttpGet("/Category/")]
    public async Task<IActionResult> GetCategory(
        [FromQuery] string cId,
        [FromServices] IAppDbContext dbContext)
    {
        var productCategory = dbContext.Products.AsQueryable();

        if (!string.IsNullOrWhiteSpace(cId))
            productCategory = productCategory.Where(c =>
                c.CategoryId.ToString().ToLower().Contains(cId.ToLower()));

        var category = await productCategory.ToListAsync();

        return Ok(category);
    }

    [HttpGet("{id}")] 
    public async Task<IActionResult> GetProduct(
        [FromRoute] Guid id,
        [FromServices] IAppDbContext dbContext)
    {
        var product = await dbContext.Products
            .Where(p => p.Id == id)
            .Include(p => p.Category)
            .FirstOrDefaultAsync();
        if (product is null)
            return NotFound();
        return Ok(new GetProductDto(product));
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(
        [FromRoute] Guid id,
        [FromServices] IAppDbContext dbContext,
        UpdateProductDto productDto)
    {
        var product = await dbContext.Products
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product is null)
            return NotFound();

        product.Name = productDto.Name;
        product.Size = productDto.Size;
        product.Price = productDto.Price;
        product.Description = productDto.Description;
        product.CategoryId = productDto.CategoryId;

        await dbContext.SaveChangesAsync();
        return Ok();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(
        [FromRoute] Guid id,
        [FromServices] IAppDbContext dbContext)
    {
        var product = await dbContext.Products
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product is null)
            return NotFound();

        dbContext.Products.Remove(product);
        await dbContext.SaveChangesAsync();

        return Ok();
    }
}