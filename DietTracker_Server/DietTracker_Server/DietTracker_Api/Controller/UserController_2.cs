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

    }
}
