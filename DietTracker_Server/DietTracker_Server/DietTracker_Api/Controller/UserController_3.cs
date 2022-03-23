using DietTracker_DataAccess;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DietTracker_Api.Controller
{
    public partial class UserController : ControllerBase
    {
        [HttpGet]
        [Route(nameof(GetSingleBMR))]
        public async Task<ActionResult<int>> GetSingleBMR(string id)
        {
            var item = await userCollection.GetById(id);
            if (item == null)
            {
                return NotFound();
            }
            var bmr = 66.47 + (13.75 * item.Weight) + (5.003 * item.Height) - 6.755 * GetAgeSolo(item);
            return Ok(Convert.ToInt32(bmr));
        }

        [HttpGet]
        [Route(nameof(GetOutPut))]
        public async Task<ActionResult<int>> GetOutPut(string usrId, string distance)
        {
            var usr = await userCollection.GetById(usrId);
            if (usr == null) return NotFound();

            var dis = Convert.ToInt32(distance);
            var FullOutput = usr.Weight * dis *0.95;
            return Convert.ToInt32(FullOutput);
                
        }

        [HttpGet]
        [Route(nameof(GetCaloriesFromRunning))]
        public async Task<ActionResult<int>> GetCaloriesFromRunning(string usrId,double time)
        {
            time = time / 60;
            var usr = await userCollection.GetById(usrId);
            if (usr == null) return NotFound();

            var OutPut = usr.Weight * time * 7;
            return Ok(Convert.ToInt32(OutPut));
        }

        [HttpGet]
        [Route(nameof(GetCaloriesFromSwimming))]
        public async Task<ActionResult<int>> GetCaloriesFromSwimming(string usrId, double time)
        {
            time = time / 60;
            var usr = await userCollection.GetById(usrId);
            if ( usr == null) return NotFound();

            var OutPut = usr.Weight * time * 10;
            return Ok(Convert.ToInt32(OutPut));
        }

        [HttpGet]
        [Route(nameof(GetCaloriesFromBicycling))]
        public async Task<ActionResult<int>> GetCaloriesFromBicycling(string usrId, double time)
        {
            time = time / 60;
            var usr = await userCollection.GetById(usrId);
            if(usr == null) return NotFound();

            var OutPut = usr.Weight * time * 5;
            return Ok(Convert.ToInt32(OutPut));
        }

        [HttpGet]
        [Route(nameof(GetCaloriesFromWalking))]
        public async Task<ActionResult<int>> GetCaloriesFromWalking(string usrId, double time)
        {
            time = time / 60;
            var usr = await userCollection.GetById(usrId);
            if( usr == null) return NotFound();

            var OutPut = usr.Weight * time * 3;
            return Ok(Convert.ToInt32(OutPut));
        }

        [HttpGet]
        [Route(nameof(GetLastProgress))]
        public async Task<ActionResult<List<double>>> GetLastProgress(string usrId)
        {
            var usr = await userCollection.GetById(usrId);
            if (usr == null) return NotFound();

            var intake = new List<double>(new double[5]);

            int count = 0;
            foreach(var item in usr.CalorieIntakeIds)
            {
                if (count == 4) break;
                var calories = await calorieIntakeCollection.GetById(item);
                if(calories == null) return NotFound();
                intake[count] = calories.CalorieCurrent;
                count++;
            }
            count = 0;

            foreach( var item in usr.DailyProgressIds)
            {
                if(count== 4) break;
                var calories = await dailyProgressCollection.GetById(item);
                if (calories == null) return NotFound();
                intake[count] = intake[count] - calories.Calories;
                count++;
            }

            return intake;
        }

    }
}
