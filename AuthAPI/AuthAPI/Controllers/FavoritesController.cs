using AuthAPI.Data;
using AuthAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AuthAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FavoriteController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FavoriteController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("addfavorite")]
        public async Task<ActionResult> AddFavorite([FromBody] AddFavoriteInterface favorite)
        {
            if (ModelState.IsValid)
            {
                var existingFavorite = _context.favoritos.FirstOrDefault(x => x.DrinkId == favorite.DrinkId);
                if (existingFavorite != null) {
                    return BadRequest("favorito ja adicionado");
                }
                _context.favoritos.Add(favorite);
                await _context.SaveChangesAsync();
                return Ok(favorite);
            }
            return BadRequest(ModelState);
        }

        [HttpGet("getfavorite")]
        public async Task<ActionResult> GetFavorite(int id)
        {
            if (ModelState.IsValid)
            {
                var favList = await _context.favoritos
                    .Where(f => f.UserId == id)
                    .ToListAsync(); 

                return Ok(favList);
            }
            return BadRequest(ModelState);
        }

    }
}
