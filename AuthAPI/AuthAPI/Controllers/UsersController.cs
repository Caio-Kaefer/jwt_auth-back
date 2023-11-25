using AuthAPI.Data;
using AuthAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpPost("cadastro")]
        public async Task<ActionResult> Signup([FromBody] User postedUser)
        {
            try
            {
                if (await _context.usuarios.AnyAsync(u => u.email == postedUser.email))
                {
                    return BadRequest("Email já cadastrado");
                }

                _context.usuarios.Add(postedUser);
                await _context.SaveChangesAsync();

                return Ok("Usuário cadastrado");
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine(ex);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var user = await _context.usuarios.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return BadRequest("Usuário não encontrado");
            }

            _context.usuarios.Remove(user);
            await _context.SaveChangesAsync();

            return Ok("Usuário removido com sucesso");
        }



        [HttpGet("usuarios")]
        public async Task<ActionResult> GetUsers()
        {
            try
            {
                var users = await _context.usuarios.ToListAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine(ex);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
