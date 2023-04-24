
using Microsoft.EntityFrameworkCore;
using RentCarApp.Entities;
using RentCarApp.Components.Models;

namespace RentCarApp.Data
{
    public class RentCarAppDbContext : DbContext
    {
        public RentCarAppDbContext(DbContextOptions<RentCarAppDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<Entities.Car> Cars { get; set; }
        public DbSet<CarsCatalog> CarsCatalog { get; set; }
    }
}

