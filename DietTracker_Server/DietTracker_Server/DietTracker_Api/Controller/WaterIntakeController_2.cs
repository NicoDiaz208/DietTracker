using DietTracker_DataAccess;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DietTracker_Api.Controller
{
    public partial class WaterIntakeController : ControllerBase
    {
        [HttpPost]
        [Route(nameof(AddActivityToWaterIntake))]
        public async Task<ActionResult<Boolean>> AddActivityToWaterIntake(String waterIntakeId, String activityId)
        {
            var waterIntake = await waterIntakeCollection.GetById(waterIntakeId);
            if (waterIntake == null) return NotFound(false);

            await waterIntakeCollection.DeleteById(waterIntake.Id);

            var na = new WaterIntake(waterIntake.Id, waterIntake.GoWG, ObjectId.Parse(activityId), waterIntake.GoWC);

            await waterIntakeCollection.InsertOneAsync(na);

            return Ok(true);
        }
    }
}
