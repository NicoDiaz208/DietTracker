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
    public class UserController : ControllerBase
    {
        private readonly IMongoCollection<User> userCollection;

        public UserController(CollectionFactory cf)
        {
            userCollection = cf.GetCollection<User>();
        }

        public record UserCreationDto(
            string Name,
            DateTime DateofBirth,
            string Gender,
            double GoalWeight,
            int Height,
            string Email,
            string Phonenumber,
            int ActivityLevel);

        public record UserDto(
            string Id,
            string Name,
            DateTime DateofBirth,
            string Gender,
            double GoalWeight,
            int Height,
            string Email,
            string Phonenumber,
            int ActivityLevel);

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
        {
            var dbResult = await userCollection.GetAll();
            var result = dbResult.Select(a => new UserDto(a.Id.ToString(), a.Name, a.DateofBirth, a.Gender, a.GoalWeight, a.Height,a.Email, a.Phonenumber, a.ActivityLevel));
            return Ok(result);
        }

        [HttpGet("{id}", Name = nameof(GetSingleUser))]
        public async Task<ActionResult<UserDto>> GetSingleUser(string id)
        {
            var item = await userCollection.GetById(id);
            if (item == null)
            {
                return NotFound();
            }

            return new UserDto(item.Id.ToString(), item.Name, item.DateofBirth, item.Gender, item.GoalWeight, item.Height, item.Email, item.Phonenumber, item.ActivityLevel);
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> Add(UserCreationDto item)
        {
            var na = new User(ObjectId.Empty, item.Name, item.DateofBirth, item.Gender, item.GoalWeight, item.Height, item.Email, item.Phonenumber, item.ActivityLevel);
            await userCollection.InsertOneAsync(na);
            return CreatedAtRoute(nameof(GetSingleUser), new { id = na.Id },
                new UserDto(na.Id.ToString(), na.Name, na.DateofBirth, na.Gender, na.GoalWeight, na.Height, na.Email, na.Phonenumber, na.ActivityLevel));
        }
    }
}
