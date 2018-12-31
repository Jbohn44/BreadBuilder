using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BreadBuilder.Data;
using BreadBuilder.Models;
using BreadBuilder.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BreadBuilder.Models
{
    //Static class that holds methods to access and process database entry
    public static class DataBaseAccess
    {
        //method to add an item to database
        public static Bread AddToDataBase(AddBreadViewModel addBreadViewModel, int userId, BreadDbContext context)
        {
            //holds the list of recipe items from viewmodel
            List<RecipeItem> RecipeItemList = new List<RecipeItem>();

            //instantiates and saves a new bread recipe
            Bread newBread = new Bread
            {
                Name = addBreadViewModel.Name,
                Instructions = addBreadViewModel.Instructions,
                BakeTemp = addBreadViewModel.BakeTemp,
                BakeTime = addBreadViewModel.BakeTime,
                UserID = userId,
                FermentTime = addBreadViewModel.FermentTime,
                ProofTime = addBreadViewModel.ProofTime,
                RecipeItems = addBreadViewModel.RecipeItems.ToList()

            };

            context.Breads.Add(newBread);

            context.SaveChanges();

            //loops through each recipe item in viewmodel and instantiates a new RecipeItem--Also adds said RecipeItem to a List
            foreach (var item in addBreadViewModel.RecipeItems)
            {
                RecipeItem newRecipeItem = new RecipeItem
                {
                    RecipeIngredient = item.RecipeIngredient,
                    RecipeMeasurement = item.RecipeMeasurement,
                    Bread = newBread
                };
                context.RecipeItems.Add(newRecipeItem);
                context.SaveChanges();
                RecipeItemList.Add(newRecipeItem);
            }

            return newBread;
        }

        //method to pull contents from database and view them
        public static ViewBreadViewModel ViewBread(int id, BreadDbContext context)
        {
            List<RecipeItem> items = context.RecipeItems.Include(i => i.RecipeIngredient)
                            .Include(y => y.RecipeMeasurement)
                            .Where(x => x.Bread.ID == id)
                            .ToList();

            Bread theBread = context.Breads.Single(b => b.ID == id);

            double hydration = Conversions.HydrationLevel(items);

            List<double> totalWeights = Conversions.TotalWeights(items);

            List<double> ingredientPercentages = Conversions.IngredientPercentages(items);

            ViewBreadViewModel viewModel = new ViewBreadViewModel
            {
                Bread = theBread,
                Items = items,
                Hydration = hydration,
                TotalWeights = totalWeights,
                IngredientPercentages = ingredientPercentages
            };

            return viewModel;
        }

        //method to put contents through a conversion to grams method
        public static ViewBreadViewModel ConvertToGramsViewModel(int id, BreadDbContext context)
        {
            List<RecipeItem> items = context.RecipeItems.Include(i => i.RecipeIngredient)
                .Include(y => y.RecipeMeasurement)
                .Where(x => x.Bread.ID == id)
                .ToList();

            Bread theBread = context.Breads.Single(b => b.ID == id);

            double hydration = Conversions.HydrationLevel(items);

            items = Conversions.ConvertItemsToGrams(items);

            List<double> totalWeights = Conversions.TotalWeights(items);

            ViewBreadViewModel viewModel = new ViewBreadViewModel
            {
                Bread = theBread,
                Items = items,
                Hydration = hydration,
                TotalWeights = totalWeights
            };

            return viewModel;

        }

        //method to put contents through a conversion to ounces method
        public static ViewBreadViewModel ConvertToOuncesViewModel(int id, BreadDbContext context)
        {
            List<RecipeItem> items = context.RecipeItems.Include(i => i.RecipeIngredient)
                .Include(y => y.RecipeMeasurement)
                .Where(x => x.Bread.ID == id)
                .ToList();

            Bread theBread = context.Breads.Single(b => b.ID == id);

            double hydration = Conversions.HydrationLevel(items);

            items = Conversions.ConvertItemsToOz(items);

            List<double> totalWeights = Conversions.TotalWeights(items);

            ViewBreadViewModel viewModel = new ViewBreadViewModel
            {
                Bread = theBread,
                Items = items,
                Hydration = hydration,
                TotalWeights = totalWeights
            };

            return viewModel;

        }

        //method processes the edit of contents
        public static EditBreadViewModel EditBreadView(int id, BreadDbContext context)
        {
            List<RecipeItem> items = context.RecipeItems.Include(i => i.RecipeIngredient)
                .Include(y => y.RecipeMeasurement)
                .Where(x => x.Bread.ID == id)
                .ToList();

            Bread theBread = context.Breads.Single(b => b.ID == id);

            EditBreadViewModel viewModel = new EditBreadViewModel
            {
                ID = theBread.ID,
                Name = theBread.Name,
                RecipeItems = items,
                Instructions = theBread.Instructions,
                BakeTime = theBread.BakeTime,
                BakeTemp = theBread.BakeTemp,
                FermentTime = theBread.FermentTime,
                ProofTime = theBread.ProofTime
            };

            return viewModel;
        }
    }
}
