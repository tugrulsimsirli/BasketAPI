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
    [Route("/api/v1/basket/")]
    public class BasketController : Controller
    {
        private readonly IBasketService _basketService;
        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }
        
        [HttpGet]
        [Route("")]
        [SwaggerOperation(Summary = "Add item to basket", Description = "Add item process of basket")]
        public async Task<ResponseModel> GetBasket([FromQuery]int customerId)
        {
            var response = await _basketService.GetBasket(customerId);
            return response;
        }
        
        [HttpPost]
        [Route("")]
        [SwaggerOperation(Summary = "Add item to basket", Description = "Add item process of basket")]
        public async Task<ResponseModel> AddBasket([FromQuery]int customerId, [FromQuery] int productId, [FromQuery] int amount)
        {
            var response = await _basketService.AddItem(customerId, productId, amount);
            return response;
        }
    }
}