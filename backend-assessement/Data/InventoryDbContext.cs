using backend_assessement.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace backend_assessement.Data
{
    public class InventoryDbContext : DbContext
    {
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options)
        {

        }
        public DbSet<Reservation> reservation { get; set; }

    }
}
