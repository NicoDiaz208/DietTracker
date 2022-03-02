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
        private readonly IMongoCollection<Food> foodCollection;

        private readonly GridFSBucket bucket;

        public RecipeController(CollectionFactory cf)
        {
            recipeCollection = cf.GetCollection<Recipe>();
            foodCollection = cf.GetCollection<Food>();
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
            string Name, string PrepareTime, double Difficulty, List<CategoryDto> Category, string Preparation, List<Ingredient> FoodIds);

        public record RecipeDto(
            string Id,
            string Name, string PrepareTime, double Difficulty, List<CategoryDto> Category, string Preparation,
            List<Ingredient> FoodIds);

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecipeDto>>> GetAll()
        {
            var dbResult = await recipeCollection.GetAll();
            var result = dbResult.Select(a => new RecipeDto(a.Id.ToString(), a.Name, a.PrepareTime, a.Difficulty,
            a.Categories.Select(a => new CategoryDto(a.Id.ToString(), a.category)).ToList()
            , a.Preparation,
            a.FoodIds.Select(i => new Ingredient(i.Value, i.FoodId)).ToList()));
            return Ok(result);
        }

        [HttpGet("{id}", Name = nameof(GetSingleRecipe))]
        public async Task<ActionResult<RecipeDto>> GetSingleRecipe(string id)
        {
            var item = await recipeCollection.GetById(id);
            if (item == null) return NotFound();
            return new RecipeDto(item.Id.ToString(), item.Name, item.PrepareTime, item.Difficulty,item.Categories.Select(a => new CategoryDto(a.Id.ToString(),a.category)).ToList(),item.Preparation, item.FoodIds);
        }

        [HttpPost]
        public async Task<ActionResult<RecipeDto>> Add(RecipeCreationDto item)
        {
            if (item.Category == null) return BadRequest();
            if(item.FoodIds == null) return BadRequest();

            //Validate CategoryIds
            var categories = new List<Category>();
            foreach (var category in item.Category)
            {
                var cat = await categoryCollection.GetById(category.Id);

                if(cat == null) continue;
                categories.Add(cat);
            }

            //validate FoodIds
            var foods = new List<Ingredient>();
            foreach (var food in item.FoodIds)
            {
                var fd = await foodCollection.GetById(food.FoodId);
                if(fd == null) continue;
                foods.Add(food);
            }

            var na = new Recipe(
                ObjectId.Empty,
                ObjectId.Empty, 
                item.Name, 
                item.PrepareTime, 
                item.Difficulty, 
                item.Preparation, 
                item.FoodIds,
                categories);

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
                    foods));
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


        [HttpGet]
        [Route(nameof(GetRandomWithCount))]
        public async Task<ActionResult<IEnumerable<RecipeDto>>> GetRandomWithCount(int count)
        {
            var cnt = await recipeCollection.CountDocumentsAsync(new BsonDocument());
            if(count>cnt)count=Convert.ToInt32(cnt);

            List<RecipeDto> listOfRecipe = new List<RecipeDto>();

            for(int i = 0; i < count; i++)
            {
                var rand = new Random();



                var rec = await recipeCollection
                    .Find(new BsonDocument())
                    .Skip(rand.Next(0, Convert.ToInt32(cnt)))
                    .Limit(1)
                    .FirstOrDefaultAsync();

                if (rec == null) count--;
                else
                {
                    listOfRecipe.Add(
                        new RecipeDto(rec.Id.ToString(), 
                        rec.Name, rec.PrepareTime, 
                        rec.Difficulty, 
                        rec.Categories
                            .Select(e => new CategoryDto(
                                e.Id.ToString(),
                                e.category
                            )).ToList(), 
                        rec.Preparation,
                        rec.FoodIds)); 
                }

            }
            
            return Ok(listOfRecipe);
        }
    }
}
