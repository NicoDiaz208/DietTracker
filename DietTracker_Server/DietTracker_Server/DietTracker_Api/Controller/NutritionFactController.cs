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
    public class NutritionFactController : ControllerBase
    {
        private readonly IMongoCollection<NutritionFacts> nutritionFactCollection;

        public NutritionFactController(CollectionFactory cf)
        {
            nutritionFactCollection = cf.GetCollection<NutritionFacts>();
        }

        public record NutritionFactCreationDto(
            double Calories, double Protein, double TotalCarbohydrates, double Sugar, double Fiber, double Fat);

        public record NutritionFactDto(
            string Id, double Calories, double Protein, double TotalCarbohydrates, double Sugar, double Fiber, double Fat);

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NutritionFactDto>>> GetAll()
        {
            var dbResult = await nutritionFactCollection.GetAll();
            var result = dbResult.Select(a => new NutritionFactDto(a.Id.ToString(), a.Calories,a.Protein,a.TotalCarbohydrates,a.Sugar,a.Fiber,a.Fat));
            return Ok(result);
        }

        [HttpGet("{id}", Name = nameof(GetSingleNutritionFact))]
        public async Task<ActionResult<NutritionFactDto>> GetSingleNutritionFact(string id)
        {
            var item = await nutritionFactCollection.GetById(id);
            if (item == null)
            {
                return NotFound();
            }

            return new NutritionFactDto(item.Id.ToString(), item.Calories, item.Protein, item.TotalCarbohydrates, item.Sugar, item.Fiber, item.Fat);
        }

        [HttpPost]
        public async Task<ActionResult<NutritionFactDto>> Add(NutritionFactCreationDto item)
        {
            var na = new NutritionFacts(ObjectId.Empty, item.Calories, item.Protein, item.TotalCarbohydrates, item.Sugar, item.Fiber, item.Fat);
            await nutritionFactCollection.InsertOneAsync(na);
            return CreatedAtRoute(nameof(GetSingleNutritionFact), new { id = na.Id },
                new NutritionFactDto(na.Id.ToString(), na.Calories, na.Protein, na.TotalCarbohydrates, na.Sugar, na.Fiber, na.Fat));
        }
    }
}
