using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SampleJwtBuilderAuthenticationApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SampleController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}