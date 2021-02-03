using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace DietTracker_Server.Classes.DailyProgress
{
    class DailyProgressService
    {
        DailyProgressRepository dr = new DailyProgressRepository();

        public string AddAchievements(DailyProgress dailyProgress)
        {
            var newdaily = ConvertToBson(dailyProgress);
            return dr.AddDailyProgress(newdaily);
        }

        public string DeleteAchievements(DailyProgress dailyProgress)
        {
            var newachievement = ConvertToBson(dailyProgress);
            return dr.DeleteDailyProgress(newachievement);
        }

        private BsonDocument ConvertToBson(DailyProgress dailyProgress)
        {
            var document = new BsonDocument
            {
                { "name", dailyProgress.Name },
                { "Now", dailyProgress.Now },
                { "Goal", dailyProgress.Goal }
            };
            return document;
        }
    }
}
