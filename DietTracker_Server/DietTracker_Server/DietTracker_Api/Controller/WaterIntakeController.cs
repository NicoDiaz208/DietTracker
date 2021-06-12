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
    public partial class WaterIntakeController : ControllerBase
    {
        private readonly IMongoCollection<WaterIntake> waterIntakeCollection;

        public WaterIntakeController(CollectionFactory cf)
        {
            waterIntakeCollection = cf.GetCollection<WaterIntake>();
        }

        public record WaterIntakeCreationDto(
            int GoWG,
            int GoWC);

        public record WaterIntakeDto(
            string Id,
            int GoWG,
            int GoWC);

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WaterIntakeDto>>> GetAll()
        {
            var dbResult = await waterIntakeCollection.GetAll();
            var result = dbResult.Select(a => new WaterIntakeDto(a.Id.ToString(), a.GoWG, a.GoWC));
            return Ok(result);
        }

        [HttpGet("{id}", Name = nameof(GetSingleWaterIntake))]
        public async Task<ActionResult<WaterIntakeDto>> GetSingleWaterIntake(string id)
        {
            var item = await waterIntakeCollection.GetById(id);
            if (item == null)
            {
                return NotFound();
            }

            return new WaterIntakeDto(item.Id.ToString(), item.GoWG, item.GoWC);
        }

        [HttpPost]
        public async Task<ActionResult<WaterIntakeDto>> Add(WaterIntakeCreationDto item)
        {
            var na = new WaterIntake(ObjectId.Empty, item.GoWG, ObjectId.Empty, item.GoWC);
            await waterIntakeCollection.InsertOneAsync(na);
            return CreatedAtRoute(nameof(GetSingleWaterIntake), new { id = na.Id },
                new WaterIntakeDto(na.Id.ToString(), na.GoWG, na.GoWC));
        }

        

    }
}
