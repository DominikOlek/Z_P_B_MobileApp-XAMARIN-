using System;
using System.Collections.Generic;
using System.Text;

namespace Z_P_B_1.Models
{
    public class MandatDto
    {
        public string title { get; set; }
        public string description { get; set; }
        public int pktForTicket { get; set; }
        public int monthsOfLost { get; set; }
        public DateTime dateReceived { get; set; }
    }
}
