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
    public partial class FoodController : ControllerBase
    {
        private readonly IMongoCollection<Food> foodCollection;

        public FoodController(CollectionFactory cf)
        {
            foodCollection = cf.GetCollection<Food>();
        }

        public record FoodCreationDto(
            string Name,
            string Unit,
            double Value,
            double Calories, 
            double Protein, 
            double TotalCarbohydrates, 
            double Sugar, 
            double Fiber, 
            double Fat
            );

        public record FoodDto(
            string Id,
            string Name,
            string Unit,
            double Value,
            double Calories, 
            double Protein, 
            double TotalCarbohydrates, 
            double Sugar, 
            double Fiber, 
            double Fat
            );

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Food>>> GetAll()
        {
            var dbResult = await foodCollection.GetAll();
            var result = dbResult.Select(a => new FoodDto(a.Id.ToString(), a.Name, a.Unit, a.Value, a.Calories, a.Protein, a.TotalCarbohydrates,a.Sugar,a.Fiber,a.Fat));
            return Ok(result);
        }

        [HttpGet("{id}", Name = nameof(GetSingleFood))]
        public async Task<ActionResult<FoodDto>> GetSingleFood(string id)
        {
            var result = await foodCollection.GetById(id);
            if (result == null) return NotFound();
            return new FoodDto(result.Id.ToString(), result.Name, result.Unit, result.Value, result.Calories, result.Protein, result.TotalCarbohydrates, result.Sugar, result.Fiber, result.Fat);
        }

        [HttpPost]
        public async Task<ActionResult<FoodDto>> Add(FoodCreationDto foodDto)
        {
            var na = new Food(ObjectId.Empty, foodDto.Name, foodDto.Unit, foodDto.Value, foodDto.Calories, foodDto.Protein, foodDto.TotalCarbohydrates, foodDto.Sugar, foodDto.Fiber, foodDto.Fat);
            await foodCollection.InsertOneAsync(na);
            return CreatedAtRoute(nameof(GetSingleFood), new { na.Id },
                new FoodDto(na.Id.ToString(), na.Name, na.Unit, na.Value, na.Calories, na.Protein, na.TotalCarbohydrates, na.Sugar, na.Fiber, na.Fat));
        }

        [HttpPost]
        [Route(nameof(Replace))]
        public async Task<ActionResult<FoodDto>> Replace(FoodCreationDto foodDto,string id)
        {
            var na = new Food(ObjectId.Parse(id), foodDto.Name, foodDto.Unit, foodDto.Value, foodDto.Calories, foodDto.Protein, foodDto.TotalCarbohydrates, foodDto.Sugar, foodDto.Fiber, foodDto.Fat);
            await foodCollection.ReplaceById(id,na);
            return Ok(200);
        }
    }
}
