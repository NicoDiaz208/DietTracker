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
            double Current,
            double Now
            );

        public record CalorieIntakeDto(
            string Id,
            double Current,
            double Now
            );

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CalorieIntake>>> GetAll()
        {
            var dbResult = await calorieIntakeCollection.GetAll();
            var result = dbResult.Select(a => new CalorieIntakeDto(a.Id.ToString(), a.Current, a.Now));

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

            return new CalorieIntakeDto(result.Id.ToString(), result.Current, result.Now);
        }

        [HttpPost]
        public async Task<ActionResult<CalorieIntakeDto>> Add(CalorieIntakeCreationDto calorieIntakeCreationDto)
        {
            var na = new CalorieIntake(ObjectId.Empty, calorieIntakeCreationDto.Current, calorieIntakeCreationDto.Now);
            await calorieIntakeCollection.InsertOneAsync(na);
            return CreatedAtRoute(nameof(GetSingleCalorieIntake), new { Id = na.Id },
                new CalorieIntakeDto(na.Id.ToString(), na.Current, na.Now));
        }

    }
}
