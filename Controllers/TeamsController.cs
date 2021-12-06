using Football_Mgmt.Data;
using Football_Mgmt.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Football_Mgmt.Controllers
{
    [Route("api/[controller]")]
    public class TeamsController : ControllerBase
    {
        private readonly IFootballRepository _repository;

        public TeamsController(IFootballRepository repository)
        {
            _repository = repository;
        }
        //***************************************************
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var results = await _repository.GetAllTeamsAsync();

                return Ok(results);

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "DataBase Failure. :(");
            }

        }
        //*******************************************************

        [HttpGet("search/{TeamName}")]
        public async Task<IActionResult> Get(string TeamName)
        {
            try
            {
                var results = await _repository.GetTeamsByName(TeamName);

                return Ok(results);

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "DataBase Failure. :(");
            }

        }
        //*************************************************************
        [HttpGet("{TeamId}")]
        public async Task<IActionResult> Get(int TeamId)
        {
            try
            {
                var results = await _repository.GetTeamAsync(TeamId);

                return Ok(results);

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "DataBase Failure. :(");
            }

        }
        //******************************************************************

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Team team)
        {
            //Add a new team
            try {
                _repository.Add(team);
                await _repository.SaveChangesAsync();
                return Created("/api/teams/", team.Id);
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "DataBase Failure. :(");
            }

        }
        //******************************************************
        [HttpPut("{TeamId}/AddPlayer")] // Add a player to a team
        public async Task<ActionResult> Put([FromBody] Player player)
        {

            int TeamId = player.TeamId;
            int PlayerId = player.Id;


            try
            {
                await _repository.AddPlayerToTeam(TeamId, PlayerId);
                await _repository.SaveChangesAsync();
                return Created("/api/teams/", TeamId);

            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "DataBase Failure. :(");
            }
        }
        //*************************************************************

        [HttpDelete("{TeamId}")]
        public async Task<ActionResult> Delete(int TeamId)
        {
            //Delete a team
            try
            {
                var oldTeam = await _repository.GetTeamAsync(TeamId);
                if (oldTeam == null) return NotFound();

                _repository.Delete(oldTeam);

                if (await _repository.SaveChangesAsync())
                    return Ok();
                                
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "DataBase Failure. :(");
            }
            return BadRequest("Failed to delete the team");
        }
        //********************************************************
        [HttpPut("LinkToStadium")] // Link to a stadium
        public async Task<ActionResult> Put([FromBody] Team team)
        {

           int TeamId = team.Id;
          int StadiumId = team.StadiumId;

            try
            {
                await _repository.LinkToStadium(TeamId, StadiumId);
                await _repository.SaveChangesAsync();
                return Created("/api/teams/", TeamId);

            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "DataBase Failure. :(");
            }
        }
        //*****************************************************
    }
}
