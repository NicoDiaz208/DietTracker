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
            AcitvityEnum name,
            double distance,		//km
            int minutes,			//duration of activity
            double burnedCalories,
            DateTime date
       );

       public record ActivityDto(
           string Id,
           AcitvityEnum name,
            double distance,		//km
            int minutes,			//duration of activity
            double burnedCalories,
            DateTime date
           );

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActivityDto>>> GetAll()
        {
            var dbResult = await activityCollection.GetAll();
            var result = dbResult.Select(a => new ActivityDto(a.Id.ToString(), a.name, a.distance, a.minutes, a.burnedCalories, a.date));
            
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

            return new ActivityDto(result.Id.ToString(), result.name, result.distance, result.minutes, result.burnedCalories, result.date);
        }

        [HttpPost]
        public async Task<ActionResult<ActivityDto>> Add(ActivityCreationDto activityCreationDto)
        {
            var na = new Activity(ObjectId.Empty, activityCreationDto.name, activityCreationDto.distance, activityCreationDto.minutes, activityCreationDto.burnedCalories,activityCreationDto.date);
            await activityCollection.InsertOneAsync(na);
            return CreatedAtRoute(nameof(GetSingleActivity), new { Id = na.Id },
                new ActivityDto(na.Id.ToString(), na.name, na.distance, na.minutes, na.burnedCalories, na.date));
        }

        [HttpPost]
        [Route(nameof(Replace))]
        public async Task<ActionResult<ActivityDto>> Replace(ActivityCreationDto activityCreationDto, string id)
        {
            var na = new Activity(ObjectId.Parse(id), activityCreationDto.name, activityCreationDto.distance, activityCreationDto.minutes, activityCreationDto.burnedCalories, activityCreationDto.date);
            await activityCollection.ReplaceById(id, na);
            return Ok(200);
        }
    }
}
