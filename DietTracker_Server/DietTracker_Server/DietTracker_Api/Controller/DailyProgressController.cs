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
            string Name,
            double Now,
            DateTime date,
            double Goal);

        public record DailyProgressDto(
            string Id,
            string Name,
            double Now,
            DateTime date,
            double Goal);

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DailyProgressDto>>> GetAll()
        {
            var dbResult = await dailyProgressCollection.GetAll();
            var result = dbResult.Select(a => new DailyProgressDto(a.Id.ToString(), a.Name, a.Now,a.date, a.Goal));
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

            return new DailyProgressDto(item.Id.ToString(), item.Name, item.Now,item.date, item.Goal);
        }

        [HttpPost]
        public async Task<ActionResult<DailyProgressDto>> Add(DailyProgressCreationDto item)
        {
            var na = new DailyProgress(ObjectId.Empty, item.Name, item.Now,item.date, item.Goal);
            await dailyProgressCollection.InsertOneAsync(na);
            return CreatedAtRoute(nameof(GetSingleDailyProgress), new { id = na.Id },
                new DailyProgressDto(na.Id.ToString(), na.Name, na.Now,na.date, na.Goal));
        }
    }
}
