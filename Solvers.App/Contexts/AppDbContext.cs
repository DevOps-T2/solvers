
using Microsoft.EntityFrameworkCore;
using Solvers.App.Models;


namespace Contexts.Solvers
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Solver> Solvers { get; set; }



    }
}
