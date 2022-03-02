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
    public class CategoryController : ControllerBase
    {

        private readonly IMongoCollection<Category> categoryController;

        public CategoryController(CollectionFactory cc)
        {
            categoryController = cc.GetCollection<Category>();
        }
        public record CategoryDto(
            string Id,
            string name);

        public record CategoryCreationDto(
            string name);

        [HttpGet]
        [Route(nameof(GetAll))]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAll()
        {
            var dbResult = await categoryController.GetAll();
            var result = dbResult.Select(a => new CategoryDto(a.Id.ToString(), a.category));
            return Ok(result);
        }

        [HttpGet("{id}", Name = nameof(GetSingleCategory))]
        public async Task<ActionResult<CategoryDto>> GetSingleCategory(string id)
        {
            var item = await categoryController.GetById(id);
            if (item == null) return NotFound();
            return new CategoryDto(item.Id.ToString(), item.category);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDto>> Add(CategoryCreationDto item)
        {
            var na = new Category(ObjectId.Empty, item.name);
            await categoryController.InsertOneAsync(na);
            return CreatedAtRoute(nameof(GetSingleCategory), new { id = na.Id },
                new CategoryDto(na.Id.ToString(), na.category));
        }

        [HttpPost]
        [Route(nameof(Replace))]
        public async Task<ActionResult<CategoryDto>> Replace(CategoryDto item, string id)
        {
            var na = new Category(ObjectId.Parse(id), item.name);
            await categoryController.ReplaceById(id, na);
            return Ok(200);
        }

    }
}
