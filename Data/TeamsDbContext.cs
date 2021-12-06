using Microsoft.EntityFrameworkCore;
using Football_Mgmt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Football_Mgmt.Data
{
    public class TeamsDbContext: DbContext
    {
        public TeamsDbContext(DbContextOptions <TeamsDbContext> options): base (options)
         {


        }
        public DbSet<Team> Teams { get; set; }



    }
}
