using System;
using System.Collections.Generic;
using System.Text;

namespace Z_P_B_1.Models
{
    public class RecipeDto
    {
        public int iD { get; set; }
        public int medicID { get; set; }
        public string pESEL { get; set; }
        public DateTime birthDate { get; set; }
        public DateTime expireDate { get; set; }
        public string description { get; set; }
    }
}
