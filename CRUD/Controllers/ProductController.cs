using CRUD.DAL;
using CRUD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUD.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly MyAppDbContext _context;
        public ProductController(MyAppDbContext context)
        {
            _context = context;
        }

        // FOR GETTING ALL THE PRODUCTS FROM THE TABLE IN DB
        [HttpGet]
        public IActionResult Get() {

            try
            {
                var products = _context.Products.ToList();

                if (products.Count == 0)
                {
                    return NotFound("Products are not available");
                }

                return Ok(products);
            }
            catch (Exception ex) {

                return BadRequest(ex.Message);
            }
        }

        // FOR GETTING PRODUCT WITH ID FROM THE TABLE IN DB
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var product = _context.Products.Find(id);
                if (product == null)
                {
                    return NotFound($"Product details not found with id{id}");
                }
                return Ok(product);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        // POST METHOD TO ADD DATA IN DB
        [HttpPost]
        public IActionResult Post(Product model)
        {
            try { 
            _context.Add(model);
                _context.SaveChanges();
                return Ok("Product created!!");
            } catch (Exception ex) { return BadRequest("Couldn't add product"); }
        }

        // PUT METHOD TO UPDATE PRODUCT
        [HttpPut]
        public IActionResult Put(Product model)
        {
            try {
                if(model==null || model.Id == 0)
                {
                    if (model == null) { return BadRequest("Model data is invalid!!"); }
                    else if (model.Id == 0) { return BadRequest($"Product with id {model.Id} is invalid!!"); }
                }

                var product = _context.Products.Find(model.Id);
                if(product == null) { return NotFound($"Product not found with id {model.Id}"); }

                product.ProductName = model.ProductName;
                product.Price = model.Price;
                product.Quantity = model.Quantity;
                _context.SaveChanges();
                return Ok("Product details updated successfully");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            
        }
        // DELETE METHOD FOR DELETING PRODUCT WITH ID
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                var product = _context.Products.Find(id);
                if (product == null) { return NotFound($"Product with id {id} is not found!!"); }
                _context.Products.Remove(product);
                _context.SaveChanges();
                return Ok($"Product with id {id} is deleted");
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}
