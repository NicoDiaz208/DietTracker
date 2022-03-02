using DietTracker_DataAccess;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DietTracker_Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AchievementsController : ControllerBase
    {
        private readonly IMongoCollection<Achievement> achievementCollection;

        public AchievementsController(CollectionFactory cf)
        {
            achievementCollection = cf.GetCollection<Achievement>();
        }

        public record AchievementCreationDto(
            string Name,
            double Now,
            string Description,
            double Goal);

        public record AchievementDto(
            string Id,
            string Name,
            double Now,
            string Description,
            double Goal);

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AchievementDto>>> GetAll()
        {
            var dbResult = await achievementCollection.GetAll();
            var result = dbResult.Select(a => new AchievementDto(a.Id.ToString(), a.Name, a.Now,a.Description, a.Goal));
            return Ok(result);
        }

        [HttpGet("{id}", Name = nameof(GetSingleAchievement))]
        public async Task<ActionResult<AchievementDto>> GetSingleAchievement(string id)
        {
            var item = await achievementCollection.GetById(id);
            if (item == null) return NotFound();
            return new AchievementDto(item.Id.ToString(), item.Name, item.Now,item.Description, item.Goal);
        }

        [HttpPost]
        public async Task<ActionResult<AchievementDto>> Add(AchievementCreationDto item)
        {
            var na = new Achievement(ObjectId.Empty, item.Name, item.Now,item.Description, item.Goal);
            await achievementCollection.InsertOneAsync(na);
            return CreatedAtRoute(nameof(GetSingleAchievement), new { id = na.Id },
                new AchievementDto(na.Id.ToString(), na.Name, na.Now,na.Description, na.Goal));
        }

        [HttpPost]
        [Route(nameof(Replace))]
        public async Task<ActionResult<AchievementDto>> Replace(AchievementCreationDto item, string id)
        {
            var na = new Achievement(ObjectId.Parse(id), item.Name, item.Now,item.Description, item.Goal);
            await achievementCollection.ReplaceById(id, na);
            return Ok(200);
        }
    }
}