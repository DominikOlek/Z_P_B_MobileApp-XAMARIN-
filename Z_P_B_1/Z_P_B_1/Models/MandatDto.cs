using System;
using System.Collections.Generic;
using System.Text;

namespace Z_P_B_1.Models
{
    public class MandatDto
    {
        public string tytul { get; set; }
        public string opis { get; set; }
        public int pktZaMandat { get; set; }
        public int mWstrzymania { get; set; }
        public DateTime dataWydania { get; set; }
    }
}
