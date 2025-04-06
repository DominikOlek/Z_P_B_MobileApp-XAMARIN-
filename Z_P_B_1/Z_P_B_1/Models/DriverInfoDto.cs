using System;
using System.Collections.Generic;
using System.Text;

namespace Z_P_B_1.Models
{
    public class DriverInfoDto
    {
        public MandatDto[] mandats { get; set; }
        public string[] history { get; set; }
        public string importantInfo { get; set; } = " ";
    }
}
