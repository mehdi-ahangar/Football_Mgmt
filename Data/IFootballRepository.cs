using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Football_Mgmt.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Football_Mgmt.Data
{
    public interface IFootballRepository
    {

        // General 
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();

        // Teams
        Task<Team[]> GetAllTeamsAsync();
        Task<Team> GetTeamAsync(int TeamId);
        Task<Team[]> GetTeamsByName(string  TeanName);
        Task<bool> AddPlayerToTeam(int TeamId, int PlayerId);
        Task<bool> LinkToStadium(int TeamId, int StadiumId);

        // Players
        Task<Player[]> GetAllPlayersAsync();
        
        Task<Player> GetPlayerAsync(int PlayerId);

        Task<Player[]> GetPlayersByName(string PlayerName);

        // Stadium
        
        Task<Stadium> GetStadiumAsync(int stadiumId);
        Task<Stadium[]> GetAllStadiumsAsync();
    }
}
