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
       // public DbSet<RecipeItem> RecipeItems { get; set; }  //Might need this... but at the moment I think I can get by with just an Ingredients and Measurements table

        public DbSet<Measurement> Measurements { get; set; }

        public BreadDbContext(DbContextOptions<BreadDbContext> options)
            :base(options)
        { }


    }
}
