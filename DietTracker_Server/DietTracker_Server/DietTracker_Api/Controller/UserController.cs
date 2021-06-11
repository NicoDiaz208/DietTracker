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

        private readonly IMongoCollection<Recipe> recipeCollection;

        private readonly IMongoCollection<DailyProgress> dailyProgressCollection;

        private readonly IMongoCollection<Activity> activityCollection;

        public UserController(CollectionFactory cf,CollectionFactory rf, CollectionFactory dp,CollectionFactory ac)
        {
            userCollection = cf.GetCollection<User>();
            recipeCollection = rf.GetCollection<Recipe>();
            dailyProgressCollection = dp.GetCollection<DailyProgress>();
            activityCollection = ac.GetCollection<Activity>();
        }

        public record UserCreationDto(
            string Name,
            DateTime DateOfBirth,
            string Gender,
            double GoalWeight,
            int Height,
            string Email,
            string PhoneNumber,
            int ActivityLevel);

        public record UserDto(
            string Id,
            string Name,
            DateTime DateOfBirth,
            string Gender,
            double GoalWeight,
            int Height,
            string Email,
            string PhoneNumber,
            int ActivityLevel);

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
        {
            var dbResult = await userCollection.GetAll();
            var result = dbResult.Select(a => new UserDto(a.Id.ToString(), a.Name, a.DateOfBirth, a.Gender, a.GoalWeight, a.Height,a.Email, a.PhoneNumber, a.ActivityLevel));
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

            return new UserDto(item.Id.ToString(), item.Name, item.DateOfBirth, item.Gender, item.GoalWeight, item.Height, item.Email, item.PhoneNumber, item.ActivityLevel);
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> Add(UserCreationDto item)
        {
            var na = new User(ObjectId.Empty, item.Name, item.DateOfBirth, item.Gender, item.GoalWeight, item.Height, item.Email, item.PhoneNumber, new List<ObjectId>(), new List<ObjectId>(), new List<ObjectId>(), item.ActivityLevel);
            await userCollection.InsertOneAsync(na);
            return CreatedAtRoute(nameof(GetSingleUser), new { id = na.Id },
                new UserDto(na.Id.ToString(), na.Name, na.DateOfBirth, na.Gender, na.GoalWeight, na.Height, na.Email, na.PhoneNumber, na.ActivityLevel));
        }

        [HttpPost]
        [Route(nameof(AddDailyProgressToUser))]
        public async Task<ActionResult<Boolean>> AddDailyProgressToUser(String userId, String dailyProgressId)
        {
            var usr = await userCollection.GetById(userId);
            if(usr == null) return NotFound(false);

            await userCollection.DeleteById(usr.Id);

            var listIds = usr.DailyProgressId;
            listIds.Add(ObjectId.Parse(dailyProgressId));

            var na = new User(usr.Id, usr.Name, usr.DateOfBirth, usr.Gender, usr.GoalWeight, usr.Height, usr.Email, usr.PhoneNumber, usr.RecipeIds, usr.ActivityIds, listIds, usr.ActivityLevel);

            await userCollection.InsertOneAsync(na);

            return Ok(true);
        }

        [HttpPost]
        [Route(nameof(AddActivityToUser))]
        public async Task<ActionResult<Boolean>> AddActivityToUser(String userId, String activityId)
        {
            var usr = await userCollection.GetById(userId);
            if (usr == null) return NotFound(false);

            await userCollection.DeleteById(usr.Id);

            var listIds = usr.ActivityIds;
            listIds.Add(ObjectId.Parse(activityId));

            var na = new User(usr.Id, usr.Name, usr.DateOfBirth, usr.Gender, usr.GoalWeight, usr.Height, usr.Email, usr.PhoneNumber, usr.RecipeIds, listIds, usr.DailyProgressId, usr.ActivityLevel);

            await userCollection.InsertOneAsync(na);

            return Ok(true);
        }

        [HttpPost]
        [Route(nameof(AddRecipeIdToUser))]
        public async Task<ActionResult<Boolean>> AddRecipeIdToUser(String userId, String recipeId)
        {
            var usr = await userCollection.GetById(userId);
            if (usr == null) return NotFound(false);

            await userCollection.DeleteById(usr.Id);

            var listIds = usr.RecipeIds;
            listIds.Add(ObjectId.Parse(recipeId));

            var na = new User(usr.Id, usr.Name, usr.DateOfBirth, usr.Gender, usr.GoalWeight, usr.Height, usr.Email, usr.PhoneNumber, listIds, usr.ActivityIds, usr.DailyProgressId, usr.ActivityLevel);

            await userCollection.InsertOneAsync(na);

            return Ok(true);
        }

        [HttpGet]
        [Route(nameof(GetSingleUserByUsername))]
        public async Task<ActionResult<User>> GetSingleUserByUsername(string username)
        {
            var user = await userCollection.GetUserByUsername(username);
            if (user == null) return NotFound();

            return Ok(new UserDto(user.Id.ToString(), user.Name, user.DateOfBirth, user.Gender, user.GoalWeight, user.Height, user.Email, user.PhoneNumber, user.ActivityLevel));
        }

        [HttpGet]
        [Route(nameof(GetAllRecipes))]
        public async Task<ActionResult<List<Recipe>>> GetAllRecipes(string userId)
        {
            var usr = await userCollection.GetById(userId);
            if (usr == null) return NotFound(false);

            List<Recipe> reclist = new();

            foreach (ObjectId i in usr.RecipeIds)
            {
                var recipes = await recipeCollection.GetById(i.ToString());
                if (recipes == null) break;
                reclist.Add(recipes);
            }

            return reclist;

        }

        [HttpGet]
        [Route(nameof(GetAllDailyProgress))]
        public async Task<ActionResult<List<DailyProgress>>> GetAllDailyProgress(string userId)
        {
            var usr = await userCollection.GetById(userId);
            if (usr == null) return NotFound();

            List<DailyProgress> dpList = new();

            foreach(var id in usr.DailyProgressId) {
                var cur = await dailyProgressCollection.GetById(id.ToString());
                if (cur == null) continue;
                dpList.Add(cur);
            }

            return dpList;
        }

        [HttpGet]
        [Route(nameof(GetAllActivitys))]
        public async Task<ActionResult<List<Activity>>> GetAllActivitys(string userId)
        {
            var usr = await userCollection.GetById(userId);
            if (usr == null) return NotFound(false);

            List<Activity> aclist = new();

            foreach (ObjectId i in usr.ActivityIds)
            {
                var activity = await activityCollection.GetById(i.ToString());
                if (activity == null) break;
                aclist.Add(activity);
            }

            return aclist;

        }


    }
}
