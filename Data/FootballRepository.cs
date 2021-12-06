using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Football_Mgmt.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Football_Mgmt.Data;


namespace Football_Mgmt.Data
{
    public class FootballRepository : IFootballRepository
    {
        private readonly FootballContext _context;
        private readonly ILogger<FootballRepository> _logger;

        public FootballRepository(FootballContext context, ILogger<FootballRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        //**********************************************************
        public void Add<T>(T entity) where T : class
        {
            _logger.LogInformation($"Adding an object of type {entity.GetType()} to the context.");
            _context.Add(entity);
        }
        //**********************************************************
        public void Delete<T>(T entity) where T : class
        {
            _logger.LogInformation($"Removing an object of type {entity.GetType()} to the context.");
            _context.Remove(entity);
        }
        //************************************************************
        public async Task<bool> SaveChangesAsync()
        {
            _logger.LogInformation($"Attempitng to save the changes in the context");

            // Only return success if at least one row was changed
            return (await _context.SaveChangesAsync()) > 0;
        }
        //************************************************
        public async Task<Team[]> GetTeamsByName( string TeamName)
        {
            _logger.LogInformation($"Getting all Teams like a name...");

            IQueryable<Team> query = _context.Teams;
                       
            // Order It
            query = query.Where(t => t.Name.ToLower().Contains(TeamName.ToLower()));

            return await query.ToArrayAsync();
        }
        //********************************************************
        public async Task<Team[]> GetAllTeamsAsync()
        {
            _logger.LogInformation($"Getting all Teams");

            IQueryable<Team> query = _context.Teams;      
            
               query = _context.Teams
                  .Include(t => t.Players);

          // Order It      

             return await query.ToArrayAsync();        
        }

        
        public async Task<bool> AddPlayerToTeam(int TeamId, int PlayerId)
        {

            _context.Players.Find(PlayerId).TeamId = TeamId;
                
            return (await _context.SaveChangesAsync()) > 0 ;

        }
        //*******************************************************
        public async Task<bool> LinkToStadium(int TeamId, int StadiumId)
        {

          _context.Teams.Find(TeamId).StadiumId = StadiumId;

            return (await _context.SaveChangesAsync()) > 0;

        }
        //*******************************************************
        public async Task<Team> GetTeamAsync(int TeamId)
        {
            _logger.LogInformation($"Getting a Team for {TeamId}");

            IQueryable<Team> query = _context.Teams;

            query = query.Include(t => t.Players);                         

            // Query It
            query = query.Where(t => t.Id == TeamId);

            return await query.FirstOrDefaultAsync();
           
        }

        //******************************************************
        public async Task<Player[]> GetAllPlayersAsync()
        {
            _logger.LogInformation($"Getting all players...");


            IQueryable<Player> query = _context.Players;          
                                        

            return await query.ToArrayAsync();
            
        }
        //*******************************************************************
        public async Task<Player> GetPlayerAsync(int PlayerId)
        {
            _logger.LogInformation($"Getting a player descriptions for {PlayerId}");

            IQueryable<Player> query = _context.Players;                    
          
            query = query.Where(p => p.Id == PlayerId);

           return await query.FirstOrDefaultAsync();
           
        }

        //******************************************************
       
        public async Task<Stadium[]> GetAllStadiumsAsync()
        {
            _logger.LogInformation($"Getting all stadiums...");


            IQueryable<Stadium> query = _context.Stadiums;

           

            return await query.ToArrayAsync();

        }
        //*******************************************************************
        public async Task<Stadium> GetStadiumAsync(int StadiumId)
        {
            _logger.LogInformation($"Getting a stadium descriptions for {StadiumId}");

            IQueryable<Stadium> query = _context.Stadiums;

            query = query.Where(p => p.Id == StadiumId);

            return await query.FirstOrDefaultAsync();

        }

        //**********************************************************
        public async Task<Player[]> GetPlayersByName(string PlayerName)
        {
            _logger.LogInformation($"Getting all players like a name...");

            IQueryable<Player> query = _context.Players;

            // Order It
            query = query.Where(p => p.Name.ToLower().Contains(PlayerName.ToLower()));

            return await query.ToArrayAsync();
        }
        //********************************************************

    }
}
