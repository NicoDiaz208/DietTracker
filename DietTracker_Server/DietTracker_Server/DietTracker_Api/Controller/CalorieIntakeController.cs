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
            double Expected,
            double Current
            );

        public record CalorieIntakeDto(
            string Id,
            double Expected,
            double Current
            );

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CalorieIntake>>> GetAll()
        {
            var dbResult = await calorieIntakeCollection.GetAll();
            var result = dbResult.Select(a => new CalorieIntakeDto(a.Id.ToString(),a.Expected, a.Current));

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

            return new CalorieIntakeDto(result.Id.ToString(), result.Expected, result.Current);
        }

        [HttpPost]
        public async Task<ActionResult<CalorieIntakeDto>> Add(CalorieIntakeCreationDto calorieIntakeCreationDto)
        {
            var na = new CalorieIntake(ObjectId.Empty, calorieIntakeCreationDto.Expected, calorieIntakeCreationDto.Current, ObjectId.Empty);
            await calorieIntakeCollection.InsertOneAsync(na);
            return CreatedAtRoute(nameof(GetSingleCalorieIntake), new { na.Id },
                new CalorieIntakeDto(na.Id.ToString(), na.Expected, na.Current));
        }

        [HttpPost]
        [Route(nameof(AddActivityToCaloryIntake))]
        public async Task<ActionResult<Boolean>> AddActivityToCaloryIntake(String caloryIntakeId, String activityId)
        {
            var caloryIntake = await calorieIntakeCollection.GetById(caloryIntakeId);
            if (caloryIntake == null) return NotFound(false);

            await calorieIntakeCollection.DeleteById(caloryIntake.Id);

            var na = new CalorieIntake(caloryIntake.Id, caloryIntake.Expected, caloryIntake.Current, ObjectId.Parse(activityId));

            await calorieIntakeCollection.InsertOneAsync(na);

            return Ok(true);
        }

    }
}
