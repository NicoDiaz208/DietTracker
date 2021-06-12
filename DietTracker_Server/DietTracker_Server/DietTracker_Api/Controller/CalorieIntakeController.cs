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
    public class CalorieIntakeController : ControllerBase
    {
        private readonly IMongoCollection<CalorieIntake> calorieIntakeCollection;

        public CalorieIntakeController(CollectionFactory cf)
        {
            calorieIntakeCollection = cf.GetCollection<CalorieIntake>();
        }

        public record CalorieIntakeCreationDto(
            double Goal,
            double Current,
            DateTime Date
            );

        public record CalorieIntakeDto(
            string Id,
            double Goal,
            double Current,
            DateTime Date
            );

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CalorieIntake>>> GetAll()
        {
            var dbResult = await calorieIntakeCollection.GetAll();
            var result = dbResult.Select(a => new CalorieIntakeDto(a.Id.ToString(),a.Goal, a.Current, a.Date));

            return Ok(result);
        }

        [HttpGet("{id}", Name = nameof(GetSingleCalorieIntake))]
        public async Task<ActionResult<CalorieIntakeDto>> GetSingleCalorieIntake(string id)
        {
            var result = await calorieIntakeCollection.GetById(id);

            if (result == null)
            {
                return NotFound();
            }

            return new CalorieIntakeDto(result.Id.ToString(), result.Goal, result.Current, result.Date);
        }

        [HttpPost]
        public async Task<ActionResult<CalorieIntakeDto>> Add(CalorieIntakeCreationDto calorieIntakeCreationDto)
        {
            var na = new CalorieIntake(ObjectId.Empty, calorieIntakeCreationDto.Goal, calorieIntakeCreationDto.Current, calorieIntakeCreationDto.Date);
            await calorieIntakeCollection.InsertOneAsync(na);
            return CreatedAtRoute(nameof(GetSingleCalorieIntake), new { na.Id },
                new CalorieIntakeDto(na.Id.ToString(), na.Goal, na.Current, na.Date));
        }

        [HttpPost]
        [Route(nameof(Replace))]
        public async Task<ActionResult<CalorieIntakeDto>> Replace(CalorieIntakeCreationDto calorieIntakeCreationDto, string id)
        {
            var na = new CalorieIntake(ObjectId.Parse(id), calorieIntakeCreationDto.Goal, calorieIntakeCreationDto.Current, calorieIntakeCreationDto.Date); 
            await calorieIntakeCollection.ReplaceById(id, na);
            return Ok(200);
        }

    }
}
