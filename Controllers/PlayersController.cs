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
    public class PlayersController : ControllerBase
    {
        private readonly IFootballRepository _repository;
        public PlayersController (IFootballRepository repository)
        {
            _repository = repository;                
        }

        // GET: api/<PlayersController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var results = await _repository.GetAllPlayersAsync();

                return Ok(results);

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "DataBase Failure. :(");
            }

        }
        //*******************************************************

        // GET api/<PlayersController>/5
        [HttpGet("{PlayerId}")]
        public async Task<IActionResult> Get(int PlayerId)
        {
            try
            {
                var results = await _repository.GetPlayerAsync(PlayerId);

                return Ok(results);

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "DataBase Failure. :(");
            }

        }
        //******************************************************************
        // POST api/<PlayersController>
        [HttpPost]
        public async  Task<ActionResult> Post([FromBody] Player NewPlayer)
        {

            //Add a new player
            try
            {
                _repository.Add(NewPlayer);
                await _repository.SaveChangesAsync();
                return Created("/api/players/", NewPlayer.Id);
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "DataBase Failure. :(");
            }

        }
        //*******************************************************************
        // PUT api/<PlayersController>/
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }
        //********************************************************************
        // DELETE api/<PlayersController>/
        [HttpDelete("{PlayerId}")]
        public async Task<ActionResult> Delete(int PlayerId)
        {
            //Delete a player
            try
            {
                var OldPlayer = await _repository.GetPlayerAsync(PlayerId);
                if (OldPlayer == null) return NotFound();

                _repository.Delete(OldPlayer);

                if (await _repository.SaveChangesAsync())
                    return Ok();

            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "DataBase Failure. :(");
            }
            return BadRequest("Failed to delete the player");
        }
        //********************************************************
        [HttpGet("search/{PlayerName}")]
        public async Task<IActionResult> Get(string PlayerName)
        {
            try
            {
                var results = await _repository.GetPlayersByName(PlayerName);

                return Ok(results);

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "DataBase Failure. :(");
            }

        }
        //*************************************************************
    }
}
