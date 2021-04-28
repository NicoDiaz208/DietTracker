using MongoDB.Bson;
using System;

namespace DietTracker_DataAccess
{
    public record Achievement(
        ObjectId Id, 
        string Name,
        double Now,
        double Goal) : IHaveId;

    public record Activity(
        ObjectId Id,
        int Steps,
        double AktiveTime,
        double GoalTime,
        double BurnedCalories,
        double Distance) : IHaveId;

    public record CalorieIntake(
        ObjectId Id,
        double Current,
        double Now) : IHaveId;

    public record DailyProgress(
        ObjectId Id,
        string Name,
        double Now,
        double Goal) : IHaveId;

    public record Food(
        ObjectId Id,
        string Name) : IHaveId;

    public record NutritionFacts(
        ObjectId Id,
        double Calories,
        double Protein,
        double TotalCarbohydrates,
        double Sugar,
        double Fiber,
        double Fat): IHaveId;

    public record Recipe(
        ObjectId Id,
        string Name,
        double PrepareTime,
        double Difficulty,
        string Kategorie) : IHaveId;

    public record Sleep(
        ObjectId Id,
        int HoSG,
        int HoSC) : IHaveId;

    public record User(
        ObjectId Id,
        string Name,
        DateTime DateofBirth,
        string Gender,
        double GoalWeight,
        int Height,
        string Email,
        string Phonenumber,
        int ActivityLevel) : IHaveId;

    public record WaterIntake(
        ObjectId Id,
        int GoWG,
        int GoWC): IHaveId;
}
