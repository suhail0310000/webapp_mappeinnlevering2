using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MappeInnlevering_1.Models
{
    public class KundeOrdre
    {
        [Key]
        public string Fornavn { get; set; }
        public string Etternavn { get; set; }

        public int AntallBarn { get; set; }

        public int AntallStudent { get; set; }

        public int AntallVoksne { get; set; }

        public string Dato { get; set; }

        public string Tid { get; set; }

        //public double PrisBarn { get; set; }

        //public double PrisVoksen { get; set; }

        public string FraSted { get; set; }

        public string TilSted { get; set; }
    }
}
