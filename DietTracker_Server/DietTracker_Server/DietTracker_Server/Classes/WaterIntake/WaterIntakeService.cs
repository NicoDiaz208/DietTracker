using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace DietTracker_Server.Classes.WaterIntake
{
    class DailyProgressService
    {
        DailyProgressRepository wir = new DailyProgressRepository();

        public string AddAchievements(WaterIntake waterIntake)
        {
            var newwaterintake = ConvertToBson(waterIntake);
            return wir.AddWaterIntake(newwaterintake);
        }

        public string DeleteAchievements(WaterIntake waterIntake)
        {
            var newwaterintake = ConvertToBson(waterIntake);
            return wir.DeleteWaterIntake(newwaterintake);
        }

        private BsonDocument ConvertToBson(WaterIntake waterIntake)
        {
            var document = new BsonDocument
            {
                { "GoWG", waterIntake.GoWG },
                { "GoWC", waterIntake.GoWC }
            };
            return document;
        }
    }
}
