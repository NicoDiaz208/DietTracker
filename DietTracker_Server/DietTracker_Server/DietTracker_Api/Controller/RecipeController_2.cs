using DietTracker_DataAccess;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using static DietTracker_Api.Controller.CategoryController;
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
        public async Task<ActionResult<Boolean>> AddFoodToRecipe(String recipeId, String foodId, double value, string unit)
        {
            var recipe = await recipeCollection.GetById(recipeId);
            if (recipe == null) return NotFound(false);

            await recipeCollection.DeleteById(recipe.Id);

            var listIds = recipe.FoodIds;
            listIds.Add(new Ingredient(ObjectId.Parse(foodId), value, unit));

            var na = new Recipe(recipe.Picture, recipe.Id, recipe.Name, recipe.PrepareTime, recipe.Difficulty, recipe.Preparation, listIds, recipe.Categories);


            await recipeCollection.InsertOneAsync(na);

            return Ok(true);
        }

        [HttpPost]
        [Route(nameof(AddCategoryToRecipe))]
        public async Task<ActionResult<Boolean>> AddCategoryToRecipe(String recipeId, Category category)
        {
            var recipe = await recipeCollection.GetById(recipeId);
            if(recipe == null) return NotFound(false);

            if(categoryCollection.GetById(category.Id) == null)
                return NotFound(false);

            if (recipe.Categories.Where(c => c == category).Count() > 0) return BadRequest();

            recipe.Categories.Add(category);

            await recipeCollection.DeleteById(ObjectId.Parse(recipeId));
            await recipeCollection.InsertOneAsync(recipe);
            return Ok(true);
        }

        public record CategoryCounter(
                Category category,
                int count
            );

        [HttpGet]
        [Route(nameof(GetAllRecipesByCategory))]
        public async Task<ActionResult<List<RecipeDto>>> GetAllRecipesByCategory(string category)
        {
            
            var list = await recipeCollection.GetAll();
            var res = new List<RecipeDto>();

            foreach (var i in list)
            {
                foreach( var d in i.Categories)
                {
                    if (d.category == category)
                    {
                        res.Add(new RecipeDto(
                    i.Id.ToString(),
                    i.Name,
                    i.PrepareTime,
                    i.Difficulty,
                    i.Categories.Select(i => new CategoryDto(i.Id.ToString(), i.category)).ToList(),
                    i.Preparation,
                    i.FoodIds.Select(i => new IngredientDto(i.Id.ToString(), i.Value)).ToList()));
                    }
                }
            }

            return res;
        }

        [HttpPost, DisableRequestSizeLimit]
        [Route(nameof(UploadImage))]
        public async Task<IActionResult> UploadImage(string recipeId)
        {
            var recipe = await recipeCollection.GetById(recipeId);
            if (recipe == null) return NotFound(false);



            var formCollection = await Request.ReadFormAsync();
            var file = formCollection.Files[0];
            if (file.Length > 0)
            {
                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition)?.FileName?.Trim('"');
                using (var stream = await bucket.OpenUploadStreamAsync(fileName))
                {
                    var na = new Recipe(
                    stream.Id,
                    ObjectId.Parse(recipeId),
                    recipe.Name,
                    recipe.PrepareTime,
                    recipe.Difficulty,
                    recipe.Preparation,
                    recipe.FoodIds,
                    recipe.Categories);

                    await recipeCollection.ReplaceById(recipeId, na);

                    await file.CopyToAsync(stream);
                    await stream.CloseAsync();
                }

                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("images")]
        public async Task<ActionResult<FileInfo[]>> GetImages()
        {
            using var cursor = await bucket.FindAsync(Builders<GridFSFileInfo>.Filter.Empty);
            var files = await cursor.ToListAsync();
            return files.Select(f => new FileInfo(f.Id.ToString(), f.Filename)).ToArray();
        }

        [HttpGet("images/{id}")]
        public async Task<IActionResult> GetImage(string id)
        {
            var oid = new ObjectId(id);
            return File(await bucket.OpenDownloadStreamAsync(oid), "image/jpeg");
        }
    }
}
