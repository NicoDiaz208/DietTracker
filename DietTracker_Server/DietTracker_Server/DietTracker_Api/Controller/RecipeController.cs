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
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IMongoCollection<Recipe> recipeCollection;

        public RecipeController(CollectionFactory cf)
        {
            recipeCollection = cf.GetCollection<Recipe>();
        }

        public record RecipeCreationDto(
            string Name, double PrepareTime, double Difficulty, string Category);

        public record RecipeDto(
            string Id,
            string Name, double PrepareTime, double Difficulty, string Category);

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecipeDto>>> GetAll()
        {
            var dbResult = await recipeCollection.GetAll();
            var result = dbResult.Select(a => new RecipeDto(a.Id.ToString(), a.Name,a.PrepareTime,a.Difficulty,a.Category));
            return Ok(result);
        }

        [HttpGet("{id}", Name = nameof(GetSingleRecipe))]
        public async Task<ActionResult<RecipeDto>> GetSingleRecipe(string id)
        {
            var item = await recipeCollection.GetById(id);
            if (item == null)
            {
                return NotFound();
            }

            return new RecipeDto(item.Id.ToString(), item.Name, item.PrepareTime, item.Difficulty, item.Category);
        }

        [HttpPost]
        public async Task<ActionResult<RecipeDto>> Add(RecipeCreationDto item)
        {
            var na = new Recipe(ObjectId.Empty, item.Name, item.PrepareTime, item.Difficulty, item.Category);
            await recipeCollection.InsertOneAsync(na);
            return CreatedAtRoute(nameof(GetSingleRecipe), new { id = na.Id },
                new RecipeDto(na.Id.ToString(), na.Name, na.PrepareTime, na.Difficulty, na.Category));
        }

        [HttpGet]
        [Route("/RandomRecipe")]
        public async Task<ActionResult<RecipeDto>> GetRandom()
        {
            var rand = new Random();
            var cnt = await recipeCollection.CountDocumentsAsync(new BsonDocument());

            var res = await recipeCollection
                .Find(new BsonDocument())
                .Skip(rand.Next(0, Convert.ToInt32(cnt)))
                .Limit(1)
                .FirstOrDefaultAsync();

            if (res == null) return NotFound();

            return Ok(res);
        }
    }
}