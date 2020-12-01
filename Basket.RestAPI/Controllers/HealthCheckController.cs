using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Basket.RestAPI.Controllers
{
    [EnableCors("MyPolicy")]
    [ApiController]
    [Route("/api/v1/")]
    public class HealthCheckController : Controller
    {
        [HttpGet]
        [Route("ping")]
        public JsonResult HealthCheck()
        {
            return Json("Healthy");
        }
    }
}