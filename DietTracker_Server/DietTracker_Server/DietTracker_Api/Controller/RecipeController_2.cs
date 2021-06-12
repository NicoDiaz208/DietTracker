using DietTracker_DataAccess;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DietTracker_Api.Controller
{
    public partial class RecipeController :  ControllerBase
    {
        [HttpGet]
        [Route(nameof(GetRandom))]
        public async Task<ActionResult<RecipeDto>> GetRandom()
        {
            var rand = new Random();
            var cnt = await recipeCollection.CountDocumentsAsync(new BsonDocument());

            var res = await recipeCollection
                .Find(new BsonDocument())
                .Skip(rand.Next(0, Convert.ToInt32(cnt)))
                .Limit(1)
                .FirstOrDefaultAsync();

            if (res == null) return NotFound();

            return Ok(res);
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

            var na = new Recipe(recipe.Id, recipe.Name, recipe.PrepareTime, recipe.Difficulty, listIds, recipe.Category);


            await recipeCollection.InsertOneAsync(na);

            return Ok(true);
        }

        [HttpGet]
        [Route(nameof(GetAllCategories))]
        public async Task<ActionResult<List<String>>> GetAllCategories()
        {
            var list = await recipeCollection.DistinctAsync<String>("Category", new BsonDocument());
            return list.ToList();
        }
    }
}
