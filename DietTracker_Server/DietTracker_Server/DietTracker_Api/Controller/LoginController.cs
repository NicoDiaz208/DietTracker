﻿using DietTracker_DataAccess;
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

        private readonly IMongoCollection<User> userCollection;

        public LoginController(CollectionFactory cf, CollectionFactory cf2)
        {
            loginCollection = cf.GetCollection<Login>();
            userCollection = cf2.GetCollection<User>();
        }


        public record LoginDto(
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

            return new LoginDto(item.Username,item.Password);
        }

        [HttpGet(nameof(GetSingleLogin))]
        public async Task<ActionResult<String>> GetSingleLogin(string name,string password)
        {
            var item = await loginCollection.GetByNameAndPassword(name, password);
            var usr = await userCollection.GetUserByUsername(name);

            if (item == null || usr == null)
            {
                return NotFound();
            }

            return usr.Id.ToString();
        }

        [HttpPost]
        public async Task<ActionResult<LoginDto>> Add(LoginDto item)
        {
            var na = new Login(ObjectId.Empty, item.Username,item.Password);
            await loginCollection.InsertOneAsync(na);
            return CreatedAtRoute(nameof(GetSingleLogin), new { id = na.Id },
                new LoginDto( na.Username, na.Password));
        }
    }
}
