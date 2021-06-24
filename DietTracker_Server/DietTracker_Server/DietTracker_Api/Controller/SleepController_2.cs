using DietTracker_DataAccess;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DietTracker_Api.Controller
{
    public partial class SleepController : ControllerBase
    {
        [HttpPost]
        [Route(nameof(AddActivityToSleep))]
        public async Task<ActionResult<Boolean>> AddActivityToSleep(String sleepId, String activityId)
        {
            var sleep = await sleepCollection.GetById(sleepId);
            if (sleep == null) return NotFound(false);

            await sleepCollection.DeleteById(sleep.Id);

            var na = new Sleep(sleep.Id, sleep.HoSG, sleep.HoSC,sleep.Date, ObjectId.Parse(activityId));

            await sleepCollection.InsertOneAsync(na);

            return Ok(true);
        }
    }
}
