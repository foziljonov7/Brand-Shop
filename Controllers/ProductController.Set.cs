using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrandUzApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BrandUzApi.Controllers
{
    public partial class ProductController : ControllerBase
    {
        [HttpPost("{id}/set")]
        public async Task<IActionResult> CreateSet(
            [FromRoute] Guid id,
            [FromBody] IEnumerable<Guid> itemIds,
            [FromServices] IAppDbContext dbContext,
            CancellationToken cancellationToken = default)
        {
            var product = await dbContext.Products
                .Where(p => p.Id == id)
                .Include(c => c.Category)
                .FirstOrDefaultAsync(cancellationToken);
            
            var setCategory = await dbContext.Categories
                .FirstAsync(c => c.Name == "Set", cancellationToken);
            
            if(product is null)
                return NotFound(); 
            
            if(product.CategoryId != setCategory.Id)
                return BadRequest("This product does not have category {set}");

            var items = await dbContext.Products
                .Where(p => itemIds.Contains(p.Id))
                .ToListAsync(cancellationToken);

            if(items.Count < itemIds.Count())
                return BadRequest("Some items do not exist in system");
            
            if(items.Any(p => p.CategoryId == setCategory.Id))
                return BadRequest("Some items have category {set}. You cannot add them to set again");

            product.Items = items;
            await dbContext.SaveChangesAsync(cancellationToken);

            return Ok();
        }
    }
}