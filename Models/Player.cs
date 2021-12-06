using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Football_Mgmt.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte ShirtNo { get; set; }
        public string position { get; set; }
        
        public byte Height { get; set; }
        public UInt16 Goals { get; set; }
        public DateTime BirthDay { get; set; }
        public int TeamId {get; set;}
        public Team Team { get; set; } 


        public static implicit operator List<object>(Player v)
        {
            throw new NotImplementedException();
        }
    }
}
