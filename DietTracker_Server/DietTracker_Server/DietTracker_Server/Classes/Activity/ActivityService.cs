using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace DietTracker_Server.Classes.Activity
{
    class ActivityService
    {
        ActivityRepository ar = new ActivityRepository();

        public string AddUser(Activity activity)
        {
            var newActivity = ConvertToBson(activity);
            return ar.AddActivity(newActivity);
        }

        public string DeleteUser(Activity activity)
        {
            var toDeleteActivity = ConvertToBson(activity);
            return ar.DeletedActivity(toDeleteActivity);
        }

        public BsonDocument ConvertToBson(Activity activity)
        {
            var document = new BsonDocument
            {
                { "steps", activity.steps },
                { "AktiveTime", activity.AktiveTime },
                { "GoalTime", activity.GoalTime },
                { "BurnedCalories", activity.BurnedCalories },
                { "Distance", activity.Distance }
            };
            return document;
        }
    }
}
