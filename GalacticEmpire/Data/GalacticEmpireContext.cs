using GalacticEmpire.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace GalacticEmpire.Data
{
    public class GalacticEmpireContext : DbContext
    {
        public GalacticEmpireContext(DbContextOptions<GalacticEmpireContext> options)
            : base(options){}

        public DbSet<Habitant> Habitants { get; set; }
        public DbSet<Specie> Species { get; set; }
        public DbSet<Planet> Planets { get; set; }
    }
}
