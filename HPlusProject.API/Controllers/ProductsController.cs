using HPlusProject.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HPlusProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        //advanced data retrieval tutorial next
        //versioning apis
        //securing apis (enforcing https/making api friendly to js/ jwt tokens etc)
        //api design (api more restful/predictable/friendly)
        //asp.net core security

        private readonly ShopContext _shopContext;

        public ProductsController(ShopContext dbContext)
        {
            _shopContext = dbContext;

            _shopContext.Database.EnsureCreated();
        }
        [HttpGet]
        public async Task<ActionResult> GetAllProducts()
        {
            var products = await _shopContext.Products
                                                .ToListAsync();
            if (products.Count == 0 || products == null) return NotFound();

            return Ok(products);
        }
        [HttpGet]
        [Route("available")]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProductsInStock()
        {
            var products = await _shopContext.Products.Where(p => p.IsAvailable).ToListAsync();
            if (!products.Any()) return NotFound();

            return Ok(products);
        }
        [HttpGet]
        [Route("by-id/{id}")]
        public async Task<ActionResult> GetProductById(int id)
        {
            var product = await _shopContext.Products
                                            .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null) return NotFound();
            return Ok(product);
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> UpdateProduct(int id, [FromBody] Product updatedProduct)
        {
            if (id != updatedProduct.Id) return BadRequest("Incorrect Id");

            _shopContext.Entry(updatedProduct).State = EntityState.Modified;
            try
            {
                var requestUpdateProduct = _shopContext.Products.Update(updatedProduct);
                await _shopContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                if (!_shopContext.Products.Any(p => p.Id == id)) return NotFound();
                else throw;

            }
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> CreateNewProduct(Product newProduct)
        {
            if (newProduct == null || !ModelState.IsValid) return BadRequest("Product data is missing.");


            var requestNewProduct = await _shopContext.Products.AddAsync(newProduct);
            await _shopContext.SaveChangesAsync();

            if (string.IsNullOrEmpty(newProduct.Name) || newProduct.Price <= 0) return BadRequest("Invalid product details.");
            //return CreatedAtAction("Product: ",requestNewProduct); what I thought was right
            return CreatedAtAction(
                   nameof(GetProductById),
                   new { id = newProduct.Id },
                   newProduct);

        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> PartiallyUpdateProduct(int id, [FromBody] Product updatedFields)
        {
            var existingProduct = await _shopContext.Products.FindAsync(id);
            if (existingProduct == null) return NotFound();

            // Update only the provided fields (assuming `null` means "no change")
            if (updatedFields.Name != null) existingProduct.Name = updatedFields.Name;
            if (updatedFields.Price != default) existingProduct.Price = updatedFields.Price;
            if (updatedFields.Description != null) existingProduct.Description = updatedFields.Description;
            if (!updatedFields.IsAvailable) existingProduct.IsAvailable = updatedFields.IsAvailable;
            if (updatedFields.CategoryId != existingProduct.CategoryId) existingProduct.CategoryId = updatedFields.CategoryId;

            try
            {
                await _shopContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {

            var existingProduct = await _shopContext.Products.FindAsync(id);
            if (existingProduct == null) return NotFound();

            _shopContext.Products.Remove(existingProduct);
            try
            {
                await _shopContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

    }
}