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
            double CalorieGoal,
            double CalorieCurrent,
            double FatGoal,
            double FatCurrent,
            double ProteinGoal,
            double ProteinCurrent,
            double CarbohydratesGoal,
            double CarbohydratesCurrent,
            DateTime Date
            );

        public record CalorieIntakeDto(
            string Id,
            double CalorieGoal,
            double CalorieCurrent,
            double FatGoal,
            double FatCurrent,
            double ProteinGoal,
            double ProteinCurrent,
            double CarbohydratesGoal,
            double CarbohydratesCurrent,
            DateTime Date
            );

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CalorieIntake>>> GetAll()
        {
            var dbResult = await calorieIntakeCollection.GetAll();
            var result = dbResult.Select(a => new CalorieIntakeDto(a.Id.ToString(),a.CalorieGoal, a.CalorieCurrent, a.FatGoal, a.FatCurrent, a.ProteinGoal, a.ProteinCurrent, a.CarbohydratesGoal, a.CarbohydratesCurrent, a.Date));
            return Ok(result);
        }

        [HttpGet("{id}", Name = nameof(GetSingleCalorieIntake))]
        public async Task<ActionResult<CalorieIntakeDto>> GetSingleCalorieIntake(string id)
        {
            var result = await calorieIntakeCollection.GetById(id);
            if (result == null) return NotFound();
            return new CalorieIntakeDto(result.Id.ToString(), result.CalorieGoal, result.CalorieCurrent, result.FatGoal, result.FatCurrent, result.ProteinGoal, result.ProteinCurrent, result.CarbohydratesGoal, result.CarbohydratesCurrent, result.Date);
        }

        [HttpPost]
        public async Task<ActionResult<CalorieIntakeDto>> Add(CalorieIntakeCreationDto calorieIntakeCreationDto)
        {
            var na = new CalorieIntake(ObjectId.Empty, calorieIntakeCreationDto.CalorieGoal, calorieIntakeCreationDto.CalorieCurrent, calorieIntakeCreationDto.FatGoal, calorieIntakeCreationDto.FatCurrent, calorieIntakeCreationDto.ProteinGoal, calorieIntakeCreationDto.ProteinCurrent, calorieIntakeCreationDto.CarbohydratesGoal, calorieIntakeCreationDto.CarbohydratesCurrent, calorieIntakeCreationDto.Date);
            await calorieIntakeCollection.InsertOneAsync(na);
            return CreatedAtRoute(nameof(GetSingleCalorieIntake), new { na.Id },
                new CalorieIntakeDto(na.Id.ToString(), na.CalorieGoal, na.CalorieCurrent, na.FatGoal, na.FatCurrent, na.ProteinGoal, na.ProteinCurrent, na.CarbohydratesGoal, na.CarbohydratesCurrent, na.Date));
        }

        [HttpPost]
        [Route(nameof(Replace))]
        public async Task<ActionResult<CalorieIntakeDto>> Replace(CalorieIntakeDto calorieIntakeCreationDto, string id)
        {
            var na = new CalorieIntake(ObjectId.Parse(id), calorieIntakeCreationDto.CalorieGoal, calorieIntakeCreationDto.CalorieCurrent, calorieIntakeCreationDto.FatGoal, calorieIntakeCreationDto.FatCurrent, calorieIntakeCreationDto.ProteinGoal, calorieIntakeCreationDto.ProteinCurrent, calorieIntakeCreationDto.CarbohydratesGoal, calorieIntakeCreationDto.CarbohydratesCurrent, calorieIntakeCreationDto.Date); 
            await calorieIntakeCollection.ReplaceById(id, na);
            return Ok(200);
        }


        [HttpGet]
        [Route(nameof(GetByDate))]
        public async Task<ActionResult<CalorieIntake>> GetByDate(DateTime date)
        {
            var calorieIntakes = await calorieIntakeCollection.GetAll();
            var result = calorieIntakes.FirstOrDefault(a => a.Date == date);
            return Ok(result);
        }
    }
}
