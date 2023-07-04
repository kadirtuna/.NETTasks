using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaginationWEBAPI.BLL.Infrastructure;
using PaginationWEBAPI.BLL.Services;
using PaginationWEBAPI.DAL.DTO;

namespace PaginationWEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("GetProductById/{id}")]
        public async Task<ActionResult> GetProductById(int id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);

                return Ok(product);
            }
            catch (Exception exception)
            {
                return BadRequest("There is a server-side error with: " + exception);
            }
        }

        [HttpGet("GetAllProducts")]
        public async Task<ActionResult<List<ProductDTO>>> GetAllProducts()
        {
            try
            {
                var products = await _productService.GetAllProductsAsync();

                return Ok(products);
            }
            catch (Exception exception)
            {
                return BadRequest("There is a server-side error with: " + exception);
            }
        }

        [HttpGet("GetProductsByPage/{page}")]
        public async Task<ActionResult> GetProductsByPage(int page)
        {
            try
            {
                return Ok(await _productService.GetProductsByPage(page));
            }
            catch (Exception exception)
            {
                return BadRequest("There is a server-side error with: " + exception);
            }
        }

        [HttpPost("InsertProduct")]
        public async Task<ActionResult> InsertProduct(CreateProductDTO createProductDTO)
        {
            try
            {
                return Ok(await _productService.InsertProductAsync(createProductDTO));
            }
            catch (Exception exception)
            {
                return BadRequest("There is a server-side error with: " + exception);
            }
        }

        [HttpDelete("DeleteProduct/{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            try
            {
                return Ok(await _productService.DeleteProductAsync(id));
            }
            catch (Exception exception)
            {
                return BadRequest("There is a server-side error with: " + exception);
            }
        }
    }
}
