using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace DietTracker_Server.Classes.Achievements
{
    class DailyProgressService
    {
        DailyProgressRepository ar = new DailyProgressRepository();

        public string AddAchievements(Achievements achievements)
        {
            var newachievement = ConvertToBson(achievements);
            return ar.AddAchievements(newachievement);
        }

        public string DeleteAchievements(Achievements achievements)
        {
            var newachievement = ConvertToBson(achievements);
            return ar.DeleteAchievements(newachievement);
        }

        private BsonDocument ConvertToBson(Achievements achievements)
        {
            var document = new BsonDocument
            {
                { "name", achievements.Name },
                { "Now", achievements.Now },
                { "Goal", achievements.Goal }
            };
            return document;
        }
    }
}
