using Microsoft.EntityFrameworkCore;
using Football_Mgmt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Football_Mgmt.Data
{
    public class PlayersDbContext : DbContext
    {
        public PlayersDbContext(DbContextOptions<PlayersDbContext> options) : base(options)
        {
        }
        public DbSet<Player> Teams { get; set; }


    }

}
