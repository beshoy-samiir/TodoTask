using Microsoft.EntityFrameworkCore;
using Todo_backend.Models;

namespace Todo_backend.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<TodoModel> Todos { get; set; }
    }
}
