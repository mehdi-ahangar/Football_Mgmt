using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Football_Mgmt.Models
{
    public class Stadium
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Capacity { get; set; }
        public string GrassType { get; set; }
        public byte Height { get; set; }
        public byte Width { get; set; }



    }
}
