using DietTracker_DataAccess;
using Microsoft.AspNetCore.Mvc;
using System;
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
        public async Task<ActionResult<int>> GetOutPut(string id, string distance)
        {
            var usr = await userCollection.GetById(id);
            var dis = Convert.ToInt32(distance);
            var FullOutput = usr.Weight * dis *0.95;
            return Convert.ToInt32(FullOutput);
                
        }

        [HttpGet]
        [Route(nameof(GetCaloriesFromRunning))]
        public async Task<ActionResult<int>> GetCaloriesFromRunning(string id, int time)
        {
            var usr = await userCollection.GetById(id);
            var OutPut = usr.Weight * time * 7;
            return Ok(Convert.ToInt32(OutPut));
        }

        [HttpGet]
        [Route(nameof(GetCaloriesFromSwimming))]
        public async Task<ActionResult<int>> GetCaloriesFromSwimming(string id, int time)
        {
            var usr = await userCollection.GetById(id);
            var OutPut = usr.Weight * time * 10;
            return Ok(Convert.ToInt32(OutPut));
        }

        [HttpGet]
        [Route(nameof(GetCaloriesFromBicycling))]
        public async Task<ActionResult<int>> GetCaloriesFromBicycling(string id, int time)
        {
            var usr = await userCollection.GetById(id);
            var OutPut = usr.Weight * time * 5;
            return Ok(Convert.ToInt32(OutPut));
        }

        [HttpGet]
        [Route(nameof(GetCaloriesFromWalking))]
        public async Task<ActionResult<int>> GetCaloriesFromWalking(string id, int time)
        {
            var usr = await userCollection.GetById(id);
            var OutPut = usr.Weight * time * 3;
            return Ok(Convert.ToInt32(OutPut));
        }


    }
}
