using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace DietTracker_Server.Classes.DailyProgress
{
    class DailyProgressService
    {
        DailyProgressRepository dr = new DailyProgressRepository();

        public String AddDailyProgress(DailyProgress dailyProgress)
        {
            var newdaily = ConvertToBson(dailyProgress);
            return dr.AddDailyProgress(newdaily);
        }

        public String DeleteDailyProgress(DailyProgress dailyProgress)
        {
            var newachievement = ConvertToBson(dailyProgress);
            return dr.DeleteDailyProgress(newachievement);
        }

        public String ReplaceDailyProgress(DailyProgress old,DailyProgress newdP)
        {
            var toReplace = ConvertToBson(old);
            var replacement = ConvertToBson(newdP);
            return dr.ReplaceDailyProgress(toReplace, replacement);
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
