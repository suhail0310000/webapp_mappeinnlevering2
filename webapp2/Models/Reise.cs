using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MappeInnlevering_1.Models
{
    public class Reise
    {
        [Key]
        public int RId { get; set; }
        public string FraSted { get; set; }
        public string TilSted { get; set; }
        public string Dato { get; set; }
        public string avreiseTid { get; set; }
        public int PrisBarn { get; set; }
        public int PrisStudent { get; set; }
        public int PrisVoksen { get; set; }
        
    }
}
