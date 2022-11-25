using Core.JwtBuilder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace SampleJwtBuilderAuthenticationApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SampleController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public SampleController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            return Ok();
        }

        [HttpGet]
        [Route("Token/Generate")]
        public IActionResult GenerateToken()
        {
            return Ok(JwtTokenGenerator.GenerateToken(_configuration, new[] { new Claim("source", "identity") }));
        }
    }
}