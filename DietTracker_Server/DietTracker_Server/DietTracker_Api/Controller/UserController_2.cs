using DietTracker_DataAccess;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DietTracker_Api.Controller
{
    public partial class UserController : ControllerBase
    {

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

            foreach (var id in usr.DailyProgressIds)
            {
                var cur = await dailyProgressCollection.GetById(id.ToString());
                if (cur == null) continue;
                dpList.Add(cur);
            }

            return dpList;
        }

        [HttpGet]
        [Route(nameof(GetAllActivities))]
        public async Task<ActionResult<List<Activity>>> GetAllActivities(string userId)
        {
            var usr = await userCollection.GetById(userId);
            if (usr == null) return NotFound();

            List<Activity> aclist = new();

            foreach (ObjectId i in usr.ActivityIds)
            {
                var activity = await activityCollection.GetById(i.ToString());
                if (activity == null) break;
                aclist.Add(activity);
            }

            return aclist;

        }

        [HttpGet]
        [Route(nameof(GetAllCalorieIntake))]
        public async Task<ActionResult<List<CalorieIntake>>> GetAllCalorieIntake(string userId)
        {
            var usr = await userCollection.GetById(userId);
            if (usr == null) return NotFound();

            List<CalorieIntake> dpList = new();

            foreach (var id in usr.CalorieIntakeIds)
            {
                var cur = await calorieIntakeCollection.GetById(id.ToString());
                if (cur == null) continue;
                dpList.Add(cur);
            }

            return dpList;
        }

        [HttpPost]
        [Route(nameof(AddSleepToUser))]
        public async Task<ActionResult<Boolean>> AddSleepToUser(String userId, String sleepId)
        {
            var usr = await userCollection.GetById(userId);
            if (usr == null) return NotFound(false);

            await userCollection.DeleteById(usr.Id);

            var listIds = usr.CalorieIntakeIds;
            listIds.Add(ObjectId.Parse(sleepId));

            var na = new User(usr.Id, usr.Name, usr.DateOfBirth, usr.Gender, usr.GoalWeight, usr.Height, usr.Email, usr.PhoneNumber, usr.Weight, usr.RecipeIds, usr.ActivityIds, usr.DailyProgressIds, usr.CalorieIntakeIds, usr.WaterIntakeIds, listIds, usr.ActivityLevel);

            await userCollection.InsertOneAsync(na);

            return Ok(true);
        }

        [HttpPost]
        [Route(nameof(AddWaterIntakeToUser))]
        public async Task<ActionResult<Boolean>> AddWaterIntakeToUser(String userId, String waterIntakeId)
        {
            var usr = await userCollection.GetById(userId);
            if (usr == null) return NotFound(false);

            await userCollection.DeleteById(usr.Id);

            var listIds = usr.CalorieIntakeIds;
            listIds.Add(ObjectId.Parse(waterIntakeId));

            var na = new User(usr.Id, usr.Name, usr.DateOfBirth, usr.Gender, usr.GoalWeight, usr.Height, usr.Email, usr.PhoneNumber, usr.Weight, usr.RecipeIds, usr.ActivityIds, usr.DailyProgressIds, usr.CalorieIntakeIds, listIds, usr.SleepIds, usr.ActivityLevel);

            await userCollection.InsertOneAsync(na);

            return Ok(true);
        }

        [HttpGet]
        [Route(nameof(GetAllSleeps))]
        public async Task<ActionResult<List<Sleep>>> GetAllSleeps(string userId)
        {
            var usr = await userCollection.GetById(userId);
            if (usr == null) return NotFound();

            List<Sleep> dpList = new();

            foreach (var id in usr.SleepIds)
            {
                var cur = await sleepCollection.GetById(id.ToString());
                if (cur == null) continue;
                dpList.Add(cur);
            }

            return dpList;
        }

        
        [HttpGet]
        [Route(nameof(GetAllWaterIntakes))]
        public async Task<ActionResult<List<WaterIntake>>> GetAllWaterIntakes(string userId)
        {
            var usr = await userCollection.GetById(userId);
            if (usr == null) return NotFound();

            List<WaterIntake> dpList = new();

            foreach (var id in usr.WaterIntakeIds)
            {
                var cur = await waterIntakeCollection.GetById(id.ToString());
                if (cur == null) continue;
                dpList.Add(cur);
            }

            return dpList;
        }

        [HttpGet]
        [Route(nameof(GetSleepByDate))]
        public async Task<ActionResult<String>> GetSleepByDate(String userId, DateTime date)
        {
            var usr = await userCollection.GetById(userId);
            if (usr == null) return NotFound();

            foreach (var i in usr.SleepIds)
            {
                var ci = await sleepCollection.GetById(i);
                if (ci == null) continue;

                if (isSameDay(date, ci.Date))
                {
                    return ci.Id.ToString();
                }
            }
            var ciNew = new Sleep(ObjectId.Empty, 0, 0, date, ObjectId.Empty);
            sleepCollection.InsertOne(ciNew);
            await this.AddSleepToUser(userId, ciNew.Id.ToString());
            return ciNew.Id.ToString();

        }
    }
}
