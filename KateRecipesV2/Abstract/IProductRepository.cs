using KateRecipesV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KateRecipesV2.Abstract
{
  public interface IRecipeRepository
    {
        IEnumerable<Recipe> Recipes { get; }
        void SaveRecipe(Recipe recipe);
        Recipe DeleteRecipe(int recipeID);

    }
}
