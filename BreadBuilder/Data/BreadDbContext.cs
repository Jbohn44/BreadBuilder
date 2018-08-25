using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BreadBuilder.Models;
using Microsoft.EntityFrameworkCore;

namespace BreadBuilder.Data
{
    public class BreadDbContext : DbContext
    {
        public DbSet<Bread> Breads { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RecipeItem> RecipeItems { get; set; }
        //public DbSet<BreadRecipeItem> BreadRecipeItems { get; set; }

        public DbSet<Measurement> Measurements { get; set; }

        public BreadDbContext(DbContextOptions<BreadDbContext> options)
            :base(options)
        { }


        // do not need this
        /*
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BreadRecipeItem>()
                .HasKey(c => new { c.BreadID, c.RecipeItemID });
        }*/

    }
}
