using APITubefetch.Models;
using Microsoft.EntityFrameworkCore;

namespace APITubefetch.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Todo> Todos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite(connectionString: "DataSource=app.db;Cache=Shared");
    }
}

