using Microsoft.AspNetCore.Mvc;
using Football_Mgmt.Data;
using Football_Mgmt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Football_Mgmt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StadiumsController : ControllerBase
    {
        private readonly IFootballRepository _repository;
        public StadiumsController(IFootballRepository repository)
        {
            _repository = repository;
        }

        //*****************************************************


        // GET: api/<StadiumController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var results = await _repository.GetAllStadiumsAsync();

                return Ok(results);

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "DataBase Failure. :(");
            }

        }
        //*******************************************************
        // GET api/<StadiumsController>/5
        [HttpGet("{StadiumId}")]
        public async Task<IActionResult> Get(int StadiumId)
        {
            try
            {
                var results = await _repository.GetStadiumAsync(StadiumId);

                return Ok(results);

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "DataBase Failure. :(");
            }

        }

        //***************************************************************
        // POST api/<StadiumController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Stadium NewStadium)
        {

            //Add a new stadium
            try
            {
                _repository.Add(NewStadium);
                await _repository.SaveChangesAsync();
                return Created("/api/stadiums/", NewStadium.Id);
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "DataBase Failure. :(");
            }

        }

        //*******************************************************************
        // DELETE api/<StadiumController>/5
        [HttpDelete("{StadiumId}")]
        public async Task<ActionResult> Delete(int StadiumId)
        {

           
                //Delete a stadium
                try
                {
                    var OldStadium = await _repository.GetStadiumAsync(StadiumId);
                    if (OldStadium == null) return NotFound();

                    _repository.Delete(OldStadium);

                    if (await _repository.SaveChangesAsync())
                        return Ok();

                }
                catch
                {
                    return this.StatusCode(StatusCodes.Status500InternalServerError, "DataBase Failure. :(");
                }
                return BadRequest("Failed to delete the team");
            
        }
        //***********************************************************
        

        // PUT api/<StadiumController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }
    }
}
