using System;
using Merso.Data;
using Microsoft.EntityFrameworkCore;

namespace Merso.Models
{

    public class BrandContext : ApplicationDbContext
    {
        public BrandContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Brand> Brands { get; set; }
    }


    public class Brand
	{
        public int Id { get; set; }
        public string Name { get; set; }
    }


}

