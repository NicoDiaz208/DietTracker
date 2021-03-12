using System;
using DietTracker_Server.Classes;
using DietTracker_Server.Classes.User;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using DietTracker_Server.Classes.Achievement;

namespace DietTracker_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            MongoCRUD db = new MongoCRUD("DietTracker");
            AchievmentRepository ar = new AchievmentRepository("mongodb+srv://ameldz:Eldin2010@diettracker.ijgzi.mongodb.net/test");
            Achievement a = new Achievement("Running", 1, 2);
            ar.AddAchievement(a, "DietTracker");
            


        }
    }
}
