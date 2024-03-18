using System;
using Merso.Data;
using Microsoft.EntityFrameworkCore;

namespace Merso.Models
{
    public class TasitContext : ApplicationDbContext
    {
        public TasitContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Tasit> Tasits { get; set; }
    }

    public class Tasit
	{
        public int Id { get; set; }
        public string CarPlate { get; set; }
        public int Km { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string GearType { get; set; }

        // Foreign key relationship with Brand entity
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
    }
}

