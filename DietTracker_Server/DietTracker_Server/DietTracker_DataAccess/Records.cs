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
        int Steps,
        double ActiveTime,
        double GoalTime,
        double BurnedCalories,
        bool IsDone,
        DateTime Date,
        double Distance) : IHaveId;

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
        double Now,
        double Protein,
        double Calories,
        DateTime Date) : IHaveId;

    public record Food(
        ObjectId Id,
        ObjectId NutritionFactIds,
        string Name) : IHaveId;

    public record NutritionFacts(
        ObjectId Id,
        double Calories,
        double Protein,
        double TotalCarbohydrates,
        double Sugar,
        double Fiber,
        double Fat): IHaveId;
    public record Ingredient(
        ObjectId Id,
        double Value,
        string Unit);

    public record Recipe(
        ObjectId Picture,
        ObjectId Id,
        string Name,
        string PrepareTime,
        double Difficulty,
        string Preparation,
        List<Ingredient> FoodIds,
        List<Category> CategorysId) : IHaveId;

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
