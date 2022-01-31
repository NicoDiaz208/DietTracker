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
    public class DailyProgressController: ControllerBase
    {
        private readonly IMongoCollection<DailyProgress> dailyProgressCollection;

        public DailyProgressController(CollectionFactory cf)
        {
            dailyProgressCollection = cf.GetCollection<DailyProgress>();
        }

        public record DailyProgressCreationDto(
            double Now,
            double Protein,
            double Calories,
            DateTime Date);

        public record DailyProgressDto(
            string Id,
            double Protein,
            double Calories,
            double Now,
            DateTime Date);

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DailyProgressDto>>> GetAll()
        {
            var dbResult = await dailyProgressCollection.GetAll();
            var result = dbResult.Select(a => new DailyProgressDto(a.Id.ToString(),a.Protein,a.Calories, a.Percentage,a.Date));
            return Ok(result);
        }

        [HttpGet("{id}", Name = nameof(GetSingleDailyProgress))]
        public async Task<ActionResult<DailyProgressDto>> GetSingleDailyProgress(string id)
        {
            var item = await dailyProgressCollection.GetById(id);
            if (item == null)
            {
                return NotFound();
            }

            return new DailyProgressDto(item.Id.ToString(),item.Protein,item.Calories, item.Percentage,item.Date);
        }

        [HttpPost]
        public async Task<ActionResult<DailyProgressDto>> Add(DailyProgressCreationDto item)
        {
            var na = new DailyProgress(ObjectId.Empty, item.Now,item.Protein,item.Calories,item.Date);
            await dailyProgressCollection.InsertOneAsync(na);
            return CreatedAtRoute(nameof(GetSingleDailyProgress), new { id = na.Id },
                new DailyProgressDto(na.Id.ToString(),na.Protein,na.Calories, na.Percentage,na.Date));
        }

        [HttpPost]
        [Route(nameof(Replace))]
        public async Task<ActionResult<DailyProgressDto>> Replace(DailyProgressDto item, string id)
        {
            var na = new DailyProgress(ObjectId.Parse(id), item.Now,item.Protein,item.Calories, item.Date);
            await dailyProgressCollection.ReplaceById(id, na);
            return Ok(200);
        }

        [HttpPost]
        [Route(nameof(InitDailyProgress))]
        public async Task<ActionResult<DailyProgressDto>> InitDailyProgress()
        {
            var na = new DailyProgress(ObjectId.Empty, 5, 200, 500, DateTime.Now);
            await dailyProgressCollection.InsertOneAsync(na);
            na = new DailyProgress(ObjectId.Empty, 10, 201, 511, DateTime.Now);
            await dailyProgressCollection.InsertOneAsync(na);
            na = new DailyProgress(ObjectId.Empty, 3, 233, 530, DateTime.Now);
            await dailyProgressCollection.InsertOneAsync(na);

            return Ok(200);

        }
    }
}
