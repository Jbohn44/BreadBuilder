using BreadBuilder.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BreadBuilder.Models
{
    public class IngredientData
    {
        private BreadDbContext context;

        private List<Ingredient> AllIngredients = new List<Ingredient>();
        
        public IngredientData(BreadDbContext dbContext)
        {
            context = dbContext;
        }
      
        public  List<Ingredient> GetAll(BreadDbContext context)
        {
            AllIngredients = context.Ingredients.ToList();
            return (AllIngredients);
        }

        internal static List<Ingredient> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
