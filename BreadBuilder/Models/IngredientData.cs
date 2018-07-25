using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BreadBuilder.Models
{
    public class IngredientData
    {

        static private List<Ingredient> ingredients = new List<Ingredient>();

        static public List<Ingredient> GetAll()
        {
            return ingredients;
        }

        static public void Add(Ingredient newIngredient)
        {
            ingredients.Add(newIngredient);
        }

        static public Ingredient GetByID(int id)
        {
            return ingredients.Single(i => i.ID == id);
        }
    }
}
