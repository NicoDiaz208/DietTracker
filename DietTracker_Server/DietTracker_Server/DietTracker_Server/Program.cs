using System;
using DietTracker_Server.Classes;
using DietTracker_Server.Classes.User;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using DietTracker_Server.Classes.Achievement;
using MongoDB.Bson;

namespace DietTracker_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            MongoCRUD db = new MongoCRUD("DietTracker");
            AchievmentRepository ar = new AchievmentRepository("mongodb://localhost:27017");
            Achievement a = new Achievement(ObjectId.Empty, "Running", 1, 2);
            ar.AddAchievement(a, "DietTracker");

            ar.DeleteAchievement(a, "DietTracker");

        }
    }
}
