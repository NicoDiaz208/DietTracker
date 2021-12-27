using DietTracker_DataAccess;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DietTracker_Api.Controller.CategoryController;

namespace DietTracker_Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class RecipeController : ControllerBase
    {
        private readonly IMongoCollection<Recipe> recipeCollection;
        private readonly IMongoCollection<Category> categoryCollection;

        private readonly GridFSBucket bucket;

        public RecipeController(CollectionFactory cf)
        {
            recipeCollection = cf.GetCollection<Recipe>();
            categoryCollection = cf.GetCollection<Category>();
            bucket = new GridFSBucket(cf.GetDatabase(), new GridFSBucketOptions
            {
                BucketName = "RecipeImages",
                ChunkSizeBytes = 1048576, // 1MB
                WriteConcern = WriteConcern.WMajority,
                ReadPreference = ReadPreference.Primary
            });
        }

        public record RecipeCreationDto(
            string Name, string PrepareTime, double Difficulty, List<CategoryDto> Category, string Preparation, List<IngredientDto> FoodIds);

        public record IngredientDto(
            string Id,
            double Value
            );
        public record RecipeDto(
            string Id,
            string Name, string PrepareTime, double Difficulty, List<CategoryDto> Category, string Preparation,
            List<IngredientDto> FoodIds);

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecipeDto>>> GetAll()
        {
            var dbResult = await recipeCollection.GetAll();
            var result = dbResult.Select(a => new RecipeDto(a.Id.ToString(), a.Name, a.PrepareTime, a.Difficulty,
            a.Categories.Select(a => new CategoryDto(a.Id.ToString(), a.category)).ToList()
            , a.Preparation,
            a.FoodIds.Select(i => new IngredientDto(i.Id.ToString(), i.Value)).ToList()));
            return Ok(result);
        }

        [HttpGet("{id}", Name = nameof(GetSingleRecipe))]
        public async Task<ActionResult<RecipeDto>> GetSingleRecipe(string id)
        {
            var item = await recipeCollection.GetById(id);
            if (item == null)
            {
                return NotFound();
            }

            return new RecipeDto(
                item.Id.ToString(), 
                item.Name, 
                item.PrepareTime, 
                item.Difficulty,
                item.Categories.Select(a => new CategoryDto(
                    a.Id.ToString(),
                    a.category)).ToList(),
                item.Preparation, 
                item.FoodIds.Select(i => new IngredientDto(
                    i.Id.ToString(), 
                    i.Value)).ToList());
        }

        [HttpPost]
        public async Task<ActionResult<RecipeDto>> Add(RecipeCreationDto item)
        {
            if (item.Category == null) return BadRequest();
            if(item.FoodIds == null) return BadRequest();

            var na = new Recipe(ObjectId.Empty,ObjectId.Empty, item.Name, item.PrepareTime, item.Difficulty, item.Preparation, new List<Ingredient>(), new List<Category>() );
            await recipeCollection.InsertOneAsync(na);
            return CreatedAtRoute(nameof(GetSingleRecipe), new { id = na.Id },
                new RecipeDto(
                    na.Id.ToString(), 
                    na.Name, 
                    na.PrepareTime, 
                    na.Difficulty,
                    na.Categories.Select(i =>
                    new CategoryDto(
                        i.Id.ToString(),
                        i.category)).ToList(),
                    na.Preparation, 
                    na.FoodIds.Select(i => 
                    new IngredientDto(
                        i.Id.ToString(), 
                        i.Value)).ToList()));
        }

        [HttpPost]
        [Route(nameof(Replace))]
        public async Task<ActionResult<RecipeDto>> Replace(RecipeDto item, string id)
        {
            var oldRecipe = await recipeCollection.GetById(ObjectId.Parse(id));
            if (oldRecipe != null)
            {
                var na = new Recipe(
                    ObjectId.Empty,
                    ObjectId.Parse(id), 
                    item.Name, 
                    item.PrepareTime, 
                    item.Difficulty, 
                    item.Preparation, 
                    oldRecipe.FoodIds, 
                    oldRecipe.Categories);
                await recipeCollection.ReplaceById(id, na);
            }
            return Ok(200);
        }

        [HttpPost]
        [Route(nameof(InitRecipe))]
        public async Task<ActionResult<RecipeDto>> InitRecipe()
        {
            var na = new Recipe(ObjectId.Empty,ObjectId.Empty, "Pizza Magherita", "3 hours", 2, "Hier sollte ein großer text stehen", new List<Ingredient>(), new List<Category>());
            await recipeCollection.InsertOneAsync(na);
            na = new Recipe(ObjectId.Empty,ObjectId.Empty, "Apfel Strudel", "1 hour", 3, "Hier sollte ein großer text stehen", new List<Ingredient>(), new List<Category>());
            await recipeCollection.InsertOneAsync(na);
            na = new Recipe(ObjectId.Empty,ObjectId.Empty, "Nicos Salat", "5 hours", 7, "Hier sollte ein großer text stehen", new List<Ingredient>(), new List<Category>());
            await recipeCollection.InsertOneAsync(na);
            return Ok(200);
        }

    }
}
