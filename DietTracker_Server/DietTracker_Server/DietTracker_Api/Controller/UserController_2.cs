using DietTracker_DataAccess;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
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

            var na = new User(usr.Id, usr.Name, usr.DateOfBirth, usr.Gender, usr.GoalWeight, usr.Height, usr.Email, usr.PhoneNumber, usr.Weight, usr.RecipeIds, usr.ActivityIds, usr.DailyProgressIds, usr.CalorieIntakeIds, usr.WaterIntakeIds, listIds, usr.AchievementsIds, usr.ActivityLevel);

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

            var listIds = usr.WaterIntakeIds;
            listIds.Add(ObjectId.Parse(waterIntakeId));

            var na = new User(usr.Id, usr.Name, usr.DateOfBirth, usr.Gender, usr.GoalWeight, usr.Height, usr.Email, usr.PhoneNumber, usr.Weight, usr.RecipeIds, usr.ActivityIds, usr.DailyProgressIds, usr.CalorieIntakeIds, listIds, usr.SleepIds, usr.AchievementsIds, usr.ActivityLevel);

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
        [Route(nameof(GetAllAchievements))]
        public async Task<ActionResult<List<Achievement>>> GetAllAchievements(string userId)
        {
            var usr = await userCollection.GetById(userId);
            if (usr == null) return NotFound();

            List<Achievement> dpList = new();

            foreach (var id in usr.AchievementsIds)
            {
                var cur = await achievementCollection.GetById(id.ToString());
                if (cur == null) continue;
                dpList.Add(cur);
            }

            return dpList;
        }

        [HttpPost]
        [Route(nameof(AddAchievementToUser))]
        public async Task<ActionResult<Boolean>> AddAchievementToUser(String userId, String achievementId)
        {
            var usr = await userCollection.GetById(userId);
            if (usr == null) return NotFound(false);

            await userCollection.DeleteById(usr.Id);

            var listIds = usr.AchievementsIds;
            listIds.Add(ObjectId.Parse(achievementId));

            var na = new User(usr.Id, usr.Name, usr.DateOfBirth, usr.Gender, usr.GoalWeight, usr.Height, usr.Email, usr.PhoneNumber, usr.Weight, usr.RecipeIds, usr.ActivityIds, usr.DailyProgressIds, usr.CalorieIntakeIds, usr.WaterIntakeIds, usr.SleepIds, listIds, usr.ActivityLevel);

            await userCollection.InsertOneAsync(na);

            return Ok(true);
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

        [HttpGet]
        [Route(nameof(GetWaterIntakeByDate))]
        public async Task<ActionResult<String>> GetWaterIntakeByDate(String userId, DateTime date)
        {
            var usr = await userCollection.GetById(userId);
            if (usr == null) return NotFound();

            foreach (var i in usr.WaterIntakeIds)
            {
                var ci = await waterIntakeCollection.GetById(i);
                if (ci == null) continue;

                if (isSameDay(date, ci.Date))
                {
                    return ci.Id.ToString();
                }
            }
            var ciNew = new WaterIntake(ObjectId.Empty, 0, ObjectId.Empty, date, 0);
            waterIntakeCollection.InsertOne(ciNew);
            await this.AddWaterIntakeToUser(userId, ciNew.Id.ToString());
            return ciNew.Id.ToString();

        }

        [HttpPost, DisableRequestSizeLimit]
        [Route(nameof(UploadImage))]
        public async Task<IActionResult> UploadImage(string name, string source)
        {
            var formCollection = await Request.ReadFormAsync();
            var file = formCollection.Files[0];
            if (file.Length > 0)
            {
                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition)?.FileName?.Trim('"');
                using (var stream = await bucket.OpenUploadStreamAsync(fileName))
                {
                    await file.CopyToAsync(stream);
                    await stream.CloseAsync();
                }

                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        public record FileInfo(string Id, string FileName);

        [HttpGet("images")]
        public async Task<ActionResult<FileInfo[]>> GetImages()
        {
            using var cursor = await bucket.FindAsync(Builders<GridFSFileInfo>.Filter.Empty);
            var files = await cursor.ToListAsync();
            return files.Select(f => new FileInfo(f.Id.ToString(), f.Filename)).ToArray();
        }

        [HttpGet("images/{id}")]
        public async Task<IActionResult> GetImage(string id)
        {
            var oid = new ObjectId(id);
            return File(await bucket.OpenDownloadStreamAsync(oid), "image/jpeg");
        }

    }
}
