using DietTracker_Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DietTracker_Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class SampleController : ControllerBase
    {
        private readonly ISampleRepo repo;

        public SampleController(SampleRepo repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Sample>))]
        public IActionResult GetAll() => Ok(repo.GetAll());

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Sample))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public IActionResult Add([FromBody] Sample newSample)
        {
            if (newSample.name == null) return BadRequest("Name is null");

            repo.Add(newSample);
            return Created("TODO",newSample);
        }
    }
}
