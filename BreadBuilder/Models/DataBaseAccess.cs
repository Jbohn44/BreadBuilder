﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BreadBuilder.Data;
using BreadBuilder.Models;
using BreadBuilder.ViewModels;

namespace BreadBuilder.Models
{
    public static class DataBaseAccess
    {

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
    }
}
