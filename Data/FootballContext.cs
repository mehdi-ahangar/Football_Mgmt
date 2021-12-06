using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Football_Mgmt.Models;

namespace Football_Mgmt.Data
{
    public class FootballContext : DbContext
    {
        private readonly IConfiguration _config;

        public FootballContext(DbContextOptions options, IConfiguration config) : base(options)
        {
            _config = config;

        }  
        public FootballContext(DbContextOptions options): base (options)
        {

        }

        public DbSet<Team> Teams {  get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Stadium> Stadiums { get; set; }

    }
}
