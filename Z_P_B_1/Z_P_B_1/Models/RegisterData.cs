using System;
using System.Collections.Generic;
using System.Text;

namespace Z_P_B_1.Models
{
    public class RegisterData
    {
        public string pesel { get; set; }
        public string name { get; set; }
        public string lastName { get; set; }
        public string password { get; set; }
        public string confirmPassword { get; set; }
        public string email { get; set; }
        public string nr_tel { get; set; }
        public DateTime birthDate { get; set; }
    }
}
