using GameService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace GameService.Infrastructure.Context
{
    public class GameServiceDbContext : DbContext
    {
        public DbSet<Game> Games { get; set; }  
        public DbSet<Genre> Genres { get; set; }  
        public DbSet<Publisher> Publishers { get; set; }  

        public GameServiceDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
