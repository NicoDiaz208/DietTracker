using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace DietTracker_Server.Classes.Sleep
{
    class DailyProgressService
    {
        DailyProgressRepository sr = new DailyProgressRepository();

        public string AddAchievements(Sleep sleep)
        {
            var newsleep = ConvertToBson(sleep);
            return sr.AddSleep(newsleep);
        }

        public string DeleteAchievements(Sleep sleep)
        {
            var newsleep = ConvertToBson(sleep);
            return sr.DeleteSleep(newsleep);
        }

        private BsonDocument ConvertToBson(Sleep sleep)
        {
            var document = new BsonDocument
            {
                { "HoSG", sleep.HoSG },
                { "HoSC", sleep.HoSC }
            };
            return document;
        }
    }
}
