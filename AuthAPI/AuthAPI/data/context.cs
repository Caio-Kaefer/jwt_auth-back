using AuthAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuthAPI.Data // Corrigindo o namespace para "AuthAPI.Data"
{
    public class ApplicationDbContext : DbContext // Corrigindo o nome da classe para "AppDbContext"
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public virtual DbSet<User> usuarios { get; set; } = null!;

    }
}
