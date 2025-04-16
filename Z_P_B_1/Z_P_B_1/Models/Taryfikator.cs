using SQLite;

namespace Z_P_B_1.Models
{
    public class Taryfikator
    {
        [PrimaryKey]
        public int id { get; set; }
        public int pktNumber { get; set; }
        public int monthsOfLost { get; set; }
        public string title { get; set; }
    }
}
