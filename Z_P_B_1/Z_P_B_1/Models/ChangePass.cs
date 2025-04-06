using System;
using System.Collections.Generic;
using System.Text;

namespace Z_P_B_1.Models
{
    public class ChangePass
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public int EmailCode { get; set; }

    }
}
