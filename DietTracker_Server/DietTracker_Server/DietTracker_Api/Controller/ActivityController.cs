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
    public class ActivityController : ControllerBase
    {
        private readonly IMongoCollection<Activity> activityCollection;

        public ActivityController(CollectionFactory cf)
        {
            activityCollection = cf.GetCollection<Activity>();
        }

       public record ActivityCreationDto(
           int Steps, 
           double ActiveTime, 
           double GoalTime, 
           double BurnedCalories, 
           double Distance
           );

       public record ActivityDto(
           string Id,
           int Steps, 
           double ActiveTime, 
           double GoalTime, 
           double BurnedCalories, 
           double Distance
           );

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActivityDto>>> GetAll()
        {
            var dbResult = await activityCollection.GetAll();
            var result = dbResult.Select(a => new ActivityDto(a.Id.ToString(), a.Steps, a.ActiveTime, a.GoalTime, a.BurnedCalories, a.Distance));
            
            return Ok(result);
        }

        [HttpGet("{id}", Name = nameof (GetSingleActivity))]
        public async Task<ActionResult<ActivityDto>> GetSingleActivity(string id)
        {
            var result = await activityCollection.GetById(id);
            
            if(result == null)
            {
                return NotFound();
            }

            return new ActivityDto(result.Id.ToString(), result.Steps, result.ActiveTime, result.GoalTime, result.BurnedCalories, result.Distance);
        }

        [HttpPost]
        public async Task<ActionResult<ActivityDto>> Add(ActivityCreationDto activityCreationDto)
        {
            var na = new Activity(ObjectId.Empty, activityCreationDto.Steps, activityCreationDto.ActiveTime, activityCreationDto.GoalTime, activityCreationDto.BurnedCalories, activityCreationDto.Distance);
            await activityCollection.InsertOneAsync(na);
            return CreatedAtRoute(nameof(GetSingleActivity), new { Id = na.Id },
                new ActivityDto(na.Id.ToString(), na.Steps, na.ActiveTime, na.GoalTime, na.BurnedCalories, na.Distance));
        }
    }
}
