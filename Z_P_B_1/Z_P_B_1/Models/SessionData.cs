using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Z_P_B_1.Models
{
    public class SessionData
    {
        [PrimaryKey]
        public int id { get; set; }
        public string accessToken { get; set; }
        public string refreshToken { get; set; }
    }
}
