using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IntegrationModule.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MySecuredController : ControllerBase
    {
        // GET: api/<MySecuredController>
        [HttpGet]
        [Authorize(Roles = "Admin,ReadWrite")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<MySecuredController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [AllowAnonymous]
        [HttpGet("name-and-role")]
        public object GetNameAndRole()
        {
            string name = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            string role = User.FindFirst(ClaimTypes.Role)?.Value;

            return new
            {
                Name = name,
                Role = role
            };
        }


        [AllowAnonymous]
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<MySecuredController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MySecuredController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
 