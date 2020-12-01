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
    [Route("/api/v1/customer/")]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        
        [HttpGet]
        [Route("")]
        [SwaggerOperation(Summary = "Customer information", Description = "Get all customers' information")]
        public async Task<ResponseModel> GetCustomers()
        {
            var response = await _customerService.GetCustomers();
            return response;
        }
    }
}