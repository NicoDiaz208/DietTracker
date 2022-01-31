using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace DietTracker_DataAccess
{
    public record Achievement(
        ObjectId Id, 
        string Name,
        double Now,
        string Description,
        double Goal) : IHaveId;

    public record Activity(
        ObjectId Id,
        AcitvityEnum name,
        double distance,		//km
        int minutes,			//duration of activity
        double burnedCalories,
        DateTime date) : IHaveId;

    public record CalorieIntake(
        ObjectId Id,
        double CalorieGoal,
        double CalorieCurrent,
        double FatGoal,
        double FatCurrent,
        double ProteinGoal,
        double ProteinCurrent,
        double CarbohydratesGoal,
        double CarbohydratesCurrent,
        DateTime Date) : IHaveId;

    public record DailyProgress(
        ObjectId Id,
        double Percentage,
        double Protein,
        double Calories,        //
        DateTime Date) : IHaveId;

    public record Food(
        ObjectId Id,
        string Name,
        string Unit,
        double Value,
        double Calories, 
        double Protein, 
        double TotalCarbohydrates, 
        double Sugar, 
        double Fiber, 
        double Fat) : IHaveId;

    //Not a Database Entity, just for Recipe. Holds Id from food and the Amount of it
    public record Ingredient(
        double Value,
        string FoodId);

    public record Recipe(
        ObjectId Picture,
        ObjectId Id,
        string Name,
        string PrepareTime,
        double Difficulty,
        string Preparation,
        List<Ingredient> FoodIds,
        List<Category> Categories) : IHaveId;

    public record Sleep(
        ObjectId Id,
        int HoSG,
        int HoSC,
        DateTime Date,
        ObjectId ActivityId) : IHaveId;

    public record FileInfo(string Id, string FileName);

    public record User(
        ObjectId Picture,
        ObjectId Id,
        string Name,
        DateTime DateOfBirth,
        string Gender,
        double GoalWeight,
        int Height,
        string Email,
        string PhoneNumber,
        double Weight,
        List<ObjectId> RecipeIds,
        List<ObjectId> ActivityIds,
        List<ObjectId> DailyProgressIds,
        List<ObjectId> CalorieIntakeIds,
        List<ObjectId> WaterIntakeIds,
        List<ObjectId> SleepIds,
        List<ObjectId> AchievementsIds,
        int ActivityLevel) : IHaveId;

    public record WaterIntake(
        ObjectId Id,
        int GoWG,
        ObjectId ActivityId,
        DateTime Date,
        int GoWC): IHaveId;

    public record Login(
        ObjectId Id,
        string Username,
        string Password): IHaveId;

    public record Category(
        ObjectId Id,
        string category): IHaveId;
}
