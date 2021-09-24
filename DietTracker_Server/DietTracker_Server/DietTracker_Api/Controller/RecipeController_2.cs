using DietTracker_DataAccess;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DietTracker_Api.Controller.FoodController;

namespace DietTracker_Api.Controller
{
    public partial class RecipeController : ControllerBase
    {
        [HttpGet]
        [Route(nameof(GetRandom))]
        public async Task<ActionResult<String>> GetRandom()
        {
            var rand = new Random();
            var cnt = await recipeCollection.CountDocumentsAsync(new BsonDocument());

            var res = await recipeCollection
                .Find(new BsonDocument())
                .Skip(rand.Next(0, Convert.ToInt32(cnt)))
                .Limit(1)
                .FirstOrDefaultAsync();

            if (res == null) return NotFound();

            return Ok(res.Id.ToString());
        }

        [HttpPost]
        [Route(nameof(AddFoodToRecipe))]
        public async Task<ActionResult<Boolean>> AddFoodToRecipe(String recipeId, String foodId)
        {
            var recipe = await recipeCollection.GetById(recipeId);
            if (recipe == null) return NotFound(false);

            await recipeCollection.DeleteById(recipe.Id);

            var listIds = recipe.FoodIds;
            listIds.Add(ObjectId.Parse(foodId));

            var na = new Recipe(recipe.Id, recipe.Name, recipe.PrepareTime, recipe.Difficulty, recipe.Preparation, listIds, recipe.Category);


            await recipeCollection.InsertOneAsync(na);

            return Ok(true);
        }

        public record CategoryCounter(
                String category,
                int count
            );

        [HttpGet]
        [Route(nameof(GetAllCategories))]
        public async Task<ActionResult<List<CategoryCounter>>> GetAllCategories()
        {
            var list = await recipeCollection.DistinctAsync<String>("Category", new BsonDocument());
            int cnt = 0;
            List<CategoryCounter> result = new List<CategoryCounter>();

            foreach (var i in list.ToList())
            {
                var current = await recipeCollection.GetAllRecipesByCategory(i);
                cnt = current.Count();
                result.Add(new CategoryCounter(i, cnt));
            }

            return result;
        }

        [HttpGet]
        [Route(nameof(GetAllRecipesByCategory))]
        public async Task<ActionResult<List<RecipeDto>>> GetAllRecipesByCategory(string category)
        {
            var list = await recipeCollection.GetAllRecipesByCategory(category);
            var res = new List<RecipeDto>();

            foreach(var i in list)
            {
                res.Add(new RecipeDto(i.Id.ToString(), i.Name, i.PrepareTime, i.Difficulty, i.Category, i.Preparation, i.FoodIds.Select(i => i.ToString()).ToList()));
            }

            return res;
        }
    }
}
