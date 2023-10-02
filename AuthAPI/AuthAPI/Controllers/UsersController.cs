using AuthAPI.Data;
using AuthAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpPost("cadastro")]
        public async Task<ActionResult> signup([FromServices] context _context, [FromBody] User postedUser)
        {
            var existingUser = await _context.usuarios.FirstOrDefaultAsync(u => u.email == postedUser.email);
            if (existingUser != null)
            {
                return BadRequest("Email já cadastrado");
            }

             _context.Add(postedUser);
             await _context.SaveChangesAsync();

            return Ok("usuario cadastrado");
        }

        [HttpGet("usuarios")]
        public async Task<ActionResult> getusers([FromServices] context _context)
        {
            var users = await _context.usuarios.ToListAsync();
            return Ok(users);
        }
    }
}
