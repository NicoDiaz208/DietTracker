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
        [Route(nameof(GetListOfFood))]
        public async Task<ActionResult<List<FoodDto>>> GetListOfFood(string[] foods)
        {
            List<FoodDto> result = new List<FoodDto>();
            foreach (var item in foods)
            {
                var curr = await foodCollection.GetById(item);
                if(curr == null) continue;

                result.Add(new FoodDto(curr.Id.ToString(), curr.Name, curr.Unit, curr.Value, curr.Calories, curr.Protein, curr.TotalCarbohydrates, curr.Sugar, curr.Fiber, curr.Fat));
            }

            return result;
        }
    }
}
