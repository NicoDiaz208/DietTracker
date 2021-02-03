using System;
using DietTracker_Server.Classes;
using DietTracker_Server.Classes.User;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace DietTracker_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            MongoCRUD db = new MongoCRUD("TestDietTracker");
            db.insertRecord("Users", new User ("Amel Dzogovic",new DateTime(2008, 5, 1, 8, 30, 52) ,"M", 70, 175,"dzogovicamel@gmail.com","Private",3 ));
            db.insertRecord("Achievements", new Achievements("running", 4.5, 7.8));
            db.insertRecord("Activity", new Activity(5435, 3.3, 7.5, 654, 233));
            db.insertRecord("CalorieIntake", new CalorieIntake(342, 2342));
            db.insertRecord("DailyProgress", new DailyProgress("walking", 2.2, 7.5));
            db.insertRecord("Food", new Food("Appel"));
            db.insertRecord("NutritionFacts", new NutritionFacts(77, 55, 44, 99, 22, 11));
            db.insertRecord("Recipe", new Recipe("Pizza", 2.22, 7, "not Vegan"));
            db.insertRecord("Sleep", new Sleep(4, 7));
            db.insertRecord("WaterIntake", new WaterIntake(8, 5));
        }
    }
}
