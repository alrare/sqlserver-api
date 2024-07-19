using Microsoft.AspNetCore.Mvc;
using MSqlServerDbCRUD.Data;
using SqlServerDbCRUD.Model;

namespace SqlServerDbCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly DataContext _context;

        public ProductController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllProducts")]
        public async Task<ActionResult<List<Product>>> Get()
        {
            return Ok(await _context.Products.ToListAsync());
        }

        [HttpGet("GetProductById/{id}")]
        public async Task<ActionResult<List<Product>>> Get(int id)
        {
            var prod = await _context.Products.FindAsync(id);
            if (prod == null)
                return BadRequest("Product not found.");
            return Ok(prod);
        }

        [HttpPost("AddProduct")]
        public async Task<ActionResult<Product>> Add(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return Ok(await _context.Products.ToListAsync());
        }

        [HttpPut("UpdateProduct")]
        public async Task<ActionResult<Product>> UpdateProduct(Product request)
        {
            var dbProd = await _context.Products.FindAsync(request.Id);
            if (dbProd == null)
                return BadRequest("Product not found.");

            dbProd.Name = request.Name;
            dbProd.Description = request.Description;

            await _context.SaveChangesAsync();

            return Ok(await _context.Products.ToListAsync());
        }

        [HttpDelete("DeleteProduct/{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var dbProd = await _context.Products.FindAsync(id);
            if (dbProd == null)
                return BadRequest("Product not found.");

            _context.Products.Remove(dbProd);
            await _context.SaveChangesAsync();

            return Ok(await _context.Products.ToListAsync());
        }

    }
}
