using Inventory.Service.API.Database;
using Inventory.Service.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Service.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly InventoryContext _context;

        public ProductController(InventoryContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetProducts()
        {
            var listProduct = _context.inventories.ToList();           
            return Ok(listProduct);
        }
        //[HttpPost]
        //public IActionResult CreateProduct(Database.InventoryEntity productEntity)
        //{
        //    var result = new ListProduct();
        //    result.add(productEntity);
        //    return Ok(result.getProducts());
        //}
    }
}
