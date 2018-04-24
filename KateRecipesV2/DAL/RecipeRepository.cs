using KateRecipesV2.Abstract;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using KateRecipesV2.Models;

namespace KateRecipesV2.DAL
{
    public class RecipeRepository : IRecipeRepository
    {

        private RecipesDBEntities context = new RecipesDBEntities();


        public IEnumerable<Recipe> Recipes
        {
            get { return context.Recipe; }
        }

        public void SaveRecipe(Recipe recipe)
        {
            if (recipe.Id == 0)
            {
                context.Recipe.Add(recipe);
            }
            else
            {
                Recipe dbEntry = context.Recipe.Find(recipe.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = recipe.Name;
                    dbEntry.ShortDescryption = recipe.ShortDescryption;
                    dbEntry.Components = recipe.Components;
                    dbEntry.Implementations = recipe.Implementations;
                    dbEntry.ImageData = recipe.ImageData;
                    dbEntry.ImageMimeType = recipe.ImageMimeType;
                }
            }
            context.SaveChanges();
        }

        public Recipe DeleteRecipe(int productID)
        {
            Recipe dbEntry = context.Recipe.Find(productID);
            if (dbEntry != null)
            {
                context.Recipe.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

    }
}