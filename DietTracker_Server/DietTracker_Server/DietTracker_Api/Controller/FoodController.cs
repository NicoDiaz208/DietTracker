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
    [Route("api/[Controller]")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private readonly IMongoCollection<Food> foodCollection;

        public FoodController(CollectionFactory cf)
        {
            foodCollection = cf.GetCollection<Food>();
        }

        public record FoodCreationDto(
            string Name
            );

        public record FoodDto(
            string Id,
            string Name
            );

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Food>>> GetAll()
        {
            var dbResult = await foodCollection.GetAll();
            var result = dbResult.Select(a => new FoodDto(a.Id.ToString(), a.Name));

            return Ok(result);
        }

        [HttpGet("{id}", Name = nameof(GetSingleFood))]
        public async Task<ActionResult<FoodDto>> GetSingleFood(string id)
        {
            var result = await foodCollection.GetById(id);

            if (result == null)
            {
                return NotFound();
            }

            return new FoodDto(result.Id.ToString(), result.Name);
        }

        [HttpPost]
        public async Task<ActionResult<FoodDto>> Add(FoodCreationDto foodDto)
        {
            var na = new Food(ObjectId.Empty, ObjectId.Empty, foodDto.Name);
            await foodCollection.InsertOneAsync(na);
            return CreatedAtRoute(nameof(GetSingleFood), new { Id = na.Id },
                new FoodDto(na.Id.ToString(), na.Name));
        }

        [HttpPost]
        [Route(nameof(AddNutritionFactToFood))]
        public async Task<ActionResult<Boolean>> AddNutritionFactToFood(String foodId, String nutritionFactId)
        {
            var food = await foodCollection.GetById(foodId);
            if (food == null) return NotFound(false);

            await foodCollection.DeleteById(food.Id);

            var na = new Food(food.Id, ObjectId.Parse(nutritionFactId), food.Name);

            await foodCollection.InsertOneAsync(na);

            return Ok(true);
        }

    }
}
