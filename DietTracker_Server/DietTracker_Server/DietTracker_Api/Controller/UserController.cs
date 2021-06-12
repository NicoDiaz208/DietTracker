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
    public partial class UserController : ControllerBase
    {
        private readonly IMongoCollection<User> userCollection;

        private readonly IMongoCollection<Recipe> recipeCollection;

        private readonly IMongoCollection<DailyProgress> dailyProgressCollection;

        private readonly IMongoCollection<Activity> activityCollection;

        private readonly IMongoCollection<CalorieIntake> calorieIntakeCollection;

        public UserController(CollectionFactory cf,CollectionFactory rf, CollectionFactory dp,CollectionFactory ac, CollectionFactory ci)
        {
            userCollection = cf.GetCollection<User>();
            recipeCollection = rf.GetCollection<Recipe>();
            dailyProgressCollection = dp.GetCollection<DailyProgress>();
            activityCollection = ac.GetCollection<Activity>();
            calorieIntakeCollection = ci.GetCollection<CalorieIntake>();
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
            var na = new User(ObjectId.Empty, item.Name, item.DateOfBirth, item.Gender, item.GoalWeight, item.Height, item.Email, item.PhoneNumber, new List<ObjectId>(), new List<ObjectId>(), new List<ObjectId>(), new List<ObjectId>(), item.ActivityLevel);
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

            var listIds = usr.DailyProgressIds;
            listIds.Add(ObjectId.Parse(dailyProgressId));

            var na = new User(usr.Id, usr.Name, usr.DateOfBirth, usr.Gender, usr.GoalWeight, usr.Height, usr.Email, usr.PhoneNumber, usr.RecipeIds, usr.ActivityIds, listIds, usr.CalorieIntakeIds, usr.ActivityLevel);

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

            var na = new User(usr.Id, usr.Name, usr.DateOfBirth, usr.Gender, usr.GoalWeight, usr.Height, usr.Email, usr.PhoneNumber, usr.RecipeIds, listIds, usr.DailyProgressIds, usr.CalorieIntakeIds, usr.ActivityLevel);

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

            var na = new User(usr.Id, usr.Name, usr.DateOfBirth, usr.Gender, usr.GoalWeight, usr.Height, usr.Email, usr.PhoneNumber, listIds, usr.ActivityIds, usr.DailyProgressIds,usr.CalorieIntakeIds, usr.ActivityLevel);

            await userCollection.InsertOneAsync(na);

            return Ok(true);
        }

        [HttpPost]
        [Route(nameof(AddCalorieIntakeToUser))]
        public async Task<ActionResult<Boolean>> AddCalorieIntakeToUser(String userId, String calorieIntakeId)
        {
            var usr = await userCollection.GetById(userId);
            if (usr == null) return NotFound(false);

            await userCollection.DeleteById(usr.Id);

            var listIds = usr.CalorieIntakeIds;
            listIds.Add(ObjectId.Parse(calorieIntakeId));

            var na = new User(usr.Id, usr.Name, usr.DateOfBirth, usr.Gender, usr.GoalWeight, usr.Height, usr.Email, usr.PhoneNumber, usr.RecipeIds, usr.ActivityIds, usr.DailyProgressIds, listIds, usr.ActivityLevel);

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

        [HttpPost]
        [Route(nameof(CalculateDailyProgress))]
        public async Task<ActionResult<String>> CalculateDailyProgress(string userId, DateTime date)
        {
            var usr = await userCollection.GetById(userId);
            if (usr == null) return NotFound();

            var acList = new List<Activity>();
            var countIsTrue = 0;                //Counts all IsDone from Activities which are TRUE
            foreach (var i in usr.ActivityIds)
            {
                var cur = await activityCollection.GetById(i.ToString());
                if (cur == null) continue;

                if (cur.IsDone == true && isSameDay(cur.Date, date))
                {
                    countIsTrue++;
                    acList.Add(cur);
                }
                else if(cur.IsDone == false && isSameDay(cur.Date, date))
                {
                    acList.Add(cur);
                }
            }

            var acLength = acList.Count;
            var percentage = (acLength == 0) ? 0 : 100 * (Convert.ToDouble(countIsTrue) / Convert.ToDouble(acLength));

            DailyProgress? dailyProgress = null;
            foreach(var i in usr.DailyProgressIds)
            {
                var cur = await dailyProgressCollection.GetById(i.ToString());
                if (cur == null) continue;

                if (isSameDay(cur.Date, date))
                {
                    dailyProgress = cur;
                    await dailyProgressCollection.DeleteById(dailyProgress.Id);
                    break;
                }
            }

            if(dailyProgress == null)
            {
                dailyProgress = new DailyProgress(ObjectId.GenerateNewId(), percentage, date);

                usr.DailyProgressIds.Add(dailyProgress.Id);
                await userCollection.ReplaceById(userId, usr);
            }

            DailyProgress dp = new DailyProgress(dailyProgress.Id, percentage, dailyProgress.Date);

            await dailyProgressCollection.InsertOneAsync(dp);

            return dailyProgress.Id.ToString();
        }

        private bool isSameDay(DateTime a, DateTime b)
        {
            var aStr = a.ToString().Substring(0, 10);
            var bStr = b.ToString().Substring(0,10);

            if (aStr == bStr) return true;
           
            return false;
        }

        [HttpPost]
        [Route(nameof(Replace))]
        public async Task<ActionResult<UserDto>> Replace(UserDto usr, string id)
        {
            var na = new User(ObjectId.Parse(id), usr.Name, usr.DateOfBirth, usr.Gender, usr.GoalWeight, usr.Height, usr.Email, usr.PhoneNumber, usr.RecipeIds, usr.ActivityIds, usr.DailyProgressIds, listIds, usr.ActivityLevel);
            await userCollection.ReplaceById(id, na);
            return Ok(200);
        }
    }
}
