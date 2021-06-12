using DietTracker_DataAccess;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DietTracker_Api.Controller
{
    public partial class FoodController : ControllerBase
    {
        [HttpPost]
        [Route(nameof(AddNutritionFactToFood))]
        public async Task<ActionResult<Boolean>> AddNutritionFactToFood(String foodId, String nutritionFactId)
        {
            var food = await foodCollection.GetById(foodId);
            if (food == null) return NotFound(false);

            await foodCollection.DeleteById(food.Id);

            var na = new Food(food.Id, ObjectId.Parse(nutritionFactId), food.Name);

            await foodCollection.InsertOneAsync(na);

            return Ok(true);
        }

    }
}
