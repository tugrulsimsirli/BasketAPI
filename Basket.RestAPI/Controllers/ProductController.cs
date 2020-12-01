using System.Threading.Tasks;
using Basket.Business.Services.Interfaces;
using Basket.Common.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Basket.RestAPI.Controllers
{
    [EnableCors("MyPolicy")]
    [ApiController]
    [Route("/api/v1/product/")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        
        [HttpGet]
        [Route("")]
        [SwaggerOperation(Summary = "Product information", Description = "Get all products' information")]
        public async Task<ResponseModel> GetProducts()
        {
            var response = await _productService.GetProducts();
            return response;
        }
    }
}