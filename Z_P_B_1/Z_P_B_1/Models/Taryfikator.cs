using SQLite;

namespace Z_P_B_1.Models
{
    public class Taryfikator
    {
        [PrimaryKey]
        public int id { get; set; }
        public int liczba_PKT { get; set; }
        public int miesiąceWstrzymania { get; set; }
        public string tytul { get; set; }
    }
}
