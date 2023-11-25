using AuthAPI.Data;
using Microsoft.AspNetCore.Mvc;
using AuthAPI.Tokens;
using AuthAPI.Jwt;
using AuthAPI.Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace AuthAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly Token _token = new Token();

        [HttpPost ("login")] 
        public async Task<ActionResult> Auth([FromServices] ApplicationDbContext _context, [FromBody] UserLoginInterface postedUser)
        {
           
            var user = await _context.usuarios.FirstOrDefaultAsync(u => u.email == postedUser.email );
            if(user == null)
            {
                return BadRequest("usuario nao encontrado");
            }
            if(user.password != postedUser.password)
            {
                return Unauthorized("senha incorreta");
            }
            var tokenbuilder = new JwtTokenBuilder()
                .AddSecurityKey(JwtSecurityKey.Create(_token.JwtKey()));
            tokenbuilder.AddClaim("nome", user.name);
            tokenbuilder.AddClaim("email", user.email);
            tokenbuilder.AddClaim("age", user.age.ToString());
            var token = tokenbuilder.Build();
            return Ok(JToken.Parse(JsonConvert.SerializeObject(token)).ToString(Formatting.Indented));
        }        
        
    }
}

