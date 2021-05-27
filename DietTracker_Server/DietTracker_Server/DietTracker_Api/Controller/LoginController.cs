using DietTracker_DataAccess;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DietTracker_Api.Controller
{
    [Route("api/[Controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IMongoCollection<Login> loginCollection;

        public LoginController(CollectionFactory cf)
        {
            loginCollection = cf.GetCollection<Login>();
        }


        public record LoginDto(
            string id,
            string Username,
            string Password
            );

        


        [HttpGet("{id}", Name = nameof(GetSingleLogin))]
        public async Task<ActionResult<LoginDto>> GetSingleLogin(string id)
        {
            var item = await loginCollection.GetById(id);
            if (item == null)
            {
                return NotFound();
            }

            return new LoginDto(item.Id.ToString(),item.Username,item.Password);
        }

        [HttpGet("test")]
        public async Task<ActionResult<LoginDto>> GetSingleLogin(string name,string password)
        {
            var item = await loginCollection.GetByNameAndPassword(name, password);
            if (item == null)
            {
                return NotFound();
            }

            return new LoginDto(item.Id.ToString(), item.Username, item.Password);
        }



        [HttpPost]
        public async Task<ActionResult<LoginDto>> Add(LoginDto item)
        {
            var na = new Login(ObjectId.Empty, item.Username,item.Password);
            await loginCollection.InsertOneAsync(na);
            return CreatedAtRoute(nameof(GetSingleLogin), new { id = na.Id },
                new LoginDto(na.Id.ToString(), na.Password, na.Username));
        }
    }
}
