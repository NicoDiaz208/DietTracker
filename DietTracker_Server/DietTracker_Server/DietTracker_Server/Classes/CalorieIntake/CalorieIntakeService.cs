using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace DietTracker_Server.Classes.CalorieIntake
{
    class CalorieIntakeService
    {
        CalorieIntakeRepository cr = new CalorieIntakeRepository();

        public string AddUser(CalorieIntake calorie)
        {
            var newcalorie = ConvertToBson(calorie);
            return cr.AddCalorie(newcalorie);
        }

        public string DeleteUser(CalorieIntake calorie)
        {
            var toDeleteCalorie = ConvertToBson(calorie);
            return cr.DeleteCalorie(toDeleteCalorie);
        }

        public BsonDocument ConvertToBson(CalorieIntake calorie)
        {
            var document = new BsonDocument
            {
                { "Current", calorie.Current },
                { "Now", calorie.Now }
            };
            return document;
        }
    }
}
