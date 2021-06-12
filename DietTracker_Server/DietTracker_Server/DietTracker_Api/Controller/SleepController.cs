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
    [Route("api/[controller]")]
    [ApiController]
    public partial class SleepController : ControllerBase
    {
        private readonly IMongoCollection<Sleep> sleepCollection;

        public SleepController(CollectionFactory cf)
        {
            sleepCollection = cf.GetCollection<Sleep>();
        }

        public record SleepCreationDto(
            int HoSG, int HoSC);

        public record SleepDto(
            string Id,
            int HoSG, int HoSC);

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SleepDto>>> GetAll()
        {
            var dbResult = await sleepCollection.GetAll();
            var result = dbResult.Select(a => new SleepDto (a.Id.ToString(),a.HoSG, a.HoSC));
            return Ok(result);
        }

        [HttpGet("{id}", Name = nameof(GetSingleSleep))]
        public async Task<ActionResult<SleepDto>> GetSingleSleep(string id)
        {
            var item = await sleepCollection.GetById(id);
            if (item == null)
            {
                return NotFound();
            }

            return new SleepDto(item.Id.ToString(), item.HoSG, item.HoSC);
        }

        [HttpPost]
        public async Task<ActionResult<SleepDto>> Add(SleepCreationDto item)
        {
            var na = new Sleep(ObjectId.Empty, item.HoSG, item.HoSC, ObjectId.Empty);
            await sleepCollection.InsertOneAsync(na);
            return CreatedAtRoute(nameof(GetSingleSleep), new { id = na.Id },
                new SleepDto(na.Id.ToString(), na.HoSG, na.HoSC));
        }

        [HttpPost]
        [Route(nameof(Replace))]
        public async Task<ActionResult<SleepDto>> Replace(SleepDto item, string id)
        {
            var na = new Sleep(ObjectId.Parse(id), item.HoSG, item.HoSC, ObjectId.Empty);
            await sleepCollection.ReplaceById(id, na);
            return Ok(200);
        }


    }
}
