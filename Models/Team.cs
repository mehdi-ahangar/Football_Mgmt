using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Football_Mgmt.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StadiumId { get; set; }
        public int PlayerId { get; set; }
        public string Owner { get; set; }

        public  List<Player> Players { get; set; } = new List<Player>();
        
       
        public Team()
        {

        }
    }
}
